namespace Library
{
    public sealed class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return FirstName.Substring(0, 1).ToUpper() + ". " + LastName;
        }
    }
}
