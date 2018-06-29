using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.DatabaseContext.Model
{
    public class LeaveInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int HoursStartDate { get; set; }
        public int HoursEndDate { get; set; }
        public bool ApprovalStatus { get; set; }
        public string Comment { get; set; }
        public DateTime AprroveTime { get; set; }
        public Guid ApprovedBy { get; set; }
        public byte[] AttachedFile { get; set; }
        public DateTime RequestedDateTime { get; set; }
    }
}
