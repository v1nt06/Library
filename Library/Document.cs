using System;

namespace Library
{
    public abstract class Document : IDocument
    {
        public string Name { get; }
        public int PagesCount { get; }
        public string Annotation { get; set; }
        public DateTime PublicationDate { get; }

        protected Document(string name, int pagesCount, DateTime publicationDate)
        {
            Name = name;
            PagesCount = pagesCount;
            PublicationDate = publicationDate;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Document)) return -1;
            if (PublicationDate > ((Document) obj).PublicationDate)
            {
                return 1;
            }

            if (PublicationDate < ((Document) obj).PublicationDate)
            {
                return -1;
            }

            return 0;
        }
    }
}
