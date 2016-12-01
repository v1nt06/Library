using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class PatentTests
    {
        private const string Name = "Телефон";
        private const int PagesCount = 1274;
        private const string RegistrationNumber = "Т-1";
        private readonly DateTime applicationDate = new DateTime(1950, 1, 1);
        private const string Country = "Франция";
        private readonly IList<Person> inventors = new List<Person> { new Person("Alexander", "Bell") };
        private readonly DateTime publicationDate = new DateTime(1950, 1, 1);

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
            Assert.AreEqual(inventors.Count, patent.Inventors.Count);

            for (var i = 0; i < inventors.Count; i++)
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithoutName()
        {
            new Patent(null, PagesCount, RegistrationNumber, applicationDate, Country, inventors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithEmptyName()
        {
            new Patent(" ", PagesCount, RegistrationNumber, applicationDate, Country, inventors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithoutInventors()
        {
            new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, null, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithEmptyInventors()
        {
            new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, new List<Person>(), publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithoutCountry()
        {
            new Patent(Name, PagesCount, RegistrationNumber, applicationDate, null, inventors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreatePatentWithEmptyCountry()
        {
            new Patent(Name, PagesCount, RegistrationNumber, applicationDate, " ", inventors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreatePatentWithApplicationDateBelow1950()
        {
            new Patent(Name, PagesCount, RegistrationNumber, new DateTime(1949, 12, 31), Country, inventors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreatePatentWithPublicationDateBelow1950()
        {
            new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, inventors, new DateTime(1949, 12, 31));
        }

        [TestMethod]
        public void ChangePatentAnnotation()
        {
            var patent = new Patent(Name, PagesCount, RegistrationNumber, applicationDate, Country, inventors, publicationDate);

            var newAnnotation = "New annotation";
            patent.ChangeAnnotation(newAnnotation);

            Assert.AreEqual(newAnnotation, patent.Annotation);
        }
    }
}
