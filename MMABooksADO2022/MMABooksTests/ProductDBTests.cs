using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductDBTests
    {
        [Test]
        public void TestGetProduct()
        {
            Product prod = ProductDB.GetProduct("CS10");
            Assert.AreEqual("CS10", prod.ProductCode);
        }

        [Test]
        public void TestCreateProduct()
        {
            Product c = new Product();
            c.ProductCode = "CS10";
            c.Description = "Murach's C# 2010";
            c.UnitPrice = 56.50m;
            c.OnHandQuantity = 5136;

            string productCode = ProductDB.AddProduct(c);
            c = ProductDB.GetProduct(productCode);
            Assert.AreEqual(c, ProductDB.GetProduct(productCode));
        }
        [Test]
        public void TestProductDelete()
        {
            Product c = new Product();
            c.ProductCode = "CS10";
            c.Description = "Murach's C# 2010";
            c.UnitPrice = 56.50m;
            c.OnHandQuantity = 5136;

            bool prodDelete = ProductDB.DeleteProduct(c);
            Assert.AreEqual(prodDelete, true);
        }
        [Test]
        public void TestUpdateProduct()
        {
            Product c = new Product();
            c.ProductCode = "CS10";
            c.Description = "Murach's C# 2010";
            c.UnitPrice = 56.50m;
            c.OnHandQuantity = 5136;
            Product oldC = new Product();
            oldC.ProductCode = "CRFC";
            oldC.Description = "Murach's CICS Desk Reference";
            oldC.UnitPrice = 50.00m;
            oldC.OnHandQuantity = 1865;

            bool prodUpdate = ProductDB.UpdateProduct(oldC, c);
            Assert.AreEqual(prodUpdate, true);
        }
    }
}
