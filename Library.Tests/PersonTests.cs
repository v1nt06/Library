using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class PersonTests
    {
        private const string FirstName = "Лев";
        private const string LastName = "Толстой";

        [TestMethod]
        public void CreatePerson()
        {
            var person = new Person(FirstName, LastName);

            Assert.AreEqual(FirstName, person.FirstName);
            Assert.AreEqual(LastName, person.LastName);
        }

        [TestMethod]
        public void PersonToString()
        {
            var person = new Person(FirstName, LastName);

            Assert.AreEqual("Л. Толстой", person.ToString());
        }
    }
}
