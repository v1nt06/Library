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
            if (inventors == null)
            {
                throw new ArgumentException("Inventors should contains at least one person", "inventors");
            }

            foreach (var inventor in inventors)
            {
                if (inventor == null)
                {
                    throw new ArgumentException("Inventor shouldn't be null", "inventors");
                }
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
    }
}
