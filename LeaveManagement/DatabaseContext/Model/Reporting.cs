using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.DatabaseContext.Model
{
    public class Reporting
    {
        public string StaffId { get; set; }
        public string Approver { get; set; }
    }
}
