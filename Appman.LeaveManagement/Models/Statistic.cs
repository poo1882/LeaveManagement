using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Models
{
    public class Statistic
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffId { get; set; }
        public string Position { get; set; }
        public string Section { get; set; }
        public int Pending { get; set; }
        public int Approve { get; set; }
        public int Reject { get; set; }

        public Statistic(string staffId, EmployeeRepository _empRepo, LeaveInfoRepository _leaveRepo)
        {
            var employee = _empRepo.GetProfile(staffId);
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            StaffId = employee.StaffId;
            Position = employee.Position;
            Section = employee.Section;
            Pending = _leaveRepo.PendingAmount(staffId);
            Approve = _leaveRepo.ApprovedAmount(staffId);
            Reject = _leaveRepo.RejectedAmount(staffId);
        }
    }
}
