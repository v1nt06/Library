using System;

namespace Library
{
    public sealed class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private Person() { }

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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var person = (Person)obj;
            return FirstName == person.FirstName && LastName == person.LastName;
        }
    }
}
