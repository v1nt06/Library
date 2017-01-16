using System;
using System.Collections.Generic;
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
        private readonly IList<Person> authors = new List<Person> { new Person("Lev", "Tolstoy") };
        private readonly DateTime publicationDate = new DateTime(1900, 1, 1);

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
            Assert.AreEqual(authors.Count, book.Authors.Count);

            for (var i = 0; i < authors.Count; i++)
            {
                Assert.AreEqual(authors[i].FirstName, book.Authors[i].FirstName);
                Assert.AreEqual(authors[i].LastName, book.Authors[i].LastName);
            }
        }

        [TestMethod]
        public void BookToString()
        {
            var book = new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);

            Assert.AreEqual("Война и мир - L. Tolstoy", book.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithoutName()
        {
            new Book(null, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithEmptyName()
        {
            new Book(" ", PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithoutAuthors()
        {
            new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, null, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithEmptyAuthors()
        {
            new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, new List<Person>(), publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithoutPlaceOfPublication()
        {
            new Book(Name, PagesCount, null, Publisher, ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithEmptyPlaceOfPublication()
        {
            new Book(Name, PagesCount, " ", Publisher, ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithoutPublisher()
        {
            new Book(Name, PagesCount, PlaceOfPublication, null, ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithEmptyPublisher()
        {
            new Book(Name, PagesCount, PlaceOfPublication, " ", ISBN, authors, publicationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBookWithPublicationDateBelow1900()
        {
            new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, new DateTime(1899, 12, 31));
        }

        [TestMethod]
        public void ChangeBookProperties()
        {
            var book = new Book(Name, PagesCount, PlaceOfPublication, Publisher, ISBN, authors, publicationDate);

            var newName = "Метель";
            var newAnnotation = "New annotation";
            var newPagesCount = 777;
            var newPublicationDate = DateTime.Today;
            var newPlaceOfPublication = "Ижевск";
            var newPublisher = "Другое издательство";
            var newAuthors = new List<Person> {new Person("Александр", "Пушкин")};
            var newISBN = "ISBN 978-1-934293-06-5";

            book.Name = newName;
            book.Annotation = newAnnotation;
            book.PagesCount = newPagesCount;
            book.PublicationDate = newPublicationDate;
            book.PlaceOfPublication = newPlaceOfPublication;
            book.Publisher = newPublisher;
            book.Authors = newAuthors;
            book.ISBN = newISBN;

            Assert.AreEqual(newName, book.Name);
            Assert.AreEqual(newAnnotation, book.Annotation);
            Assert.AreEqual(newPagesCount, book.PagesCount);
            Assert.AreEqual(newPublicationDate, book.PublicationDate);
            Assert.AreEqual(newPlaceOfPublication, book.PlaceOfPublication);
            Assert.AreEqual(newPublisher, book.Publisher);
            CollectionAssert.AreEqual(newAuthors, book.Authors);
            Assert.AreEqual(newISBN, book.ISBN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBookWithIncorrectISBN()
        {
            new Book(Name, PagesCount, PlaceOfPublication, Publisher, "ISBN 978-3-16-148410-1", authors, publicationDate);
        }
    }
}
