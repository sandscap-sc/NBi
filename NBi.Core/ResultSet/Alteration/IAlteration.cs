﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.ResultSet.Alteration
{
    public interface IAlteration
    {
        ResultSet Execute(ResultSet resultSet);
    }
}