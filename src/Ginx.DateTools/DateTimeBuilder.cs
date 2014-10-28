namespace Ginx.DateTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class DateTimeBuilder : IFormattable
    {
        private readonly DateTime date;
        private readonly DatePart part;

        internal DateTimeBuilder(DateTime date, DatePart part)
        {
            this.date = date;
            this.part = part;
        }

        public DateTime DateTime
        {
            get
            {
                return this.date;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date.Date;
            }
        }

        public DateTimeRange Range
        {
            get
            {
                return new DateTimeRange(this.Begin(), this.End());
            }
        }

        private DateTimeBuilder Clone(DateTime date)
        {
            return new DateTimeBuilder(date, this.part);
        }

        public DateTimeBuilder Add(int increment)
        {
            switch (this.part)
            {
                case DatePart.Day:
                    return this.Clone(this.date.AddDays(increment));

                case DatePart.Week:

                    int counter = increment;
                    var dayOfWeek = this.date.DayOfWeek;
                    var current = this.date;
                    if (increment > 0)
                    {
                        while (counter-- > 0)
                        {
                            current = current.Next(dayOfWeek);
                        }
                    }

                    if (increment < 0)
                    {
                        while (counter++ < 0)
                        {
                            current = current.Previous(dayOfWeek);
                        }
                    }

                    return this.Clone(current);

                case DatePart.HalfMonth:
                    {
                        var date = this.date.AddMonths(increment / 2);

                        if (increment % 2 != 0)
                        {
                            var tmp = date.AddDays(15);

                            if (date.Day <= 15 && tmp.Month != date.Month)
                            {
                                tmp = date.With(DatePart.Month).End();
                            }

                            if (date.Day > 15 && tmp.Month == date.Month)
                            {
                                tmp = date.With(DatePart.Month).Next().Begin();
                            }

                            date = tmp;
                        }

                        return this.Clone(date);
                    }

                case DatePart.Month:
                    return this.Clone(this.date.AddMonths(increment));

                case DatePart.Quarter:
                    return this.Clone(this.date.AddMonths(increment * 3));

                case DatePart.Semester:
                    return this.Clone(this.date.AddMonths(increment * 6));

                case DatePart.Year:
                    return this.Clone(this.date.AddYears(increment));

                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        public DateTimeBuilder Next()
        {
            return this.Add(1);
        }

        public DateTimeBuilder Previous()
        {
            return this.Add(-1);
        }

        public DateTimeBuilder Begin()
        {
            switch (this.part)
            {
                case DatePart.Day:
                    return this.Clone(this.date.Date);

                case DatePart.Week:
                    var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    return this.Clone(this.date.InThisWeek(firstDayOfWeek));

                case DatePart.HalfMonth:
                    return this.Clone(
                        new DateTime(
                            this.date.Year,
                            this.date.Month,
                            this.date.Day <= 15 ? 1 : 16));

                case DatePart.Month:
                    return this.Clone(new DateTime(this.date.Year, this.date.Month, 1));

                case DatePart.Quarter:
                    {
                        int month = (((int)Math.Ceiling(date.Month / 3.0)) - 1) * 3 + 1;
                        return this.Clone(new DateTime(this.date.Year, month, 1));
                    }

                case DatePart.Semester:
                    {
                        int month = (((int)Math.Ceiling(date.Month / 6.0)) - 1) * 6 + 1;
                        return this.Clone(new DateTime(this.date.Year, month, 1));
                    }

                case DatePart.Year:
                    return this.Clone(new DateTime(this.date.Year, 1, 1));

                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        public DateTimeBuilder End()
        {
            return this.Clone(this.Begin().Next().DateTime.AddTicks(-1));
        }

        public static implicit operator DateTime(DateTimeBuilder d)
        {
            return d.date;
        }

        public override string ToString()
        {
            return this.date.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.date.ToString(format, formatProvider);
        }
    }
}
