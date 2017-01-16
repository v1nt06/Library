using System;
using System.Text.RegularExpressions;

namespace Library
{
    public sealed class Newspaper : PrintedProduct
    {
        public int Number { get; set; }
        public string ISSN { get; set; }

        private Newspaper() { }

        public Newspaper(string name, int pagesCount, string placeOfPublication, string publisher,
            string issn, int number, DateTime pulicationDate) :
            base(name, pagesCount, placeOfPublication, publisher, pulicationDate)
        {
            if (number < 1)
            {
                throw new ArgumentException("Number should be positive", "number");
            }

            if (!IsISSNCorrect(issn))
            {
                throw new ArgumentException("Invalid ISSN", "issn");
            }

            Number = number;
            ISSN = issn;
        }

        private bool IsISSNCorrect(string issn)
        {
            var issnDigitString = Regex.Replace(issn, @"[^\d]", string.Empty);

            return Regex.IsMatch(issn, @"^ISSN \d{4}-\d{4}$")
                   && issnDigitString.Length == 8
                   && isCheckSumCorrect(issnDigitString);
        }

        private bool isCheckSumCorrect(string issnDigitString)
        {
            var digits = new int[8];
            for (var i = 0; i < digits.Length; i++)
            {
                digits[i] = issnDigitString[i] - 0x30;
            }

            var mod = (digits[0] * 8 + digits[1] * 7 + digits[2] * 6 + digits[3] * 5 + digits[4] * 4 + digits[5] * 3 + digits[6] * 2) % 11;
            if (mod == 0)
            {
                return digits[7] == mod;
            }

            return digits[7] == 11 - mod;
        }

        public override string ToString()
        {
            return $"{Name} №{Number}";
        }

        public override bool Equals(object obj)
        {
            var areEquals = base.Equals(obj);
            var newspaper = obj as Newspaper;
            if (newspaper == null)
            {
                return false;
            }

            return areEquals && Number == newspaper.Number && ISSN == newspaper.ISSN;
        }
    }
}
