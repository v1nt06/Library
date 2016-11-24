using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class PatentTests
    {
        private const string Name = "Телефон";
        private const int PagesCount = 1274;
        private const string RegistrationNumber = "Т-1";
        private readonly DateTime applicationDate = new DateTime(1930, 10, 5);
        private const string Country = "Франция";
        private readonly Person[] inventors = {new Person("Александр", "Белл")};
        private readonly DateTime publicationDate = new DateTime(1869, 1, 1);

        [TestMethod]
        public void CreatePatent()
        {
            var patent = new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, inventors, publicationDate);

            Assert.AreEqual(Name, patent.Name);
            Assert.AreEqual(PagesCount, patent.PagesCount);
            Assert.AreEqual(RegistrationNumber, patent.RegistrationNumber);
            Assert.AreEqual(applicationDate, patent.ApplicationDate);
            Assert.AreEqual(Country, patent.Country);
            Assert.AreEqual(publicationDate, patent.PublicationDate);
            Assert.AreEqual(inventors.Length, patent.Inventors.Length);

            for (var i = 0; i < inventors.Length; i++)
            {
                Assert.AreEqual(inventors[i].FirstName, patent.Inventors[i].FirstName);
                Assert.AreEqual(inventors[i].LastName, patent.Inventors[i].LastName);
            }
        }

        [TestMethod]
        public void PatentToString()
        {
            var patent = new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, inventors, publicationDate);

            Assert.AreEqual("Телефон №Т-1", patent.ToString());
        }
    }
}
