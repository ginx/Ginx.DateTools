namespace Ginx.DateTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class DateTimeExtensions
    {
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            date = date.Date.AddDays(1);
            int days = ((int)dayOfWeek - (int) date.DayOfWeek + 7) % 7;
            return date.AddDays(days);
        }

        public static DateTime Previous(this DateTime date, DayOfWeek dayOfWeek)
        {
            date = date.Date.AddDays(-1);
            int days = ((int)date.DayOfWeek - (int)dayOfWeek + 7) % 7;
            return date.AddDays(-days);
        }

        public static DateTime InThisWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            date = date.AddDays(-(date.DayOfWeek - firstDayOfWeek));
            
            if (dayOfWeek != firstDayOfWeek)
            {
                date = date.Next(dayOfWeek);
            }

            return date;
        }

        public static DateTimeBuilder With(this DateTime date, DatePart part)
        {
            return new DateTimeBuilder(date, part);
        }

        public static DateTimePeriod Current(this DateTime date)
        {
            return new DateTimePeriod(date);
        }
    }
}
