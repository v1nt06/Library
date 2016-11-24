using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class BookTests
    {
        private const string Name = "Война и мир";
        private const int PagesCount = 1274;
        private const string PlaceOfPublication = "Москва";
        private const string Publisher = "Издательство";
        private const string ISBN = "ISBN 978-3-16-148410-0";
        private readonly Person[] authors = {new Person("Лев", "Толстой")};
        private readonly DateTime publicationDate = new DateTime(1869, 1, 1);

        [TestMethod]
        public void CreateBook()
        {
            var book = new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);

            Assert.AreEqual(Name, book.Name);
            Assert.AreEqual(PagesCount, book.PagesCount);
            Assert.AreEqual(PlaceOfPublication, book.PlaceOfPublication);
            Assert.AreEqual(Publisher, book.Publisher);
            Assert.AreEqual(ISBN, book.ISBN);
            Assert.AreEqual(publicationDate, book.PublicationDate);
            Assert.AreEqual(authors.Length, book.Authors.Length);

            for (var i = 0; i < authors.Length; i++)
            {
                Assert.AreEqual(authors[i].FirstName, book.Authors[i].FirstName);
                Assert.AreEqual(authors[i].LastName, book.Authors[i].LastName);
            }
        }

        [TestMethod]
        public void BookToString()
        {
            var book = new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);
            
            Assert.AreEqual("Война и мир - Л. Толстой", book.ToString());
        }
    }
}
