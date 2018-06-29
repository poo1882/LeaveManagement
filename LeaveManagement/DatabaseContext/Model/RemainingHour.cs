﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.DatabaseContext.Model
{
    public class RemainingHour
    {
       
        public Guid EmployeeId { get; set; }
        public string Year { get; set; }
        public int AnnualHours { get; set; }
        public int SickHours { get; set; }
        public int LWPHours { get; set; }
    }
}
