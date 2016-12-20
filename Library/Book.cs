using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            if (!IsISBNCorrect(isbn))
            {
                throw new ArgumentException("Invalid ISBN", "isbn");
            }

            Authors = authors;
            ISBN = isbn;
        }

        private bool IsISBNCorrect(string isbn)
        {
            var isbnDigitString = Regex.Replace(isbn, @"[^\d]", string.Empty);
            
            return Regex.IsMatch(isbn, @"^ISBN 978-\d{1,5}-\d{1,7}-\d{1,7}-\d$")
                && isbnDigitString.Length == 13
                && isCheckSumCorrect(isbnDigitString);
        }

        protected bool isCheckSumCorrect(string isbnDigitString)
        {
            var digits = new int[13];
            for (var i = 0; i < digits.Length; i++)
            {
                digits[i] = isbnDigitString[i] - 0x30;
            }

            var evensSum = 0;
            for (var i = 1; i < digits.Length; i += 2)
            {
                evensSum += digits[i];
            }
            evensSum *= 3;

            var oddsSum = 0;
            for (var i = 0; i < digits.Length; i += 2)
            {
                evensSum += digits[i];
            }

            var mod = (evensSum + oddsSum) % 10;

            return digits[12] == (10 - mod != 10 ? 10 - mod : 0);
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
