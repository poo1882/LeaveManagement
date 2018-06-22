using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.DatabaseContext.Model
{
    public class LeaveInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int HoursStartDate { get; set; }
        public int HoursEndDate { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public DateTime AprroveTime { get; set; }
        public Guid ApprovedBy { get; set; }
        public byte[] AttachedFile { get; set; }



    }
}
