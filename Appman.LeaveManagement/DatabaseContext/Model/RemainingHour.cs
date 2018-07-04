using System;
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

        public RemainingHour(int defaultHours, string staffId, string year)
        {
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
