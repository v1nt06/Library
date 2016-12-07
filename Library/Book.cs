using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public sealed class Book : PrintedProduct
    {
        public IList<Person> Authors { get; }
        public string ISBN { get; }

        public Book(string name, int pagesCount, string placeOfPublication, string publisher,
            string isbn, IList<Person> authors, DateTime publicationDate) :
            base(name, pagesCount, placeOfPublication, publisher, publicationDate)
        {
            if (authors == null || authors.Count == 0)
            {
                throw new ArgumentException("Authors should contains at least one person", "authors");
            }

            if (string.IsNullOrWhiteSpace(placeOfPublication))
            {
                throw new ArgumentException("Place of publication shouldn't be empty", "placeOfPublication");
            }

            if (authors.Any(author => author == null))
            {
                throw new ArgumentException("Author shouldn't be null", "author");
            }

            Authors = authors;
            ISBN = isbn;
        }

        public override string ToString()
        {
            var name = Name;
            if (Authors.Count > 0)
            {
                name += " - " + string.Join(", ", Authors);
            }
            return name;
        }

        public override bool Equals(object obj)
        {
            var book = obj as Book;

            if (Authors.Count != book?.Authors.Count)
            {
                return false;
            }

            var areEqual = base.Equals(obj) && ISBN == book.ISBN;

            for (var i = 0; i < Authors.Count; i++)
            {
                areEqual = areEqual && Authors[i].Equals(book.Authors[i]);
            }

            return areEqual;
        }
    }
}
