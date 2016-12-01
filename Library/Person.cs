using System;

namespace Library
{
    public sealed class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name shouldn't be empty", "firstName");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name shouldn't be empty", "firstName");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return FirstName.Substring(0, 1).ToUpper() + ". " + LastName;
        }
    }
}
