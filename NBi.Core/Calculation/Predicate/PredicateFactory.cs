﻿using NBi.Core.Calculation.Predicate.Boolean;
using NBi.Core.Calculation.Predicate.DateTime;
using NBi.Core.Calculation.Predicate.Numeric;
using NBi.Core.Calculation.Predicate.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.Calculation.Predicate
{
    class PredicateFactory
    {
        public IPredicate Get(IPredicateInfo info)
        {
            switch (info.ColumnType)
            {
                case NBi.Core.ResultSet.ColumnType.Text:
                    switch (info.ComparerType)
                    {
                        case ComparerType.LessThan: return new TextLessThan(info.Reference);
                        case ComparerType.LessThanOrEqual: return new TextLessThanOrEqual(info.Reference);
                        case ComparerType.Equal: return new TextEqual(info.Reference);
                        case ComparerType.MoreThanOrEqual: return new TextMoreThanOrEqual(info.Reference);
                        case ComparerType.MoreThan: return new TextMoreThan(info.Reference);
                        case ComparerType.Null: return new TextNull();
                        case ComparerType.Empty: return new TextEmpty();
                        case ComparerType.NullOrEmpty: return new TextNullOrEmpty();
                        case ComparerType.LowerCase: return new TextLowerCase();
                        case ComparerType.UpperCase: return new TextUpperCase();
                        case ComparerType.StartsWith: return new TextStartsWith(info.Reference, info.StringComparison);
                        case ComparerType.EndsWith: return new TextEndsWith(info.Reference, info.StringComparison);
                        case ComparerType.Contains: return new TextContains(info.Reference, info.StringComparison);
                        case ComparerType.MatchesRegex: return new TextMatchesRegex(info.Reference, info.StringComparison);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case NBi.Core.ResultSet.ColumnType.Numeric:
                    switch (info.ComparerType)
                    {
                        case ComparerType.LessThan: return new NumericLessThan(info.Reference);
                        case ComparerType.LessThanOrEqual: return new NumericLessThanOrEqual(info.Reference);
                        case ComparerType.Equal: return new NumericEqual(info.Reference);
                        case ComparerType.MoreThanOrEqual: return new NumericMoreThanOrEqual(info.Reference);
                        case ComparerType.MoreThan: return new NumericMoreThan(info.Reference);
                        case ComparerType.Null: return new NumericNull();
                        case ComparerType.WithinRange: return new NumericWithinRange(info.Reference);
                        default:
                            throw new ArgumentOutOfRangeException($"Numeric columns don't support {info.ComparerType.ToString()} comparer.");
                    }
                case NBi.Core.ResultSet.ColumnType.DateTime:
                    switch (info.ComparerType)
                    {
                        case ComparerType.LessThan: return new DateTimeLessThan(info.Reference);
                        case ComparerType.LessThanOrEqual: return new DateTimeLessThanOrEqual(info.Reference);
                        case ComparerType.Equal: return new DateTimeEqual(info.Reference);
                        case ComparerType.MoreThanOrEqual: return new DateTimeMoreThanOrEqual(info.Reference);
                        case ComparerType.MoreThan: return new DateTimeMoreThan(info.Reference);
                        case ComparerType.Null: return new DateTimeNull();
                        default:
                            throw new ArgumentOutOfRangeException("DateTime columns don't support Empty comparer.");
                    }
                case NBi.Core.ResultSet.ColumnType.Boolean:
                    switch (info.ComparerType)
                    {
                        case ComparerType.Equal: return new BooleanEqual(info.Reference);
                        case ComparerType.Null: return new BooleanNull();
                        default:
                            throw new ArgumentOutOfRangeException("Boolean columns only support Equal and Null comparers and not More or Less Than comparers.");
                    }
                default:
                    break;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
