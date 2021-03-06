﻿using System;
using System.Collections.Generic;

namespace Northwind2API_EFDB.Models
{
    public partial class EmployeeTerritory
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        internal virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }
    }
}
