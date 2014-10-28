using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ginx.DateTools.Test
{
    [TestClass]
    public class DateTimeBuilderTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Year;

            Assert.AreEqual(new DateTime(1984, 1, 1), builder.Begin().Date);
            Assert.AreEqual(new DateTime(1984, 1, 1), builder.Begin().Date);
        }
    }
}
