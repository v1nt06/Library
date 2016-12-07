using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public sealed class Patent : Document
    {
        public IList<Person> Inventors { get; }
        public string Country { get; }
        public string RegistrationNumber { get; }
        public DateTime ApplicationDate { get; }

        public Patent(string name, int pagesCount, string registrationNumber,
            DateTime applicationDate, string country, IList<Person> inventors, DateTime publicationDate) : 
            base(name, pagesCount, publicationDate)
        {
            if (inventors == null || inventors.Count == 0)
            {
                throw new ArgumentException("Inventors should contains at least one person", "inventors");
            }

            if (inventors.Any(inventor => inventor == null))
            {
                throw new ArgumentException("Inventor shouldn't be null", "inventors");
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("Country shouldn't be null", "country");
            }

            if (applicationDate.Year < 1950)
            {
                throw new ArgumentOutOfRangeException("applicationDate", "Application date should be greater than 1949");
            }

            if (publicationDate.Year < 1950)
            {
                throw new ArgumentOutOfRangeException("publicationDate", "Publication date should be greater than 1949");
            }

            Inventors = inventors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
        }

        public override string ToString()
        {
            return $"{Name} №{RegistrationNumber}";
        }

        public override bool Equals(object obj)
        {
            var patent = obj as Patent;

            if (Inventors.Count != patent?.Inventors.Count)
            {
                return false;
            }

            var areEqual = base.Equals(obj) && Country == patent.Country
                && RegistrationNumber == patent.RegistrationNumber && ApplicationDate == patent.ApplicationDate;

            for (var i = 0; i < Inventors.Count; i++)
            {
                areEqual = areEqual && Inventors[i].Equals(patent.Inventors[i]);
            }

            return areEqual;
        }
    }
}
