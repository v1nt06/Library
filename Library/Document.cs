using System;

namespace Library
{
    public abstract class Document
    {
        public string Name { get; }
        public int PagesCount { get; }
        public string Annotation { get; set; }
        public DateTime PublicationDate { get; }

        protected Document(string name, int pagesCount, DateTime publicationDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name shouldn't be empty", "name");
            }

            Name = name;
            PagesCount = pagesCount;
            PublicationDate = publicationDate;
        }

        public override string ToString()
        {
            return Name;
        }

        public void ChangeAnnotation(string newAnnotation)
        {
            Annotation = newAnnotation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var document = (Document)obj;

            return Name == document.Name && PagesCount == document.PagesCount
                   && Annotation == document.Annotation && PublicationDate == document.PublicationDate;
        }
    }
}
