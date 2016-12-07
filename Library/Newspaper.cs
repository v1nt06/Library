using System;

namespace Library
{
    public sealed class Newspaper : PrintedProduct
    {
        public int Number { get; }
        public string ISSN { get; }

        public Newspaper(string name, int pagesCount, string placeOfPublication, string publisher,
            string issn, int number, DateTime pulicationDate) : 
            base(name, pagesCount, placeOfPublication, publisher, pulicationDate)
        {
            if (number < 1)
            {
                throw new ArgumentException("Number should be positive", "number");
            }

            Number = number;
            ISSN = issn;
        }

        public override string ToString()
        {
            return $"{Name} №{Number}";
        }

        public override bool Equals(object obj)
        {
            var newspaper = obj as Newspaper;
            if (newspaper == null)
            {
                return false;
            }

            return base.Equals(obj) && Number == newspaper.Number && ISSN == newspaper.ISSN;
        }
    }
}
