﻿using NUnit.Framework;

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
    class CustomerDBTests
    {
        public class StateDBTests
        {
            CustomerDB db;

            [SetUp]
            public void ResetData()
            {
                db = new CustomerDB();
                DBCommand command = new DBCommand();
                command.CommandText = "usp_testingResetCustomer1Data";
                command.CommandType = CommandType.StoredProcedure;
                db.RunNonQueryProcedure(command);
            }

            [Test]
            public void TestRetrieve()
            {
                CustomerProps p = (CustomerProps)db.Retrieve(1);
                Assert.AreEqual(1, p.CustomerID);
                Assert.AreEqual("Molunguri, A", p.Name);
                Assert.AreEqual("1108 Johanna Bay Drive", p.Address);
            }

            [Test]
            public void TestRetrieveAll()
            {
                List<CustomerProps> list = (List<CustomerProps>)db.RetrieveAll();
                Assert.AreEqual(696, list.Count);
            }
            [Test]
            public void TestUpdate()
            {
                CustomerProps p = (CustomerProps)db.Retrieve(1);
                p.Name = "Molunguri, A";
                Assert.True(db.Update(p));
                p = (CustomerProps)db.Retrieve(1);
                Assert.AreEqual("Molunguri, A", p.Name);
            }
            [Test]
            public void TestCreate()
            {
                CustomerProps p = new CustomerProps();
                p.Name = "Minnie Mouse";
                p.Address = "101 Main Street";
                p.City = "Orlando";
                p.State = "FL";
                p.ZipCode = "10001";
                p = (CustomerProps)db.Create(p);
                CustomerProps p2 = (CustomerProps)db.Retrieve(p.CustomerID);
                Assert.AreEqual(p.GetState(), p2.GetState());
            }
            
            [Test]
            public void TestDelete()
            {
                CustomerProps p = (CustomerProps)db.Retrieve(1);
                Assert.True(db.Delete(p));
                Assert.Throws<Exception>(() => db.Retrieve(1));
            }


        }
    }
}
