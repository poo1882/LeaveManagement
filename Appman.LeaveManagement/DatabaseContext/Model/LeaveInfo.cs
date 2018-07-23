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
        public string AttachedFile1 { get; set; }
        public string AttachedFileName1 { get; set; }
        public string AttachedFile2 { get; set; }
        public string AttachedFileName2 { get; set; }
        public string AttachedFile3 { get; set; }
        public string AttachedFileName3 { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public bool IsExisting { get; set; }
        public string CommentByAdmin { get; set; }
    }
}
