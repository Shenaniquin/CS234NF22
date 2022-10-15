using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer def;
        private Customer c;
        // this method gets called BEFORE EVERY TEST to recreate the Customer object
        // so that every test gets a "fresh" state and the results of one test
        // don't impact the results of the next
        [SetUp]
        public void SetUp()
        {
            def = new Customer();
            c = new Customer(1, "Mouse, Mickey", "101 Main Street", "Orlando", "FL", "10001");
        }
        [Test]
        public void TestDefaultConstructor()
        {
            Assert.IsNotNull(def);
            Assert.AreEqual(null, def.Name);
            Assert.AreEqual(null, def.Address);
            Assert.AreEqual(null, def.City);
            Assert.AreEqual(null, def.State);
            Assert.AreEqual(null, def.ZipCode);
        }
        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(c);
            Assert.AreNotEqual(null, c.Name);
            Assert.AreEqual("Mouse, Mickey", c.Name);
            Assert.AreNotEqual(null, c.Address);
            Assert.AreEqual("101 Main Street", c.Address);
            Assert.AreNotEqual(null, c.City);
            Assert.AreEqual("Orlando", c.City);
            Assert.AreNotEqual(null, c.State);
            Assert.AreEqual("FL", c.State);
            Assert.AreNotEqual(null, c.ZipCode);
            Assert.AreEqual("10001", c.ZipCode);
        }
        [Test]
        public void TestNameSetter()
        {
            c.Name = "Mouse, Minnie";
            Assert.AreNotEqual("Mouse, Mickey", c.Name);
            Assert.AreEqual("Mouse, Minnie", c.Name);
        }
        [Test]
        public void TestSettersNameTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Name = "      ");
        }
        [Test]
        public void TestSettersNameTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Name = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }
        [Test]
        public void TestAddressSetter()
        {
            c.Address = "100 Main Street";
            Assert.AreNotEqual("101 Main Street", c.Address);
            Assert.AreEqual("100 Main Street", c.Address);
        }
        [Test]
        public void TestSettersAddressTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Address = "      ");
        }
        [Test]
        public void TestSettersAddressTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Address = "123456789012345678901234512345678901234567890123456");
        }
        [Test]
        public void TestCitySetter()
        {
            c.City = "Portland";
            Assert.AreNotEqual("Orlando", c.City);
            Assert.AreEqual("Portland", c.City);
        }
        [Test]
        public void TestSettersCityTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.City = "      ");
        }
        [Test]
        public void TestSettersCityTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.City = "123456789012345678901");
        }
        [Test]
        public void TestStateSetter()
        {
            c.State = "OR";
            Assert.AreNotEqual("FL", c.State);
            Assert.AreEqual("OR", c.State);
        }
        [Test]
        public void TestSettersStateTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.State = "      ");
        }
        [Test]
        public void TestSettersStateTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.State = "123");
        }
        [Test]
        public void TestZipCodeSetter()
        {
            c.ZipCode = "94444";
            Assert.AreNotEqual("10001", c.ZipCode);
            Assert.AreEqual("94444", c.ZipCode);
        }
        [Test]
        public void TestSettersZipCodeTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.ZipCode = "      ");
        }
        [Test]
        public void TestSettersZipcodeTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => c.ZipCode = "1234567890123456");
        }
    }
}
