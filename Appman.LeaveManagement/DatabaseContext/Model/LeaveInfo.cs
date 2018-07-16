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
        public int LeaveId { get; set; }
        public string Type { get; set; }
        public string StaffId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int HoursStartDate { get; set; }
        public int HoursEndDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comment { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public string ApprovedBy { get; set; }
        public string AttachedFile { get; set; }
        public string AttachedFileName { get; set; }
        public DateTime RequestedDateTime { get; set; }
    }
}
