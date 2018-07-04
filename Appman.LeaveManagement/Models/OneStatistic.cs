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

        public string FirstName;
        public string LastName;
        public string StaffId;
        public string Section;
        public string Position;
        public int AnnualHours;
        public int SickHours;
        public int LWPHours;
        public List<LeaveInfo> Leaves;

        public OneStatistic(string staffId, EmployeeRepository _empRepo, RemainingHourRepository _remRepo)
        {
            Employee emp = _empRepo.GetProfile(staffId);
            FirstName = emp.FirstName;
            LastName = emp.LastName;
            StaffId = emp.StaffId;
            Section = emp.Section;
            Position = emp.Position;
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
