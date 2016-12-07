using System;

namespace Library
{
    public abstract class PrintedProduct : Document
    {
        public string PlaceOfPublication { get; }
        public string Publisher { get; }

        protected PrintedProduct(string name, int pagesCount, string placeOfPublication,
            string publisher, DateTime publicationDate) :
            base(name, pagesCount, publicationDate)
        {
            if (string.IsNullOrWhiteSpace(publisher))
            {
                throw new ArgumentException("Publisher shouldn't be empty", "publisher");
            }

            if (publicationDate.Year < 1900)
            {
                throw new ArgumentOutOfRangeException("publicationDate", "Publication date should be greater than 1899 year");
            }

            PlaceOfPublication = placeOfPublication;
            Publisher = publisher;
        }

        public override bool Equals(object obj)
        {
            var printedProduct = obj as PrintedProduct;
            if (printedProduct == null)
            {
                return false;
            }

            return base.Equals(obj) && PlaceOfPublication == printedProduct.PlaceOfPublication
                && Publisher == printedProduct.Publisher;
        }
    }
}
