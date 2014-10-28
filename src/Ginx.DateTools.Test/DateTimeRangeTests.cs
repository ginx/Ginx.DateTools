namespace Ginx.DateTools.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeRangeTests
    {
        [TestMethod]
        public void DateRangeStartEndConstructorTest()
        {
            var start = new DateTime(1984, 9, 1);
            var end = new DateTime(1984, 9, 30);
            var range = new DateTimeRange(start, end);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }

        [TestMethod]
        public void DateRangeYearMonthConstructorTest()
        {
            var start = new DateTime(1984, 9, 1);
            var end = new DateTime(1984, 9, 30);
            var range = new DateTimeRange(1984, 9);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }

        [TestMethod]
        public void DateRangeYearConstructorTest()
        {
            var start = new DateTime(1984, 1, 1);
            var end = new DateTime(1984, 12, 31);
            var range = new DateTimeRange(1984);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }
        
        [TestMethod]
        public void DateRangeContainsTest()
        {
            var bestDayInHistory = new DateTime(1984, 9, 13);
            var bestMonthInHistory = new DateTimeRange(1984, 9);
            var bestYearInHistory = new DateTimeRange(1984);
            var worstDayInHistory = new DateTime(1914, 6, 28);
            var worstMonthInHistory = new DateTimeRange(1914, 9);
            var worstYearInHistory = new DateTimeRange(1914);

            Assert.IsTrue(bestMonthInHistory.Contains(bestDayInHistory));
            Assert.IsFalse(bestMonthInHistory.Contains(worstDayInHistory));

            Assert.IsFalse(bestMonthInHistory.Contains(bestDayInHistory, worstDayInHistory));

            Assert.IsTrue(bestMonthInHistory.Contains(bestMonthInHistory));
            Assert.IsFalse(bestMonthInHistory.Contains(worstMonthInHistory));

            Assert.IsTrue(bestYearInHistory.Contains(bestDayInHistory));
            Assert.IsFalse(bestYearInHistory.Contains(worstDayInHistory));

            Assert.IsFalse(bestYearInHistory.Contains(bestDayInHistory, worstDayInHistory));

            Assert.IsTrue(bestYearInHistory.Contains(bestMonthInHistory));
            Assert.IsTrue(bestYearInHistory.Contains(bestYearInHistory));
        }

        [TestMethod]
        public void DateRangeTimespanTest()
        {
            var bestDayInHistory = new DateTime(1984, 9, 13);
            var bestMonthInHistory = new DateTimeRange(1984, 9);

            Assert.AreEqual(bestMonthInHistory.End - bestMonthInHistory.Start, bestMonthInHistory.Duration);
        }

        [TestMethod]
        public void DateRangeEnumerableTest()
        {
            var r = new DateTimeRange(1984, 9);
            r.ToEnumerable(DatePart.Day).Where(d => d.DayOfWeek == DayOfWeek.Friday).ToList();
        }
    }
}
