﻿using System;
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

        public static void Add(Document document)
        {
            Documents.Add(document);
        }

        public static void Add(IEnumerable<Document> documents)
        {
            Documents.AddRange(documents);
        }

        public static void Remove(int index)
        {
            Documents.Remove(Documents[index]);
        }

        public static IEnumerable<Document> GetCatalogContent()
        {
            return Documents;
        }

        public static IEnumerable<Document> FindByName(string name)
        {
            return Documents.Where(d => d.Name == name);
        }

        public static IEnumerable<Document> GetSortedContentByPublicationDateAsc()
        {
            return Documents.OrderBy(d => d.PublicationDate);
        }

        public static IEnumerable<Document> GetSortedContentByPublicationDateDesc()
        {
            return Documents.OrderByDescending(d => d.PublicationDate);
        }

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

        public static IEnumerable<Book> GetBooksByPartOfPublisher(string partOfPublisherName)
        {
            return Documents.Where(d => d is Book && ((Book)d).Publisher.StartsWith(partOfPublisherName))
                .Cast<Book>().OrderBy(b => b.Publisher);
        }

        public static void Save(string path, string stylesheet = null)
        {
            SaveXml(path);
            if (!string.IsNullOrEmpty(stylesheet))
            {
                TransformXml(path, stylesheet, false);
            }
        }

        private static void SaveXml(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(CatalogContent), "http://www.library/catalog");
            try
            {
                using (var fileStream = new FileStream(path, FileMode.CreateNew))
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

        public static void Load(string xml, string schema = "XML/Documents.xsd", string stylesheet = null)
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

        private static string TransformXml(string xml, string stylesheet, bool rename = true)
        {
            var xslt = new XslCompiledTransform();
            var settings = new XsltSettings { EnableScript = true };
            xslt.Load(stylesheet, settings, new XmlUrlResolver());

            var document = new XPathDocument(xml);

            if (rename)
            {
                xml = Guid.NewGuid() + ".xml";
            }

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

        public static void CreateReport(string xml, string stylesheet, string reportPath)
        {
            Load(xml, stylesheet: stylesheet);
            Save(reportPath, "XML/Report.xslt");
        }
    }
}
