using NUnit.Framework;

using MMABooksProps;
using System;
using System.Linq;

namespace MMABooksTests
{
    [TestFixture]
    class ProductPropsTests
    {
        ProductProps props;
        [SetUp]
        public void Setup()
        {
            props = new ProductProps();
            props.ProductID = 17;
            props.ProductCode = "Test1";
            props.Description = "Test Desc.";
            props.UnitPrice = 10.1;
            props.OnHandQuantity = 150;
        }

        [Test]
        public void TestGetState()
        {
            string jsonString = props.GetState();
            Console.WriteLine(jsonString);
            Assert.IsTrue(jsonString.Contains(props.ProductCode));
            Assert.IsTrue(jsonString.Contains(props.Description));
        }

        [Test]
        public void TestSetState()
        {
            string jsonString = props.GetState();
            ProductProps newProps = new ProductProps();
            newProps.SetState(jsonString);
            Assert.AreEqual(props.ProductID, newProps.ProductID);
            Assert.AreEqual(props.ProductCode, newProps.ProductCode);
            Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
        }

        [Test]
        public void TestClone()
        {
            ProductProps newProps = (ProductProps)props.Clone();
            Assert.AreEqual(props.ProductID, newProps.ProductID);
            Assert.AreEqual(props.ProductCode, newProps.ProductCode);
            Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
        }
    }
}
