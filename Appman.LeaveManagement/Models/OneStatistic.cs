using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Models
{
    public class OneStatistic
    {
        //public string ApprovalStatus;
        //public string LeaveId;
        //public string StaffId;
        //public DateTime RequestedDateTime;
        //public DateTime StartDateTime;
        //public DateTime EndDateTime;
        //public string Approver;

        public string FirstNameTH;
        public string LastNameTH;
        public string StaffId;
        public string Department;
        public string Position;
        public int AnnualHours;
        public int SickHours;
        public int LWPHours;
        public List<LeaveInfo> Leaves;

        public OneStatistic(string staffId, EmployeeRepository _empRepo, RemainingHourRepository _remRepo,MdRoleRepository _mdRoleRepo)
        {
            
            Employee emp = _empRepo.GetProfile(staffId);
            MdRole role = _mdRoleRepo.GetRole(emp.RoleCode);
            FirstNameTH = emp.FirstNameTH;
            LastNameTH = emp.LastNameTH;
            StaffId = emp.StaffId;
            Department = role.Department;
            Position = role.Position;
            AnnualHours = _remRepo.ViewHour(staffId, DateTime.Now.Year.ToString(), "Annual");
            SickHours = _remRepo.ViewHour(staffId, DateTime.Now.Year.ToString(), "Sick");
            LWPHours = _remRepo.ViewHour(staffId, DateTime.Now.Year.ToString(), "LWP");
            
            
        }

        //public OneStatistic(int LeaveId, LeaveInfoRepository _leaveRepo, EmployeeRepository _empRepo)
        //{
        //    var leave = _leaveRepo.ViewLeaveInfo(LeaveId);
        //    ApprovalStatus = leave.ApprovalStatus;
        //    LeaveId = leave.Id;
        //    StaffId = leave.StaffId.ToString();
        //    RequestedDateTime = leave.RequestedDateTime;
        //    StartDateTime = leave.StartDateTime;
        //    EndDateTime = leave.EndDateTime;
        //    if (leave.ApprovedBy != null)
        //        Approver = _empRepo.GetProfile(leave.ApprovedBy).FirstName;
        //    else
        //        Approver = null;
        //}

       
    }

    
}
