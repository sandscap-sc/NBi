﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.Query.Generation.Postgresql
{
    class FieldFormatterPostgresql : BaseFieldFormatter
    {
        public FieldFormatterPostgresql()
        : base('"', '"') { }
    }
}
