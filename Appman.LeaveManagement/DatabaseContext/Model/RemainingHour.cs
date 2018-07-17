﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class RemainingHour
    {
        public string StaffId { get; set; }
        public string Year { get; set; }
        public int AnnualHours { get; set; }
        public int SickHours { get; set; }
        public int LWPHours { get; set; }
        public int TotalAnnualHours { get; set; }
        public int TotalSickHours { get; set; }
        public int TotalLWPHours { get; set; }

        public RemainingHour(int defaultHours, string staffId, string year)
        {
            TotalAnnualHours = defaultHours;
            TotalSickHours = defaultHours;
            TotalLWPHours = defaultHours;
            AnnualHours = defaultHours;
            SickHours = defaultHours;
            LWPHours = defaultHours;
            Year = year;
            StaffId = staffId;
        }

        public RemainingHour()
        {

        }
    }
}
