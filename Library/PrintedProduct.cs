using System;

namespace Library
{
    public abstract class PrintedProduct : Document
    {
        public string PlaceOfPublication { get; }
        public string Publisher { get; }
        public int YearOfPublishing => PublicationDate.Year;
        

        protected PrintedProduct(string name, int pagesCount, string placeOfPublication,
            string publisher, DateTime publicationDate) :
            base(name, pagesCount, publicationDate)
        {
            PlaceOfPublication = placeOfPublication;
            Publisher = publisher;
        }
    }
}
