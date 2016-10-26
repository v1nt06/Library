using System;

namespace Library
{
    public sealed class Newspaper : PrintedProduct
    {
        public int Number { get; }
        public DateTime Date { get; set; }
        public string ISSN { get; }

        public Newspaper(string name, int pagesCount, string placeOfPublication, string publisher,
            string issn, int number, DateTime pulicationDate) : 
            base(name, pagesCount, placeOfPublication, publisher,pulicationDate)
        {
            Number = number;
            ISSN = issn;
        }
    }
}
