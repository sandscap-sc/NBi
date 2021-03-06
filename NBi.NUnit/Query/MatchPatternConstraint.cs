﻿using System;
using System.Data;
using System.Linq;
using NUnit.Framework.Constraints;
using NUnitCtr = NUnit.Framework.Constraints;
using NBi.Core.Query.Format;
using System.Collections.Generic;
using NBi.Extensibility.Query;

namespace NBi.NUnit.Query
{
    public class MatchPatternConstraint : NBiConstraint
    {
        private string regex;
        private readonly IList<string> invalidMembers = new List<string>();
        protected IFormatEngine engine;
        /// <summary>
        /// Engine dedicated to ResultSet comparaison
        /// </summary>
        protected internal IFormatEngine Engine
        {
            set
            {
                engine = value ?? throw new ArgumentNullException();
            }
        }

        protected IFormatEngine GetEngine(IQuery actual)
        {
            if (engine == null)
                engine = new FormatEngineFactory().Instantiate(actual);
            return engine;
        }

        #region Modifiers
        /// <summary>
        /// Set the regex pattern
        /// </summary>
        public MatchPatternConstraint Regex(string pattern)
        {
            this.regex = pattern;
            return this;
        }
        #endregion

        /// <summary>
        /// Store for the result of the engine's execution
        /// </summary>
        protected IEnumerable<string> formattedResults;

        protected virtual NUnitCtr.Constraint BuildInternalConstraint()
        {
            NUnitCtr.Constraint ctr = null;

            if (!string.IsNullOrEmpty(regex))
            {
                if (ctr != null)
                    ctr = ctr.And.Matches(regex);
                else
                    ctr = new NUnitCtr.RegexConstraint(regex);
            }

            return ctr;
        }

        protected virtual bool DoMatch(NUnitCtr.Constraint ctr, string caption)
        {
            IResolveConstraint exp = ctr;
            var multipleConstraint = exp.Resolve();
            return multipleConstraint.Matches(caption);
        }

        public override bool Matches(object actual)
        {
            if (actual is IQuery)
                return Process((IQuery)actual);
            else if (actual is IEnumerable<string>)
            {
                this.actual = actual;

                var res = true;
                foreach (var result in (IEnumerable<string>)actual)
                {
                    var ctr = BuildInternalConstraint();
                    if (!DoMatch(ctr, result))
                    {
                        res = false;
                        invalidMembers.Add(result);
                    }
                }
                return res;
            }
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Handle an IDbCommand (Query and ConnectionString) and check it with the expectation (Another IDbCommand or a ResultSet)
        /// </summary>
        /// <param name="actual">IDbCommand</param>
        /// <returns></returns>
        public bool Process(IQuery actual)
        {
            var result = GetEngine(actual).ExecuteFormat();
            return this.Matches(result);
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(NUnitCtr.MessageWriter writer)
        {
            //writer.WriteActualValue(actual);

            writer.WritePredicate(string.Format("The formatted value of each cell matchs the"));

            if (!string.IsNullOrEmpty(regex))
            {
                writer.WritePredicate(" regex pattern ");
                writer.WritePredicate(regex);
            }
        }

        public override void WriteActualValueTo(NUnitCtr.MessageWriter writer)
        {
            if (invalidMembers.Count == 1)
                writer.WriteMessageLine(string.Format("The element <{0}> doesn't validate this pattern", invalidMembers[0]));
            else
            {
                writer.WriteMessageLine(string.Format("{0} elements don't validate this pattern:", invalidMembers.Count));
                foreach (var invalidMember in invalidMembers)
                    writer.WriteMessageLine(string.Format("    <{0}>", invalidMember));
            }
        }
    }
}
