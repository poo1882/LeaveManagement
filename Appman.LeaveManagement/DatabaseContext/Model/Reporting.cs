using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class Reporting
    {
        public Guid EmployeeId { get; set; }
        public Guid ReportingTo { get; set; }
    }
}
