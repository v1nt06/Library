﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class CatalogTests
    {
        [TestMethod]
        public void AddToCatalog()
        {
            try
            {
                var book = CreateTestBook();
                var newspaper = new Newspaper("Коммерсант", 30, "Москва", "Издательство", "ISSN 0378-5955", 1, DateTime.Today);
                var patent = new Patent("Телефон", 50, "123456", DateTime.Today, "Россия",
                    new List<Person> { new Person("Ivan", "ivanov") }, DateTime.Today);

                var documents = new List<Document> { book, newspaper, patent };

                Catalog.Add(book);
                Catalog.Add(newspaper);
                Catalog.Add(patent);

                CollectionAssert.AreEqual(documents, Catalog.GetCatalogContent().ToList());
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void RemoveFromCatalog()
        {
            try
            {
                Catalog.Add(CreateTestBook());

                Catalog.Remove(0);

                Assert.AreEqual(0, Catalog.GetCatalogContent().Count());

            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void CheckCatalogContent()
        {
            try
            {
                var book = CreateTestBook();
                var docments = new List<Document> { book, book };

                Catalog.Add(book);
                Catalog.Add(book);

                CollectionAssert.AreEqual(docments, Catalog.GetCatalogContent().ToList());

            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void FindByName()
        {
            try
            {
                Catalog.Add(CreateTestBook());

                Assert.AreEqual("Война и мир", Catalog.FindByName("Война и мир").First().Name);
                Assert.AreEqual(0, Catalog.FindByName("Ведьмак").Count());
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void SortByPublicationDate()
        {
            try
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
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void FindBooksByAuthor()
        {
            try
            {
                var bookLev = new Book("Война и мир", 500, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Лев", "Толстой"), }, DateTime.Today);
                var bookChakLev = new Book("Бойцовский клуб", 500, "Москва", "Другое издательство", "ISBN 978-3-16-148410-0",
                    new List<Person> { new Person("Чак", "Паланик"), new Person("Лев", "Толстой"), }, DateTime.Today.AddDays(-1));
                var bookAnjey = new Book("Ведьмак", 500, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                    new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
                var documents = new List<Document> { bookLev, bookChakLev };

                Catalog.Add(bookLev);
                Catalog.Add(bookChakLev);
                Catalog.Add(bookAnjey);

                CollectionAssert.AreEqual(documents, Catalog.GetBooksByAuthor(new Person("Лев", "Толстой")).ToList());
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void GetBooksGroupedByPublisherWithPublisherNamePart()
        {
            try
            {
                var bookPublisherAnother1 = new Book("Война и мир", 500, "Москва", "Издательство другое", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Лев", "Толстой"), }, DateTime.Today);
                var bookAnotherPublisher = new Book("Бойцовский клуб", 500, "Москва", "Другое издательство", "ISBN 978-3-16-148410-0",
                    new List<Person> { new Person("Чак", "Паланик"), new Person("Лев", "Толстой"), }, DateTime.Today.AddDays(-1));
                var bookPublisher = new Book("Ведьмак", 500, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                    new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
                var bookPublisherAnother2 = new Book("Хлеб с ветчиной", 500, "Москва", "Издательство другое", "ISBN 978-3-16-148410-0",
                    new List<Person> { new Person("Анджей", "Сапковский") }, DateTime.Today.AddYears(1));
                var documents = new List<Document> { bookPublisher, bookPublisherAnother1, bookPublisherAnother2 };

                Catalog.Add(bookPublisherAnother1);
                Catalog.Add(bookAnotherPublisher);
                Catalog.Add(bookPublisher);
                Catalog.Add(bookPublisherAnother2);

                CollectionAssert.AreEqual(documents, Catalog.GetBooksByPartOfPublisher("Изд").ToList());
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void GetContentByCriteria()
        {
            try
            {
                var book = CreateTestBook();
                var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1, new DateTime(2016, 12, 1));
                var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                    new List<Person> { new Person("Ivan", "Ivanov") }, new DateTime(2016, 12, 1));
                var correctData = new List<Document> { newspaper, book };
                Catalog.Add(book);
                Catalog.Add(newspaper);
                Catalog.Add(patent);

                CollectionAssert.AreEqual(correctData, Catalog.Get(d => d is PrintedProduct,
                    d => d.PagesCount, true).ToList());
            }
            finally
            {
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void CheckSavingAndLoadingCatalogContent()
        {
            var filePath = "XML/savedContent.xml";
            try
            {
                var book = CreateTestBook();
                book.Annotation = "Test annotation";
                var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1, new DateTime(2016, 12, 1));
                var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                    new List<Person> { new Person("Ivan", "Ivanov") }, new DateTime(2016, 12, 1));
                var originalContent = new List<Document> { book, newspaper, patent };
                Catalog.Add(originalContent);

                
                Catalog.Save(filePath);
                Catalog.Clear();
                Assert.IsTrue(File.Exists(filePath));
                Catalog.Load(filePath);

                CollectionAssert.AreEqual(originalContent, Catalog.GetCatalogContent().ToList());
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                Catalog.Clear();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaValidationException))]
        public void CheckLoadingIncorrectXml()
        {
            Catalog.Load("XML/IncorrectCatalog.xml");
        }

        [TestMethod]
        public void LoadTransformedXml()
        {
            var book = CreateTestBook();
            book.Annotation = "Test annotation";
            var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1, new DateTime(2016, 12, 1));
            var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                new List<Person> { new Person("Ivan", "Ivanov") }, new DateTime(2016, 12, 1));
            var originalContent = new List<Document> { book, newspaper, patent };

            Catalog.Load("XML/Transformed.xml", "XML/Documents.xsd", "XML/Test.xslt");

            CollectionAssert.AreEqual(originalContent, Catalog.GetCatalogContent().ToList());

            Catalog.Clear();
        }

        [TestMethod]
        public void SaveCatalogAsRss2()
        {
            var xml = "XML/rss2.xml";
            try
            {
                var book = CreateTestBook();
                book.Annotation = "Test annotation";
                var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1,
                    new DateTime(2016, 12, 1));
                var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                    new List<Person> {new Person("Ivan", "Ivanov")}, new DateTime(2016, 12, 1));
                Assert.AreEqual(0, Catalog.GetCatalogContent().Count());
                Catalog.Add(new Document[] {book, newspaper, patent});

                Catalog.Save(xml, "XML/rss2.xslt");

                var expected = File.ReadAllLines("XML/rss2-expected.xml");
                CollectionAssert.AreEqual(expected, File.ReadAllLines(xml));
            }
            finally
            {
                if (File.Exists(xml))
                {
                    File.Delete(xml);
                }
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void SaveCatalogAsAtom()
        {
            var xml = "XML/atom.xml";
            try
            {
                var book = CreateTestBook();
                book.Annotation = "Test annotation";
                var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1,
                    new DateTime(2016, 12, 1));
                var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                    new List<Person> { new Person("Ivan", "Ivanov") }, new DateTime(2016, 12, 1));
                Assert.AreEqual(0, Catalog.GetCatalogContent().Count());
                Catalog.Add(new Document[] { book, newspaper, patent });

                Catalog.Save(xml, "XML/atom.xslt");

                var expected = File.ReadAllLines("XML/atom-expected.xml");
                CollectionAssert.AreEqual(expected, File.ReadAllLines(xml));
            }
            finally
            {
                if (File.Exists(xml))
                {
                    File.Delete(xml);
                }
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void SaveCatalogAsSampleXmlFile()
        {
            var xml = "XML/sample.xml";
            try
            {
                var book = CreateTestBook();
                book.Annotation = "Test annotation";
                var newspaper = new Newspaper("Коммерсант", 30, "Архангельск", "Издательство", "ISSN 0378-5955", 1,
                    new DateTime(2016, 12, 1));
                var patent = new Patent("Телефон", 50, "123456", new DateTime(2016, 12, 1), "Россия",
                    new List<Person> { new Person("Ivan", "Ivanov") }, new DateTime(2016, 12, 1));
                Assert.AreEqual(0, Catalog.GetCatalogContent().Count());
                Catalog.Add(new Document[] { book, newspaper, patent });

                Catalog.Save(xml, "XML/sample.xslt");

                var expected = File.ReadAllLines("XML/sample-expected.xml");
                CollectionAssert.AreEqual(expected, File.ReadAllLines(xml));
            }
            finally
            {
                if (File.Exists(xml))
                {
                    File.Delete(xml);
                }
                Catalog.Clear();
            }
        }

        [TestMethod]
        public void CreateReport()
        {
            var report = "XML/report.html";
            try
            {
                Catalog.CreateReport("XML/SourceData.xml", "XML/Test.xslt", report);

                var expectedLines = File.ReadAllLines("XML/report-expected.html");
                var actualLines = File.ReadAllLines(report);
                Assert.AreEqual(expectedLines.Length, actualLines.Length);

                for (var i = 0; i < expectedLines.Length; i++)
                {
                    // Checking all lines in report files except for line 10 (index 9) and line 110 (index 109)
                    // becasue this lines contains current date and time.
                    if (i != 9 && i != 109)
                    {
                        Assert.AreEqual(expectedLines[i], actualLines[i]);
                    }
                }
            }
            finally
            {
                if (File.Exists(report))
                {
                    File.Delete(report);
                }
                Catalog.Clear();
            }
        }

        private Book CreateTestBook()
        {
            return new Book("Война и мир", 1274, "Москва", "Издательство", "ISBN 978-3-16-148410-0",
                new List<Person> { new Person("Lev", "Tolstoy") }, new DateTime(1900, 1, 1));
        }
    }
}
