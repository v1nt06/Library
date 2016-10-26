using System;

namespace Library
{
    public sealed class Book : PrintedProduct
    {
        public Person[] Authors { get; private set; }
        public string ISBN { get; }

        public Book(string name, int pagesCount, string placeOfPublication, string publisher,
            string isbn, Person[] authors, DateTime publicationDate) :
            base(name, pagesCount, placeOfPublication, publisher, publicationDate)
        {
            Authors = authors;
            ISBN = isbn;
        }
    }
}
