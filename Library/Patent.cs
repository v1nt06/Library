using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
    public sealed class Patent : Document
    {
        private string registrationNumber;

        public List<Person> Inventors { get; set; }
        public string Country { get; set; }
        public DateTime ApplicationDate { get; set; }

        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set
            {
                if (IsRegNumberCorrect(value))
                {
                    registrationNumber = value;
                }
                else
                {
                    throw new ArgumentException("Invalid registration number", "registrationNumber");
                }
            }
        }

        private Patent() { }

        public Patent(string name, int pagesCount, string registrationNumber,
            DateTime applicationDate, string country, IEnumerable<Person> inventors, DateTime publicationDate) :
            base(name, pagesCount, publicationDate)
        {
            if (inventors == null || inventors.Count() == 0)
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

            if (!IsRegNumberCorrect(registrationNumber))
            {
                throw new ArgumentException("Invalid registration number", "registrationNumber");
            }

            Inventors = inventors.ToList();
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
        }

        private bool IsRegNumberCorrect(string registrationNumber)
        {
            return Regex.IsMatch(registrationNumber, @"^\d{6,7}$")
                   || Regex.IsMatch(registrationNumber, @"^RE\d{6}$")
                   || Regex.IsMatch(registrationNumber, @"^PP\d{6}$")
                   || Regex.IsMatch(registrationNumber, @"^AI\d{6}$")
                   || Regex.IsMatch(registrationNumber, @"^[DXHT]\d{7}$")
                   || Regex.IsMatch(registrationNumber, @"^\d+ - \d{4}/\d+$");
        }

        public override string ToString()
        {
            return $"{Name} №{RegistrationNumber}";
        }

        public override bool Equals(object obj)
        {
            var areEquals = base.Equals(obj);

            var patent = obj as Patent;
            areEquals = areEquals && Country == patent.Country
                && RegistrationNumber == patent.RegistrationNumber
                && ApplicationDate == patent.ApplicationDate;

            if (Inventors.Count != patent?.Inventors.Count)
            {
                return false;
            }
            for (var i = 0; i < Inventors.Count; i++)
            {
                areEquals = areEquals && Inventors[i].Equals(patent.Inventors[i]);
            }

            return areEquals;
        }
    }
}
