using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ginx.DateTools.Test
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void NextDayOfWeekTest()
        {
            var d = new DateTime(1984, 9, 13); // A happy thursday

            Assert.AreEqual(new DateTime(1984, 9, 16), d.Next(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(1984, 9, 17), d.Next(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(1984, 9, 18), d.Next(DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(1984, 9, 19), d.Next(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(1984, 9, 20), d.Next(DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(1984, 9, 14), d.Next(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(1984, 9, 15), d.Next(DayOfWeek.Saturday));
        }

        [TestMethod]
        public void PreviousDayOfWeekTest()
        {
            var d = new DateTime(1984, 9, 13); // A happy thursday

            Assert.AreEqual(new DateTime(1984, 9, 9), d.Previous(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(1984, 9, 10), d.Previous(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(1984, 9, 11), d.Previous(DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(1984, 9, 12), d.Previous(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(1984, 9, 6), d.Previous(DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(1984, 9, 7), d.Previous(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(1984, 9, 8), d.Previous(DayOfWeek.Saturday));
        }

        [TestMethod]
        public void InThisWeekTest()
        {
            var d = new DateTime(1984, 9, 13); // A happy thursday
                       
            Assert.AreEqual(new DateTime(1984, 9, 9), d.InThisWeek(DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(1984, 9, 10), d.InThisWeek(DayOfWeek.Monday));
            Assert.AreEqual(new DateTime(1984, 9, 11), d.InThisWeek(DayOfWeek.Tuesday));
            Assert.AreEqual(new DateTime(1984, 9, 12), d.InThisWeek(DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(1984, 9, 13), d.InThisWeek(DayOfWeek.Thursday));
            Assert.AreEqual(new DateTime(1984, 9, 14), d.InThisWeek(DayOfWeek.Friday));
            Assert.AreEqual(new DateTime(1984, 9, 15), d.InThisWeek(DayOfWeek.Saturday));
        }
    }
}
