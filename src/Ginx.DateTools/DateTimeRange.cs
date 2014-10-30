namespace Ginx.DateTools
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public struct DateTimeRange : IEquatable<DateTimeRange>, IFormattable
    {
        private static readonly DateTimeRange minValue = new DateTimeRange(DateTime.MinValue, DateTime.MinValue);
        private static readonly DateTimeRange maxValue = new DateTimeRange(DateTime.MinValue, DateTime.MaxValue);

        public static DateTimeRange MinValue
        {
            get
            {
                return minValue;
            }
        }

        public static DateTimeRange MaxValue
        {
            get
            {
                return maxValue;
            }
        }

        private DateTime start;
        private DateTime end;

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentOutOfRangeException("end");
            }

            this.start = start;
            this.end   = end;
        }

        public DateTimeRange(int year)
        {
            this.start = new DateTime(year, 1, 1);
            this.end   = this.start.AddYears(1).AddTicks(-1);
        }

        public DateTimeRange(int year, int month)
        {
            if (month <= 0 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month");
            }

            this.start = new DateTime(year, month, 1);
            this.end = start.AddMonths(1).AddTicks(-1);
        }

        public DateTime Start
        {
            get
            {
                return this.start;
            }
        }

        public DateTime End
        {
            get
            {
                return this.end;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return this.end - this.start;
            }
        }

        public bool Contains(DateTime date)
        {
            return this.start <= date && date <= this.end;
        }

        public bool Contains(params DateTime[] dates)
        {
            var range = this;
            return dates.All(d => range.Contains(d));
        }

        public bool Contains(DateTimeRange range)
        {
            return this.Contains(range.start, range.end);
        }

        public IEnumerable<DateTime> ToEnumerable(DatePart part, int step = 1)
        {
            if (step <= 0)
            {
                throw new ArgumentOutOfRangeException("step");
            }
            
            var current = this.start.With(part);
            do
            {
                yield return current.DateTime;
                current = current.Add(step);
            } while (current.DateTime <= this.end);
        }

        public override string ToString()
        {
            return string.Format("{0} to {1}", this.start, this.end);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(
                "{0} to {1}",
                this.start.ToString(format, formatProvider),
                this.end.ToString(format, formatProvider));
        }

        public bool Equals(DateTimeRange other)
        {
            return  other.start == this.start && other.end == this.end;
        }
    }
}
