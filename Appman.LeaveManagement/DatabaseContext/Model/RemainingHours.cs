using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class RemainingHours
    {
       
        public Guid EmployeeId { get; set; }
        
        public string Year { get; set; }
        public int AnnualHours { get; set; }
        public int SickHours { get; set; }
        public int LWPHours { get; set; }
    }
}
