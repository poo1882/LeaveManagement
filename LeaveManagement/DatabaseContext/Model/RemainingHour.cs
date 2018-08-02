using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.DatabaseContext.Model
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

        

        public RemainingHour(string staffId, string year, int totalAnnualHours,int totalSickHours)
        {
            StaffId = staffId;
            Year = year;
            TotalAnnualHours = totalAnnualHours;
            TotalSickHours = totalSickHours;
            AnnualHours = TotalSickHours;
            SickHours = TotalSickHours;
            LWPHours = 0;
        }

        public RemainingHour()
        {

        }
    }
}
