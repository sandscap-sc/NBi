﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.Json
{
    public class EvaluateSelect: ElementSelect
    {
        internal EvaluateSelect(string xpath)
            : base(xpath)
        {
        }
    }
}
