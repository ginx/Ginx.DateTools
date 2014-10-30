using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ginx.DateTools.Test
{
    [TestClass]
    public class DateTimeBuilderTests
    {
        [TestMethod]
        public void NextShouldIncrementOneStep()
        {
            var d = new DateTime(1984, 9, 13);
            Assert.AreEqual(new DateTime(1984, 9, 14), d.Current().Day.Next());
            Assert.AreEqual(new DateTime(1984, 9, 20), d.Current().Week.Next());
            Assert.AreEqual(new DateTime(1984, 9, 28), d.Current().HalfMonth.Next());
            Assert.AreEqual(new DateTime(1984, 10, 13), d.Current().Month.Next());
            Assert.AreEqual(new DateTime(1984, 12, 13), d.Current().Quarter.Next());
            Assert.AreEqual(new DateTime(1985, 3, 13), d.Current().Semester.Next());
            Assert.AreEqual(new DateTime(1985, 9, 13), d.Current().Year.Next());
        }

        [TestMethod]
        public void PreviousShouldDecrementOneStep()
        {
            var d = new DateTime(1984, 9, 13);
            Assert.AreEqual(new DateTime(1984, 9, 12), d.Current().Day.Previous());
            Assert.AreEqual(new DateTime(1984, 9, 6), d.Current().Week.Previous());
            Assert.AreEqual(new DateTime(1984, 8, 28), d.Current().HalfMonth.Previous());
            Assert.AreEqual(new DateTime(1984, 8, 13), d.Current().Month.Previous());
            Assert.AreEqual(new DateTime(1984, 6, 13), d.Current().Quarter.Previous());
            Assert.AreEqual(new DateTime(1984, 3, 13), d.Current().Semester.Previous());
            Assert.AreEqual(new DateTime(1983, 9, 13), d.Current().Year.Previous());
        }

        [TestMethod]
        public void AddForHalfMonthShouldHandleDifferentMonthsLenghtSpecialCase1()
        {
            var d = new DateTime(1984, 8, 31);
            Assert.AreEqual(new DateTime(1984, 6, 30), d.Current().HalfMonth.Add(-4));
            Assert.AreEqual(new DateTime(1984, 7, 15), d.Current().HalfMonth.Add(-3));
            Assert.AreEqual(new DateTime(1984, 7, 31), d.Current().HalfMonth.Add(-2));
            Assert.AreEqual(new DateTime(1984, 8, 15), d.Current().HalfMonth.Add(-1));
            Assert.AreEqual(new DateTime(1984, 8, 31), d.Current().HalfMonth.Add(0));
            Assert.AreEqual(new DateTime(1984, 9, 15), d.Current().HalfMonth.Add(1));
            Assert.AreEqual(new DateTime(1984, 9, 30), d.Current().HalfMonth.Add(2));
            Assert.AreEqual(new DateTime(1984, 10, 15), d.Current().HalfMonth.Add(3));
            Assert.AreEqual(new DateTime(1984, 10, 31), d.Current().HalfMonth.Add(4));
        }

        [TestMethod]
        public void AddForHalfMonthShouldHandleDifferentMonthsLenghtSpecialCase2()
        {
            var d = new DateTime(1984, 2, 29);
            Assert.AreEqual(new DateTime(1983, 12, 29), d.Current().HalfMonth.Add(-4));
            Assert.AreEqual(new DateTime(1984, 1, 14), d.Current().HalfMonth.Add(-3));
            Assert.AreEqual(new DateTime(1984, 1, 29), d.Current().HalfMonth.Add(-2));
            Assert.AreEqual(new DateTime(1984, 2, 14), d.Current().HalfMonth.Add(-1));
            Assert.AreEqual(new DateTime(1984, 2, 29), d.Current().HalfMonth.Add(0));
            Assert.AreEqual(new DateTime(1984, 3, 14), d.Current().HalfMonth.Add(1));
            Assert.AreEqual(new DateTime(1984, 3, 29), d.Current().HalfMonth.Add(2));
            Assert.AreEqual(new DateTime(1984, 4, 14), d.Current().HalfMonth.Add(3));
            Assert.AreEqual(new DateTime(1984, 4, 29), d.Current().HalfMonth.Add(4));
        }

        [TestMethod]
        public void AddForHalfMonthShouldHandleDifferentMonthsLenghtSpecialCase3()
        {
            var d = new DateTime(1984, 3, 5);
            Assert.AreEqual(new DateTime(1984, 1, 5), d.Current().HalfMonth.Add(-4));
            Assert.AreEqual(new DateTime(1984, 1, 20), d.Current().HalfMonth.Add(-3));
            Assert.AreEqual(new DateTime(1984, 2, 5), d.Current().HalfMonth.Add(-2));
            Assert.AreEqual(new DateTime(1984, 2, 20), d.Current().HalfMonth.Add(-1));
            Assert.AreEqual(new DateTime(1984, 3, 5), d.Current().HalfMonth.Add(0));
            Assert.AreEqual(new DateTime(1984, 3, 20), d.Current().HalfMonth.Add(1));
            Assert.AreEqual(new DateTime(1984, 4, 5), d.Current().HalfMonth.Add(2));
            Assert.AreEqual(new DateTime(1984, 4, 20), d.Current().HalfMonth.Add(3));
            Assert.AreEqual(new DateTime(1984, 5, 5), d.Current().HalfMonth.Add(4));
        }

        [TestMethod]
        public void BeginHalfMonthShouldGetTheCorrectDate()
        {
            var d1 = new DateTime(1984, 9, 13);
            var d2 = new DateTime(1984, 9, 23);
            Assert.AreEqual(new DateTime(1984, 9, 1), d1.Current().HalfMonth.Begin());
            Assert.AreEqual(new DateTime(1984, 9, 16), d2.Current().HalfMonth.Begin());
        }

        [TestMethod]
        public void ToStringShouldReturnAStringRepresentationOfObject()
        {
            var d = new DateTime(1984, 9, 13);
            Assert.AreEqual(string.Format("Week of " + d), d.Current().Week.ToString());
            Assert.AreEqual(string.Format("Week of 13/09/1984"), d.Current().Week.ToString("dd/MM/yyyy"));
        }
    }
}
