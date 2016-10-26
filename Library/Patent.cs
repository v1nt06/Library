using System;

namespace Library
{
    public sealed class Patent : Document
    {
        public Person[] Inventors { get; private set; }
        public string Country { get; private set; }
        public string RegistrationNumber { get; private set; }
        public DateTime ApplicationDate { get; private set; }

        public Patent(string name, int pagesCount, string registrationNumber,
            DateTime applicationDate, string country, Person[] inventors, DateTime publicationDate) : 
            base(name, pagesCount, publicationDate)
        {
            Inventors = inventors;
            Country = country;
            RegistrationNumber = registrationNumber;
            ApplicationDate = applicationDate;
        }
    }
}
