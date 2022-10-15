using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;
namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        private Product def;
        private Product prod;
        // this method gets called BEFORE EVERY TEST to recreate the Customer object
        // so that every test gets a "fresh" state and the results of one test
        // don't impact the results of the next
        [SetUp]
        public void SetUp()
        {
            def = new Product();
            prod = new Product("CS10", "Murach''s C# 2010", 56.50m, 5136);
        }
        [Test]
        public void TestDefaultConstructor()
        {
            Assert.IsNotNull(def);
            Assert.AreEqual(null, def.ProductCode);
            Assert.AreEqual(null, def.Description);
            Assert.AreEqual(0.0m, def.UnitPrice);
            Assert.AreEqual(0, def.OnHandQuantity);
        }
        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(prod);
            Assert.AreNotEqual(null, prod.ProductCode);
            Assert.AreEqual("CS10", prod.ProductCode);
            Assert.AreNotEqual(null, prod.Description);
            Assert.AreEqual("Murach''s C# 2010", prod.Description);
            Assert.AreNotEqual(null, prod.UnitPrice);
            Assert.AreEqual(56.50, prod.UnitPrice);
            Assert.AreNotEqual(null, prod.OnHandQuantity);
            Assert.AreEqual(5136, prod.OnHandQuantity);
        }
        [Test]
        public void TestProductCodeSetter()
        {
            prod.ProductCode = "CRFC";
            Assert.AreNotEqual("CS10", prod.ProductCode);
            Assert.AreEqual("CRFC", prod.ProductCode);
        }
        [Test]
        public void TestSettersProductCodeTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.ProductCode = "      ");
        }
        [Test]
        public void TestSettersProductCodeTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.ProductCode = "123456789012");
        }
        [Test]
        public void TestDescriptionSetter()
        {
            prod.Description = "Murach''s CICS Desk Reference";
            Assert.AreNotEqual("Murach''s C# 2010", prod.Description);
            Assert.AreEqual("Murach''s CICS Desk Reference", prod.Description);
        }
        [Test]
        public void TestSettersDescriptionTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.Description = "      ");
        }
        [Test]
        public void TestSettersDescriptionTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.Description = "123456789012345678901234512345678901234567890123456");
        }
        [Test]
        public void TestUnitPriceSetter()
        {
            prod.UnitPrice = 50.00m;
            Assert.AreNotEqual(56.50m, prod.UnitPrice);
            Assert.AreEqual(50.00m, prod.UnitPrice);
        }
        [Test]
        public void TestSettersUnitPriceTooShort()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.UnitPrice = 00.00m);
        }
        [Test]
        public void TestSettersUnitPriceNagative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.UnitPrice = -1.11m);
        }
        [Test]
        public void TestOnHandQuantitySetter()
        {
            prod.OnHandQuantity = 1865;
            Assert.AreNotEqual(5136, prod.OnHandQuantity);
            Assert.AreEqual(1865, prod.OnHandQuantity);
        }
        [Test]
        public void TestSettersOnHandQuantityneagtive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => prod.OnHandQuantity = -2);
        }
    }
}
