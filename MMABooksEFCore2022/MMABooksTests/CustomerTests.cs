using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        
        MMABooksContext dbContext;
        Customer? c;
        List<Customer>? customers;

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer1Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer2Data()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer3Data()");
        }

        [Test]
        public void GetAllTest()
        {
            customers = dbContext.Customers.OrderBy(c => c.CustomerId).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual(1, customers[0].CustomerId);
            PrintAll(customers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            c = dbContext.Customers.Find(20);
            Assert.IsNotNull(c);
            Assert.AreEqual("30340", c.ZipCode);
            Console.WriteLine(c);
           
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the customers who live in OR
            customers = dbContext.Customers.Where(c => c.StateCode == "OR" ).OrderBy(c => c.StateCode).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual("OR", customers[0].StateCode);
            PrintAll(customers);
        }

        [Test]
        public void GetWithInvoicesTest()
        {
            // get the customer whose id is 20 and all of the invoices for that customer
            c = dbContext.Customers.Include("Invoices").Where(c => c.CustomerId == 20).SingleOrDefault();
            Assert.IsNotNull(c);
            Assert.AreEqual("30340", c.ZipCode);
            Assert.AreEqual(3, c.Invoices.Count);
            Console.WriteLine(c);
        }

        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.StateCode,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.StateCode, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        public void DeleteTest()
        {
            c = dbContext.Customers.Find(10);
            dbContext.Customers.Remove(c); 
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Customers.Find(10));
        }

        [Test]
        public void CreateTest()
        {
            c = new Customer();
            c.CustomerId = 700;
            c.Name = "test";
            c.Address = "test address";
            c.City = "test city";
            c.StateCode = "OR";
            c.ZipCode = "11111";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();
            Console.WriteLine(c);
            Assert.AreEqual("test", c.Name);
        }

        [Test]
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(1);
            c.Name = "changed name";
            dbContext.Customers.Update(c);
            dbContext.SaveChanges();
            Assert.AreEqual("changed name", c.Name);
            Console.WriteLine(c);
        }

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }
        
    }
}