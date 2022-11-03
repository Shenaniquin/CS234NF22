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
    class ProductTests
    {
        [SetUp]
        public void TestResetDatabase()
        {
            ProductDB db = new ProductDB();
            DBCommand command = new DBCommand();
            command.CommandText = "usp_testingResetProductData";
            command.CommandType = CommandType.StoredProcedure;
            db.RunNonQueryProcedure(command);
        }
        [Test]
        public void TestRetrieveFromDataStoreContructor()
        {
            // retrieves from Data Store
            Product p = new Product(6);
            Assert.AreEqual("CS10", p.ProductCode);
            Assert.AreEqual("Murach's C# 2010", p.Description);
            Assert.IsFalse(p.IsNew);
            Assert.IsTrue(p.IsValid);
        }

        [Test]
        public void TestNewProductConstructor()
        {
            // not in Data Store - no code
            Product p = new Product();
            Assert.AreEqual(string.Empty, p.ProductCode);
            Assert.AreEqual(string.Empty, p.Description);
            Assert.IsTrue(p.IsNew);
            Assert.IsFalse(p.IsValid);
        }
        [Test]
        public void TestSaveToDataStore()
        {
            Product p = new Product();
            p.ProductCode = "AB12";
            p.Description = "Test Desc";
            p.UnitPrice = 10.1;
            p.OnHandQuantity = 1001;
            p.Save();
            Product p2 = new Product(p.ProductID);
            Assert.AreEqual(p2.ProductID, p.ProductID);
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
        }
        [Test]
        public void TestUpdate()
        {
            Product p = new Product();
            p.ProductCode = "AB12";
            p.Description = "Test Desc";
            p.UnitPrice = 10.1;
            p.OnHandQuantity = 1001;
            p.Save();

            Product p2 = new Product(p.ProductID);
            Assert.AreEqual(p2.ProductID, p.ProductID);
            Assert.AreEqual(p2.ProductCode, p.ProductCode);
        }
        [Test]
        public void TestDelete()
        {
            Product p = new Product(1);
            p.Delete();
            p.Save();
            Assert.Throws<Exception>(() => new Product(1));
        }

        [Test]
        public void TestGetList()
        {
            Product p = new Product();
            List<Product> products = (List<Product>)p.GetList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual("CS10", products[5].ProductCode);
            Assert.AreEqual("Murach's C# 2010", products[5].Description);
        }

        [Test]
        public void TestNoRequiredPropertiesNotSet()
        {

            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestSomeRequiredPropertiesNotSet()
        {
            // not in Data Store - abbreviation and name must be provided
            Product p = new Product();
            Assert.Throws<Exception>(() => p.Save());
            p.ProductCode = "AB12";
            Assert.Throws<Exception>(() => p.Save());
        }

        [Test]
        public void TestInvalidPropertySet()
        {
            Product p = new Product();
            Assert.Throws<ArgumentOutOfRangeException>(() => p.ProductCode = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }

        [Test]
        public void TestConcurrencyIssue()
        {
            Product p1 = new Product(1);
            Product p2 = new Product(1);

            p1.ProductCode = "Updated1st";
            p1.Save();

            p2.ProductCode = "Updated2nd";
            Assert.Throws<Exception>(() => p2.Save());
        }
    }
}
