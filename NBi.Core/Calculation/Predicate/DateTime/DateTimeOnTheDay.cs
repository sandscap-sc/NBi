﻿using NBi.Core.ResultSet.Comparer;
using NBi.Core.ResultSet.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.Calculation.Predicate.DateTime
{
    class DateTimeOnTheDay : AbstractPredicate
    {
        public DateTimeOnTheDay(bool not)
            :base(not)
        { }

        protected override bool Apply(object x)
        {
            var converter = new DateTimeConverter();
            var dtX = converter.Convert(x);

            return (dtX.TimeOfDay.Ticks) == 0;
        }

        public override string ToString() => $"is on the day";
    }
}
