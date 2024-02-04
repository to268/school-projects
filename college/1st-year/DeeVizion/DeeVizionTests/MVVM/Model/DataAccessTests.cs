using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        /*
        [TestMethod()]
        public void ConnectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetDataTest()
        {
            Assert.Fail();
        }
        */

        [TestMethod()]
        public void EscapeQuotesTest()
        {
            DataAccess access = new DataAccess();
            String test = "test de l'armee";
            Assert.AreEqual("test de l''armee", access.EscapeQuotes(test));
        }
    }
}