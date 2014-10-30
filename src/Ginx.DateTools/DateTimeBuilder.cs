namespace Ginx.DateTools
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
            if (increment == 0)
            {
                return this;
            }

            var ret = this;

            switch (this.part)
            {
                case DatePart.Day:
                    ret = this.Clone(this.date.AddDays(increment));
                    break;

                case DatePart.Week:
                    ret = this.Clone(this.date.AddDays(increment * 7));
                    break;

                case DatePart.HalfMonth:
                    {
                        if (increment % 2 == 0)
                        {
                            ret = this.Clone(this.date.AddMonths(increment / 2));
                            break;
                        }

                        var newDate = this.date.AddMonths(increment / 2);
                        var sign = increment % 2;
                        //var days = 15;

                        if (sign == -1 && this.date.Day <= 15)
                        {
                            ret =  this.Clone(newDate.AddMonths(-1).AddDays(15));
                            break;
                        }

                        if (sign == 1 && this.date.Day > 15)
                        {
                            ret = this.Clone(newDate.AddMonths(1).AddDays(-15));
                            break;
                        }

                        if (newDate.Day == 31 && sign == -1)
                        {
                            ret = this.Clone(newDate.AddDays(-16));
                            break;
                        }

                        ret = this.Clone(newDate.AddDays(15 * sign));
                        break;
                    }

                case DatePart.Month:
                    ret = this.Clone(this.date.AddMonths(increment));
                    break;

                case DatePart.Quarter:
                    ret = this.Clone(this.date.AddMonths(increment * 3));
                    break;

                case DatePart.Semester:
                    ret = this.Clone(this.date.AddMonths(increment * 6));
                    break;

                case DatePart.Year:
                    ret = this.Clone(this.date.AddYears(increment));
                    break;
            }

            return ret;
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
            var ret = this;
            switch (this.part)
            {
                case DatePart.Day:
                    ret = this.Clone(this.date.Date);
                    break;

                case DatePart.Week:
                    var firstDayOfWeek = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    ret = this.Clone(this.date.InThisWeek(firstDayOfWeek));
                    break;

                case DatePart.HalfMonth:
                    ret = this.Clone(
                        new DateTime(
                            this.date.Year,
                            this.date.Month,
                            this.date.Day <= 15 ? 1 : 16));
                    break;

                case DatePart.Month:
                    ret = this.Clone(new DateTime(this.date.Year, this.date.Month, 1));
                    break;

                case DatePart.Quarter:
                    {
                        int month = (((int)Math.Ceiling(date.Month / 3.0)) - 1) * 3 + 1;
                        ret = this.Clone(new DateTime(this.date.Year, month, 1));
                        break;
                    }

                case DatePart.Semester:
                    {
                        int month = (((int)Math.Ceiling(date.Month / 6.0)) - 1) * 6 + 1;
                        ret = this.Clone(new DateTime(this.date.Year, month, 1));
                        break;
                    }

                case DatePart.Year:
                    ret = this.Clone(new DateTime(this.date.Year, 1, 1));
                    break;
            }

            return ret;
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
            return string.Format("{0} of {1}", this.part, this.date);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("{0} of {1}", this.part, this.date.ToString(format, formatProvider));
        }
    }
}
