using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library
{
    public sealed class Book : PrintedProduct
    {
        private string isbn;

        public List<Person> Authors { get; set; }

        public string ISBN
        {
            get { return isbn; }
            set
            {
                if (IsISBNCorrect(value))
                {
                    isbn = value;
                }
                else
                {
                    throw new ArgumentException("Invalid ISBN", "isbn");
                }
            }
        }

        private Book() { }

        public Book(string name, int pagesCount, string placeOfPublication, string publisher,
            string isbn, IEnumerable<Person> authors, DateTime publicationDate) :
            base(name, pagesCount, placeOfPublication, publisher, publicationDate)
        {
            if (authors == null || authors.Count() == 0)
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

            Authors = authors.ToList();
            ISBN = isbn;
        }

        private bool IsISBNCorrect(string isbn)
        {
            var isbnDigitString = Regex.Replace(isbn, @"[^\d]", string.Empty);

            return Regex.IsMatch(isbn, @"^ISBN 978-\d{1,5}-\d{1,7}-\d{1,7}-\d$")
                && isbnDigitString.Length == 13
                && isCheckSumCorrect(isbnDigitString);
        }

        private bool isCheckSumCorrect(string isbnDigitString)
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
            for (var i = 0; i < digits.Length - 1; i += 2)
            {
                oddsSum += digits[i];
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
            var areEquals = base.Equals(obj);

            var book = obj as Book;
            areEquals = areEquals && ISBN == book.ISBN;

            if (Authors.Count != book?.Authors.Count)
            {
                return false;
            }
            for (var i = 0; i < Authors.Count; i++)
            {
                areEquals = areEquals && Authors[i].Equals(book.Authors[i]);
            }

            return areEquals;
        }
    }
}
