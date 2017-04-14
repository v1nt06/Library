using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Library
{
    public static class Catalog
    {
        private static readonly List<Document> Documents = new List<Document>();

        // 1. Добавление записей в каталог.
        public static void Add(Document document)
        {
            Documents.Add(document);
        }

        public static void Add(IEnumerable<Document> documents)
        {
            Documents.AddRange(documents);
        }

        // 2. Удаление записей из каталога.
        public static void Remove(int index)
        {
            Documents.Remove(Documents[index]);
        }

        // 3. Просмотр каталога.
        /* 
            Так как у нас проект типа Class library, то вывод какой-либо информации реализовать
            не возможно (например в консоль или в какой-нибудь контрол на форме). Так что этот метод 
            просто возвращает содержимое каталога.
        */
        public static IEnumerable<Document> GetCatalogContent()
        {
            return Documents;
        }

        // 4. Поиск по названию.
        public static IEnumerable<Document> FindByName(string name)
        {
            return Documents.Where(d => d.Name == name);
        }

        // 5. Сортировка по году выпуска в прямом порядке.
        // 8. Группировка записей по годам издания.
        /*
            Так как тут написано "записей", а не книг, то я буду группировать все типы (в том числе и патенты у которых нет
            такого понятия как "год издания").
        */
        public static IEnumerable<Document> GetSortedContentByPublicationDateAsc()
        {
            return Documents.OrderBy(d => d.PublicationDate);
        }

        // 5. Сортировка по году выпуска в обратном порядке.
        public static IEnumerable<Document> GetSortedContentByPublicationDateDesc()
        {
            return Documents.OrderByDescending(d => d.PublicationDate);
        }

        // 6. Поиск всех книг данного автора (в том числе, тех, у которых он является соавтором).
        public static IEnumerable<Book> GetBooksByAuthor(Person suitableAuthor)
        {
            return
                Documents.Where(
                    d =>
                        d is Book &&
                        ((Book)d).Authors.Any(
                            a => a.FirstName == suitableAuthor.FirstName && a.LastName == suitableAuthor.LastName))
                    .Cast<Book>();
        }

        // 7. Вывод всех книг, название издательства которых начинаются с заданного набора символов, с группировкой по издательству.
        public static IEnumerable<Book> GetBooksByPartOfPublisher(string partOfPublisherName)
        {
            return Documents.Where(d => d is Book && ((Book)d).Publisher.StartsWith(partOfPublisherName))
                .Cast<Book>().OrderBy(b => b.Publisher);
        }

        public static void Save(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(CatalogContent), "http://www.library/catalog");
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    var catalogContent = new CatalogContent(Documents);
                    xmlSerializer.Serialize(fileStream, catalogContent);
                }
            }
            catch (IOException e)
            {
                throw new SaveCatalogException(e.Message, e);
            }
        }

        public static void Load(string xml, string schema, string stylesheet = null)
        {
            if (!string.IsNullOrEmpty(stylesheet))
            {
                xml = TransformXml(xml, stylesheet);
                CheckXml(xml, schema);
                DeserializeXml(xml);
                File.Delete(xml);
            }
            else
            {
                CheckXml(xml, schema);
                DeserializeXml(xml);
            }
        }

        private static string TransformXml(string xml, string stylesheet)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load(stylesheet);

            var document = new XPathDocument(xml);

            xml = Guid.NewGuid() + ".xml";
            using (var writer = new XmlTextWriter(xml, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                xslt.Transform(document, null, writer);
            }
            return xml;
        }

        private static void DeserializeXml(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(CatalogContent), "http://www.library/catalog");
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var catalogContent = (CatalogContent)xmlSerializer.Deserialize(fileStream);
                    Add(catalogContent.Documents);

                }
            }
            catch (FileNotFoundException e)
            {
                throw new LoadCatalogException(e.Message, e);
            }
            catch (InvalidOperationException e)
            {
                throw new LoadCatalogException(e.Message, e);
            }
        }

        private static void CheckXml(string filePath, string xmlSchema)
        {
            var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
            settings.Schemas.Add("http://www.library/catalog", xmlSchema);
            settings.ValidationEventHandler +=
                delegate (object sender, ValidationEventArgs e)
                {
                    throw new XmlSchemaValidationException($"{e.Severity}: {e.Message}", e.Exception);
                };

            using (var reader = XmlReader.Create(filePath, settings))
            {
                while (reader.Read())
                {
                }
            }
        }

        public static IEnumerable<Document> Get(Func<Document, bool> searchCriteria,
            Func<Document, object> orderCriteria, bool ascending)
        {
            if (ascending)
            {
                return GetCatalogContent().Where(searchCriteria).OrderBy(orderCriteria);
            }

            return GetCatalogContent().Where(searchCriteria).OrderByDescending(orderCriteria);
        }

        public static void Clear()
        {
            Documents.Clear();
        }
    }
}
