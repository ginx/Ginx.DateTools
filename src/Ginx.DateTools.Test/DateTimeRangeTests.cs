namespace Ginx.DateTools.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeRangeTests
    {
        [TestMethod]
        public void AnUnitializedObjectShouldHaveMinValue()
        {
            DateTimeRange r = default(DateTimeRange);
            Assert.AreEqual(DateTime.MinValue, r.Start);
            Assert.AreEqual(DateTime.MinValue, r.End);
        }

        [TestMethod]
        public void ConstructorShouldSetStartAndEnd1()
        {
            var start = new DateTime(1984, 9, 1);
            var end = new DateTime(1984, 9, 30);
            var range = new DateTimeRange(start, end);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }

        [TestMethod]
        public void ConstructorShouldSetStartAndEnd2()
        {
            var start = new DateTime(1984, 9, 1);
            var end = new DateTime(1984, 9, 30);
            var range = new DateTimeRange(1984, 9);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }

        [TestMethod]
        public void ConstructorShouldSetStartAndEnd3()
        {
            var start = new DateTime(1984, 1, 1);
            var end = new DateTime(1984, 12, 31);
            var range = new DateTimeRange(1984);

            Assert.AreEqual(start, range.Start);
            Assert.AreEqual(end, range.End.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorShouldNotAcceptInvalidMonth1()
        {
            var range = new DateTimeRange(1984, 19);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorShouldNotAcceptInvalidMonth2()
        {
            var range = new DateTimeRange(1984, -9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorShouldNotAcceptEndBeforeStart()
        {
            var start = new DateTime(1984, 9, 1);
            var end = new DateTime(1984, 9, 30);
            var range = new DateTimeRange(end, start);
        }
        
        [TestMethod]
        public void ContainsShouldWork()
        {
            // TODO: Refactor in smaller tests
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
        public void DurationShouldReturnTheCorrectTimespan()
        {
            var bestDayInHistory = new DateTime(1984, 9, 13);
            var bestMonthInHistory = new DateTimeRange(1984, 9);

            Assert.AreEqual(bestMonthInHistory.End - bestMonthInHistory.Start, bestMonthInHistory.Duration);
        }

        [TestMethod]
        public void EnumarebleShouldEnumerateAllPartsInRange()
        {
            var r = new DateTimeRange(1984, 9);
            Assert.AreEqual(30, r.ToEnumerable(DatePart.Day).Count());
        }

        [TestMethod]
        public void EqualShouldMatchTheSameObject()
        {
            var r = new DateTimeRange(1984, 9);
            Assert.IsTrue(r.Equals(r));
        }

        [TestMethod]
        public void EqualShouldMatchTwoEqualObjects()
        {
            var r1 = new DateTimeRange(1984, 9);
            var r2 = new DateTimeRange(1984, 9);
            Assert.IsTrue(r1.Equals(r2));
        }

        [TestMethod]
        public void EqualShouldNotMatchTwoNotEqualObjects1()
        {
            var r1 = new DateTimeRange(1984, 9);
            var r2 = new DateTimeRange(1984, 10);
            Assert.IsFalse(r1.Equals(r2));
        }

        [TestMethod]
        public void EqualShouldNotMatchTwoNotEqualObjects2()
        {
            var start = new DateTime(1984, 9, 1);
            var end1 = new DateTime(1984, 9, 30);
            var end2 = new DateTime(1984, 10, 30);
            var r1 = new DateTimeRange(start, end1);
            var r2 = new DateTimeRange(start, end2);
            Assert.IsFalse(r1.Equals(r2));
        }

        [TestMethod]
        public void MinValueShouldHaveMinDateTimeAsStartAndEnd()
        {
            var r = DateTimeRange.MinValue;
            Assert.AreEqual(DateTime.MinValue, r.Start);
            Assert.AreEqual(DateTime.MinValue, r.End);
        }

        [TestMethod]
        public void MaxValueShouldHaveMinDateTimeAsStartAndMaxDateTimeAsEnd()
        {
            var r = DateTimeRange.MaxValue;
            Assert.AreEqual(DateTime.MinValue, r.Start);
            Assert.AreEqual(DateTime.MaxValue, r.End);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToEnumerableShouldNotAllowZeroStep()
        {
            var r = new DateTimeRange(1984, 9);
            r.ToEnumerable(DatePart.Day, 0).ToArray();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToEnumerableShouldNotAllowNegativeStep()
        {
            var r = new DateTimeRange(1984, 9);
            r.ToEnumerable(DatePart.Day, -1).ToArray();
        }

        [TestMethod]
        public void ToStringShouldReturnTheRangeAsString()
        {
            var r = new DateTimeRange(1984, 9);
            Assert.AreEqual(string.Format("{0} to {1}", r.Start, r.End), r.ToString());
            Assert.AreEqual(string.Format("{0} to {1}", r.Start.ToString("dd/MM/yyyy"), r.End.ToString("dd/MM/yyyy")), r.ToString("dd/MM/yyyy"));
        }
    }
}
