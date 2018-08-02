using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class LeavingInfo
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SickHoursLeft { get; set; }
        public int AnnualHoursLeft { get; set; }
        public int LWPHoursLeft { get; set; }        
    }
}
