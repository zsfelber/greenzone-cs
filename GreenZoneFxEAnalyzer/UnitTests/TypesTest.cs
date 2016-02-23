using GreenZoneFxEngine.Trading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for Mt4FileUtilTest and is intended
    ///to contain all Mt4FileUtilTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TypesTest : TradingConst
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void datetime()
        {
            DateTime d0;
            do
            {
                d0 = DateTime.Now;
            } while (d0.Millisecond == 0);

            datetime d1 = (datetime)d0;
            long l1 = (long)d1;
            datetime d2 = (datetime)l1;
            int i2 = (int)d2;
            datetime d3 = (datetime)i2;

            Assert.AreEqual(d0, d1, "d0 eq d1");
            Assert.AreEqual(d1, d0, "d1 eq d0");
            Assert.AreEqual(d1, d2, "d1 eq d2");
            Assert.AreEqual(d2, d1, "d2 eq d1");
            Assert.IsTrue(d0 == d1, "d0 == d1");
            Assert.IsTrue(d1 == d0, "d1 == d0");
            Assert.IsTrue(d1 == d2, "d1 == d2");
            Assert.IsTrue(d2 == d1, "d2 == d1");
            Assert.IsFalse(d0 != d1, "d0 != d1");
            Assert.IsFalse(d1 != d0, "d1 != d0");
            Assert.IsFalse(d1 != d2, "d1 != d2");
            Assert.IsFalse(d2 != d1, "d2 != d1");
            Assert.AreNotEqual(d0.Millisecond, 0, "d0.Millisecond == 0");
            Assert.AreEqual(d0.Millisecond, d1.Millisecond, "d0.Millisecond == d1.Millisecond");
            Assert.AreEqual(d1.Millisecond, d2.Millisecond, "d1.Millisecond == d2.Millisecond");
            Assert.AreEqual(d3.Millisecond, 0, "d3.Millisecond == 0");
            Assert.AreNotEqual(d2, d3, "d2 neq d3");
            Assert.IsFalse(d2 == d3, "d2 == d3");
            Assert.IsTrue(d2 != d3, "d2 != d3");
            Assert.IsTrue(d3 < d2, "d3 < d2");
            Assert.IsTrue(d3 <= d2, "d3 <= d2");
            Assert.IsTrue(d2 > d3, "d0 > d1");
            Assert.IsTrue(d2 >= d3, "d0 >= d1");
            Assert.IsFalse(d3 > d2, "d3 > d2");
            Assert.IsFalse(d3 >= d2, "d3 >= d2");
            Assert.IsFalse(d2 < d3, "d0 < d1");
            Assert.IsFalse(d2 <= d3, "d0 <= d1");
        }

    }
}
