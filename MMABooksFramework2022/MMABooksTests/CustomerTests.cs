using NUnit.Framework;

using MMABooksBusiness;
using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    class CustomerTests
    {
        [SetUp]
        public void TestResetDatabase()
        {
            CustomerDB db = new CustomerDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetCustomer1Data";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }
        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Customer c = new Customer(1);
            Assert.AreEqual("Molunguri, A", c.Name);
            Assert.AreEqual("1108 Johanna Bay Drive", c.Address);
            Assert.IsFalse(c.IsNew);
            Assert.IsTrue(c.IsValid);
        }

        [Test]
        public void TestNewCustomerConstructor()
        {
            // not in Data Store - no code
            Customer c = new Customer();
            Assert.AreEqual(string.Empty, c.Name);
            Assert.AreEqual(string.Empty, c.Address);
            Assert.IsTrue(c.IsNew);
            Assert.IsFalse(c.IsValid);
        }
        [Test]
        public void TestSaveToDataStore()
        {
            Customer c = new Customer();
            c.Name = "Minnie Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10001";
            c.Save();
            Customer c2 = new Customer(c.CustomerID);
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }
        [Test]
        public void TestUpdate()
        {
            Customer c = new Customer();
            c.Name = "Minnie Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10001";
            c.Save();

            Customer c2 = new Customer(c.CustomerID);
            Assert.AreEqual(c2.CustomerID, c.CustomerID);
            Assert.AreEqual(c2.Name, c.Name);
        }
        [Test]
        public void TestDelete()
        {
            Customer c = new Customer(1);
            c.Delete();
            c.Save();
            Assert.Throws<Exception>(() => new Customer(1));
        }

        [Test]
        public void TestGetList()
        {
            Customer c = new Customer();
            List<Customer> customers = (List<Customer>)c.GetList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual("Molunguri, A", customers[0].Name);
            Assert.AreEqual("1108 Johanna Bay Drive", customers[0].Address);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {

            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Customer c = new Customer();
            Assert.Throws<Exception>(() => c.Save());
            c.Name = "Minnie Mouse";
            Assert.Throws<Exception>(() => c.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Customer c = new Customer();
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Name = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Customer c1 = new Customer(1);
            Customer c2 = new Customer(1);

            c1.Name = "Updated first";
            c1.Save();

            c2.Name = "Updated second";
            Assert.Throws<Exception>(() => c2.Save());
        }

    }
}
