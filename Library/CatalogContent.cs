using System.Collections.Generic;
using System.Xml.Serialization;

namespace Library
{
    public sealed class CatalogContent
    {
        [XmlArrayItem(typeof(Book))]
        [XmlArrayItem(typeof(Newspaper))]
        [XmlArrayItem(typeof(Patent))]
        public List<Document> Documents { get; set; }

        public CatalogContent(List<Document> documents)
        {
            Documents = documents;
        }

        private CatalogContent() { }
    }
}