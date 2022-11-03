using NUnit.Framework;

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
    class ProductDBTests
    {
        public class StateDBTests
        {
            ProductDB db;

            [SetUp]
            public void ResetData()
            {
                db = new ProductDB();
                DBCommand command = new DBCommand();
                command.CommandText = "usp_testingResetProductData";
                command.CommandType = CommandType.StoredProcedure;
                db.RunNonQueryProcedure(command);
            }

            [Test]
            public void TestRetrieve()
            {
                ProductProps p = (ProductProps)db.Retrieve(6);
                Assert.AreEqual(6, p.ProductID);
                Assert.AreEqual("CS10", p.ProductCode);
                Assert.AreEqual("Murach's C# 2010", p.Description);
            }

            [Test]
            public void TestRetrieveAll()
            {
                List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
                Assert.AreEqual(16, list.Count);
            }
            [Test]
            public void TestUpdate()
            {
                ProductProps p = (ProductProps)db.Retrieve(6);
                p.ProductCode = "CS10";
                Assert.True(db.Update(p));
                p = (ProductProps)db.Retrieve(6);
                Assert.AreEqual("CS10", p.ProductCode);
            }
            [Test]
            public void TestCreate()
            {
                ProductProps p = new ProductProps();
                p.ProductCode = "AB12";
                p.Description = "Test Desc";
                p.UnitPrice = 10.1;
                p.OnHandQuantity = 1001;
                p = (ProductProps)db.Create(p);
                ProductProps p2 = (ProductProps)db.Retrieve(p.ProductID);
                Assert.AreEqual(p.GetState(), p2.GetState());
            }

            [Test]
            public void TestDelete()
            {
                ProductProps p = (ProductProps)db.Retrieve(1);
                Assert.True(db.Delete(p));
                Assert.Throws<Exception>(() => db.Retrieve(1));
            }
        }
    }
}
