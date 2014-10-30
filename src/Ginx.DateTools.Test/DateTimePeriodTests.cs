using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ginx.DateTools.Test
{
    [TestClass]
    public class DateTimePeriodTests
    {
        [TestMethod]
        public void DayShouldReturnABuilderForDay()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Day;

            Assert.AreEqual(TimeSpan.FromDays(1).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void WeekShouldReturnABuilderForWeek()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Week;

            Assert.AreEqual(TimeSpan.FromDays(7).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void HalfMonthShouldReturnABuilderForHalfMonth()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().HalfMonth;

            Assert.AreEqual(TimeSpan.FromDays(15).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void MonthShouldReturnABuilderForMonth()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Month;

            Assert.AreEqual(TimeSpan.FromDays(30).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void QuarterShouldReturnABuilderForQuarter()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Quarter;

            Assert.AreEqual(TimeSpan.FromDays(92).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void SemesterShouldReturnABuilderForSemester()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Semester;

            Assert.AreEqual(TimeSpan.FromDays(184).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }

        [TestMethod]
        public void YearShouldReturnABuilderForYear()
        {
            var d = new DateTime(1984, 9, 13);
            var builder = d.Current().Year;

            Assert.AreEqual(TimeSpan.FromDays(366).Subtract(TimeSpan.FromTicks(1)), builder.Range.Duration);
        }
    }
}
