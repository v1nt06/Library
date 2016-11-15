using System;

namespace Library
{
    public sealed class Patent : Document
    {
        public Person[] Inventors { get; }
        public string Country { get; }
        public string RegistrationNumber { get; }
        public DateTime ApplicationDate { get; }

        public Patent(string name, int pagesCount, string registrationNumber,
            DateTime applicationDate, string country, Person[] inventors, DateTime publicationDate) : 
            base(name, pagesCount, publicationDate)
        {
            Inventors = inventors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
        }

        public override string ToString()
        {
            return $"{Name} №{RegistrationNumber}";
        }
    }
}
