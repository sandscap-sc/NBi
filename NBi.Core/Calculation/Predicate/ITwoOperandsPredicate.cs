﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.Calculation.Predicate
{
    public interface ITwoOperandsPredicate : IPredicate
    {
        object SecondOperand { get; set; }
    }
}
