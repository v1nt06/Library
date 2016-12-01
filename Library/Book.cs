using System;

namespace Library
{
    public sealed class Book : PrintedProduct
    {
        public Person[] Authors { get; }
        public string ISBN { get; }

        public Book(string name, int pagesCount, string placeOfPublication, string publisher,
            string isbn, Person[] authors, DateTime publicationDate) :
            base(name, pagesCount, placeOfPublication, publisher, publicationDate)
        {
            if (authors == null || authors.Length == 0)
            {
                throw new ArgumentException("Authors should contains at least one person", "authors");
            }

            if (string.IsNullOrWhiteSpace(placeOfPublication))
            {
                throw new ArgumentException("Place of publication shouldn't be empty", "placeOfPublication");
            }

            foreach (var author in authors)
            {
                if (author == null)
                {
                    throw new ArgumentException("Author shouldn't be null", "author");
                }
            }

            Authors = authors;
            ISBN = isbn;
        }

        public override string ToString()
        {
            var name = Name;
            if (Authors.Length != 0)
            {
                name += " - ";
                for (var i = 0; i < Authors.Length; i++)
                {
                    name += Authors[i];
                    if (i != Authors.Length - 1)
                    {
                        name += ", ";
                    }
                }
            }

            return name;
        }
    }
}
