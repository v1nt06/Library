using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class NewspaperTests
    {
        private const string Name = "Известия";
        private const int PagesCount = 50;
        private const string PlaceOfPublication = "Москва";
        private const string Publisher = "Издательство";
        private const string ISSN = "ISSN 2049-3630";
        private const int Number = 12;
        private readonly DateTime publicationDate = new DateTime(2016, 11, 23);

        [TestMethod]
        public void CreateNewspaper()
        {
            var newspaper = new Newspaper(Name, PagesCount, PlaceOfPublication, Publisher, ISSN, Number, publicationDate);

            Assert.AreEqual(Name, newspaper.Name);
            Assert.AreEqual(PagesCount, newspaper.PagesCount);
            Assert.AreEqual(PlaceOfPublication, newspaper.PlaceOfPublication);
            Assert.AreEqual(Publisher, newspaper.Publisher);
            Assert.AreEqual(ISSN, newspaper.ISSN);
            Assert.AreEqual(Number, newspaper.Number);
            Assert.AreEqual(publicationDate, newspaper.PublicationDate);
        }

        [TestMethod]
        public void NewspaperToString()
        {
            var newspaper = new Newspaper(Name, PagesCount, PlaceOfPublication, Publisher, ISSN, Number, publicationDate);

            Assert.AreEqual("Известия №12", newspaper.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewspaperWithoutName()
        {
            new Newspaper(null, PagesCount, PlaceOfPublication, Publisher, ISSN, Number, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewspaperWithEmptyName()
        {
            new Newspaper(" ", PagesCount, PlaceOfPublication, Publisher, ISSN, Number, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewspaperWithoutPublisher()
        {
            new Newspaper(Name, PagesCount, PlaceOfPublication, null, ISSN, Number, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewspaperWithEmptytPublisher()
        {
            new Newspaper(Name, PagesCount, PlaceOfPublication, " ", ISSN, Number, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateNewspaperWithPublicationDateBelow1900()
        {
            new Newspaper(Name, PagesCount, PlaceOfPublication, Publisher, ISSN, Number, new DateTime(1899, 12, 31));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewspaperWithNonPositiveNumber()
        {
            new Newspaper(Name, PagesCount, PlaceOfPublication, Publisher, ISSN, 0, publicationDate);
        }
    }
}
