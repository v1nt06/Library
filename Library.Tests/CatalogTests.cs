using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class CatalogTests
    {
        [TestMethod]
        public void AddToCatalog()
        {
            var book = CreateTestBook();
            var newspaper = new Newspaper("Коммерсант", 30, "Москва", "Издательство", "ISSN-1", 1, DateTime.Today);
            var patent = new Patent("Телефон", 50, "T-1", DateTime.Today, "Россия",
                new List<Person> { new Person("Ivan", "ivanov") }, DateTime.Today);

            var documents = new List<Document> { book, newspaper, patent };

            Catalog.Add(book);
            Catalog.Add(newspaper);
            Catalog.Add(patent);

            CollectionAssert.AreEqual(documents, Catalog.GetCatalogContent().ToList());

            ResetCatalog();
        }

        [TestMethod]
        public void RemoveFromCatalog()
        {
            Catalog.Add(CreateTestBook());

            Catalog.Remove(0);

            Assert.AreEqual(0, Catalog.GetCatalogContent().Count());

            ResetCatalog();
        }

        [TestMethod]
        public void CheckCatalogContent()
        {
            var book = CreateTestBook();
            var docments = new List<Document> { book, book };

            Catalog.Add(book);
            Catalog.Add(book);

            CollectionAssert.AreEqual(docments, Catalog.GetCatalogContent().ToList());

            ResetCatalog();
        }

        [TestMethod]
        public void FindByName()
        {
            Catalog.Add(CreateTestBook());

            Assert.AreEqual("Война и мир", Catalog.FindByName("Война и мир").First().Name);
            Assert.AreEqual(0, Catalog.FindByName("Ведьмак").Count());

            ResetCatalog();
        }

        [TestMethod]
        public void SortByPublicationDate()
        {
            var book2000 = new Book("Война и мир", 1274, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Лев", "Толстой") }, new DateTime(2000, 1, 1));
            var book2010 = new Book("Война и мир", 1274, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Лев", "Толстой") }, new DateTime(2010, 1, 1));
            var book2020 = new Book("Война и мир", 1274, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Лев", "Толстой") }, new DateTime(2020, 1, 1));
            var documents = new List<Document> { book2000, book2010, book2020 };

            Catalog.Add(book2010);
            Catalog.Add(book2000);
            Catalog.Add(book2020);

            CollectionAssert.AreEqual(documents, Catalog.GetSortedContentByPublicationDateAsc().ToList());

            documents = new List<Document> { book2020, book2010, book2000 };

            CollectionAssert.AreEqual(documents, Catalog.GetSortedContentByPublicationDateDesc().ToList());

            ResetCatalog();
        }

        [TestMethod]
        public void FindBooksByAuthor()
        {
            var bookLev = new Book("Война и мир", 500, "Москва", "Издательство", "ISBN-1",
                new List<Person> { new Person("Лев", "Толстой"), }, DateTime.Today);
            var bookChakLev = new Book("Бойцовский клуб", 500, "Москва", "Другое издательство", "ISBN-2",
                new List<Person> { new Person("Чак", "Паланик"), new Person("Лев", "Толстой"), }, DateTime.Today.AddDays(-1));
            var bookAnjey = new Book("Ведьмак", 500, "Москва", "Издательство", "ISSN-3",
                new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
            var documents = new List<Document> { bookLev, bookChakLev };

            Catalog.Add(bookLev);
            Catalog.Add(bookChakLev);
            Catalog.Add(bookAnjey);

            CollectionAssert.AreEqual(documents, Catalog.GetBooksByAuthor(new Person("Лев", "Толстой")).ToList());

            ResetCatalog();
        }

        [TestMethod]
        public void GetBooksGroupedByPublisherWithPublisherNamePart()
        {
            var bookPublisherAnother1 = new Book("Война и мир", 500, "Москва", "Издательство другое", "ISBN-1",
                new List<Person> { new Person("Лев", "Толстой"), }, DateTime.Today);
            var bookAnotherPublisher = new Book("Бойцовский клуб", 500, "Москва", "Другое издательство", "ISBN-2",
                new List<Person> { new Person("Чак", "Паланик"), new Person("Лев", "Толстой"), }, DateTime.Today.AddDays(-1));
            var bookPublisher = new Book("Ведьмак", 500, "Москва", "Издательство", "ISSN-3",
                new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
            var bookPublisherAnother2 = new Book("Хлеб с ветчиной", 500, "Москва", "Издательство другое", "ISSN-3",
                new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
            var documents = new List<Document> { bookPublisher, bookPublisherAnother1, bookPublisherAnother2 };

            Catalog.Add(bookPublisherAnother1);
            Catalog.Add(bookAnotherPublisher);
            Catalog.Add(bookPublisher);
            Catalog.Add(bookPublisherAnother2);

            CollectionAssert.AreEqual(documents, Catalog.GetBooksByPartOfPublisher("Изд").ToList());

            ResetCatalog();
        }

        private Book CreateTestBook()
        {
            return new Book("Война и мир", 1274, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Lev", "Tolstoy") }, new DateTime(1900, 1, 1));
        }

        private void ResetCatalog()
        {
            while (Catalog.GetCatalogContent().Any())
            {
                Catalog.Remove(0);
            }
        }
    }
}
