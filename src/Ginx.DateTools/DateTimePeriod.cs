using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ginx.DateTools
{
    public sealed class DateTimePeriod
    {
        private readonly DateTime date;

        internal DateTimePeriod(DateTime date)
        {
            this.date = date;
        }

        public DateTimeBuilder Day
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Day);
            }
        }

        public DateTimeBuilder Week
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Week);
            }
        }

        public DateTimeBuilder HalfMonth
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.HalfMonth);
            }
        }

        public DateTimeBuilder Month
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Month);
            }
        }

        public DateTimeBuilder Quarter
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Quarter);
            }
        }

        public DateTimeBuilder Semester
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Semester);
            }
        }

        public DateTimeBuilder Year
        {
            get
            {
                return new DateTimeBuilder(this.date, DatePart.Year);
            }
        }
    }
}
