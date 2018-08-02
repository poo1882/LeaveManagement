using LeaveManagement.DatabaseContext;
using LeaveManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class Statistic
    {
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string StaffId { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public int Pending { get; set; }
        public int Approve { get; set; }
        public int Reject { get; set; }

        public Statistic(string staffId, EmployeeRepository _empRepo, LeaveInfoRepository _leaveRepo,MdRoleRepository _mdRoleRepo)
        {
            var employee = _empRepo.GetProfile(staffId);
            var role = _mdRoleRepo.GetRole(employee.RoleCode);
            FirstNameEN = employee.FirstNameEN;
            LastNameEN = employee.LastNameEN;
            StaffId = employee.StaffId;
            Position = _mdRoleRepo.GetRole(employee.RoleCode).Position;
            Department = _mdRoleRepo.GetRole(employee.RoleCode).Department;
            Pending = _leaveRepo.PendingAmount(staffId);
            Approve = _leaveRepo.ApprovedAmount(staffId);
            Reject = _leaveRepo.RejectedAmount(staffId);
        }
    }
}
