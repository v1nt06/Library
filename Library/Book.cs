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
