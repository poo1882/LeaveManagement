using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class LeaveInfoRepository
    {
        LeaveManagementDbContext _dbContext;
        public LeaveInfoRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     View a leave's detail
        /// </summary>
        /// <param name="leaveId">An id of the form</param>
        /// <returns>
        ///     LeaveInfo - An information of the form
        ///     null - if no form found
        /// </returns>
        public LeaveInfo ViewLeaveInfo(string leaveId)
        {
            var emp = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
            if (emp == null)
                return null;
            return emp;
        }

        

        public bool CreateLeaveInfo(LeaveInfo info)
        {
            var remain = new RemainingHourRepository(_dbContext);
            if (info.EndDateTime.ToString() == info.StartDateTime.ToString())
            {
                if (remain.ViewHour(info.StaffId, info.StartDateTime.Year.ToString(), info.Type) >= info.HoursStartDate)
                {
                    _dbContext.LeaveInfos.Add(info);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                int totalDays = (info.EndDateTime - info.StartDateTime).Days;
                int totalHours = (totalDays - 1) * 8 + info.HoursStartDate + info.HoursEndDate;
                if (remain.ViewHour(info.StaffId, info.StartDateTime.Year.ToString(), info.Type) >= totalHours)
                {
                    //UpdateRemainHour(info.StaffId, info.Type, totalHours);
                    _dbContext.LeaveInfos.Add(info);
                    _dbContext.SaveChanges();

                    return true;
                }
                return false;
            }

        }

        public List<LeaveInfo> GetHistory()
        {
            var result = from employeelist in _dbContext.Employees
                         join leaveinfo in _dbContext.LeaveInfos on employeelist.StaffId equals leaveinfo.StaffId
                         where employeelist.IsActive == true
                         select leaveinfo;
            return _dbContext.LeaveInfos.ToList();
        }


        /// <summary>
        ///     Get all created leave forms of an employee
        /// </summary>
        /// <param name="staffId">An employee id</param>
        /// <returns>
        ///     List<LeaveInfo> - List of leave forms in db
        ///     null - if no form created
        /// </returns>
        public List<LeaveInfo> GetHistory(string staffId)
        {
            return _dbContext.LeaveInfos.Where(x => x.StaffId == staffId).ToList();
        }


        /// <summary>
        ///     Get all remaining forms waiting for approve of an approver
        /// </summary>
        /// <param name="staffId">An approver's id</param>
        /// <returns>
        ///     List<LeaveInfo> - List of remaining leave forms
        ///     null - if no form remaining
        /// </returns>
        public List<LeaveInfo> GetRemaining(string staffId)
        {

            var result = from reportlist in _dbContext.Reportings
                         join leaveinfo in _dbContext.LeaveInfos on reportlist.StaffId equals leaveinfo.StaffId
                         where reportlist.Approver == staffId
                         select leaveinfo;
            return result.ToList();
        }

        /// <summary>
        ///     Set status of form to "Approved"
        /// </summary>
        /// <param name="leaveId">id of an approved form</param>
        /// <param name="approverId">id of an approver</param>
        /// <returns>
        ///     true - if success
        ///     false - if form is already approved or rejected, or no form found
        /// </returns>
        public bool SetStatus(string status,string leaveId,string approverId)
        {
            var leave = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
            //var approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
            Reporting report = new Reporting { StaffId=leave.StaffId, Approver=approverId};

            if (leave == null)
                return false;

            if (leave.ApprovalStatus != "Pending")
                    return false;
            bool isInReport = _dbContext.Reportings.Any(x => x.Approver == approverId && x.StaffId == leave.StaffId);
            //bool isInReport = reportings.Contains(report);
            if (!isInReport && _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).IsSuperHr == false)
                    return false;
            
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedBy = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).FirstName;
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedTime = DateTime.Now;
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovalStatus = status;

            var approveBy = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).FirstName;
            leave.ApprovedTime = DateTime.Now;
            leave.ApprovedBy = approveBy;
            leave.ApprovalStatus = status;
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Set status of form to "Rejected"
        /// </summary>
        /// <param name="leaveId">id of an rejected form</param>
        /// <param name="approverId">id of an approver</param>
        /// /// <returns>
        ///     true - if success
        ///     false - if form is already approved or rejected, or no form found
        /// </returns>
        //public bool Reject(string leaveId,string approverId)
        //{
        //    var leave = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
        //    var approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
        //    Reporting report = new Reporting { StaffId = leave.StaffId, Approver = approverId };

        //    if (leave == null)
        //        return false;

        //    if (leave.ApprovalStatus != "Pending")
        //        return false;

        //    if (!_dbContext.Reportings.Contains(report) && approver.IsSuperHr == false)
        //        return false;

        //    leave.ApprovalStatus = "Rejected";
        //    leave.ApprovedBy = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).FirstName;
        //    _dbContext.SaveChanges();
        //    return true;
        //}

        /// <summary>
        ///     Calculate the amount of pending leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of pending forms
        /// </returns>
        public int PendingAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == null).Count();
        }

        /// <summary>
        ///     Calculate the amount of approved leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of approved forms
        /// </returns>
        public int ApprovedAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == "Approved").Count();
        }

        /// <summary>
        ///     Calculate the amount of rejected leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of rejected forms
        /// </returns>
        public int RejectedAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == "Rejected").Count();
        }

        /// <summary>
        ///     Calculste the amount of forms
        /// </summary>
        /// <returns>
        ///     int - the number of leaves form
        /// </returns>
        private int LeaveCount()
        {
            return _dbContext.LeaveInfos.Count();
        }

        public string GenerateLeaveId()
        {
            return "LE" + LeaveCount().ToString("D10");
        }
    }
}
