using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerDBTests
    {

        [Test]
        public void TestGetCustomer()
        {
            Customer c = CustomerDB.GetCustomer(1);
            Assert.AreEqual(1, c.CustomerID);
        }

        [Test]
        public void TestCreateCustomer()
        {
            Customer c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            int customerID = CustomerDB.AddCustomer(c);
            c = CustomerDB.GetCustomer(customerID);
            Assert.AreEqual("Mickey Mouse", c.Name);
        }
        [Test]
        public void TestDeleteCustomer()
        {
            Customer c = new Customer();
            c.Name = "Mickey Mouse";
            c.Address = "101 Main Street";
            c.City = "Orlando";
            c.State = "FL";
            c.ZipCode = "10101";

            bool custDelete = CustomerDB.DeleteCustomer(c);
            Assert.AreEqual(custDelete, true);
        }
        [Test]
        public void TestUpdateCustomer()
        {
            Customer c = new Customer();
            c.Name = "Minnie Mouse";
            c.Address = "100 Main Street";
            c.City = "Savanna";
            c.State = "GA";
            c.ZipCode = "10001";
            Customer oldC = new Customer();
            oldC.Name = "Mickey Mouse";
            oldC.Address = "101 Main Street";
            oldC.City = "Orlando";
            oldC.State = "FL";
            oldC.ZipCode = "10101";

            bool custUpdate = CustomerDB.UpdateCustomer(oldC,c);
            Assert.AreEqual(custUpdate, true);
        }
    }
}
