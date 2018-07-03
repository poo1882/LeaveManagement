using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LeaveInfo ViewLeaveInfo(string id)
        {
            var emp = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == id);
            return emp;
        }
        
        public Boolean CreateLeaveInfo(LeaveInfo info)
        {
            var remain = new RemainingHourRepository(_dbContext);
            if(info.EndDateTime == null)
            {
                if ( remain.ViewHour(info.StaffId,info.StartDateTime.Year.ToString(),info.Type) >= info.HoursStartDate)
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
                int totalHours = (totalDays-1) * 8 + info.HoursStartDate + info.HoursEndDate;
                if (remain.ViewHour(info.StaffId, info.StartDateTime.Year.ToString(), info.Type) >= totalHours)
                {
                    _dbContext.LeaveInfos.Add(info);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            
        }

        public List<LeaveInfo> GetHistory()
        {
            return _dbContext.LeaveInfos.ToList();
        }

        public List<LeaveInfo> GetHistory(string staffId)
        {

            return _dbContext.LeaveInfos.Where(x => x.StaffId == staffId).ToList();
        }

        public List<LeaveInfo> GetRemaining(string staffId)
        {

            var result = from reportlist in _dbContext.Reportings
                         join leaveinfo in _dbContext.LeaveInfos on reportlist.StaffId equals leaveinfo.StaffId
                         where reportlist.Approver == staffId
                         select leaveinfo;
            return result.ToList();
        }

        public void Approve(string leaveId,string approver)
        {
            _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovalStatus = "Approved";
            _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedBy = approver;
            _dbContext.SaveChanges();
        }

        public void Reject(string leaveId,string approver)
        {
            _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovalStatus = "Rejected";
            _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedBy = approver;
            _dbContext.SaveChanges();
        }

        public int PendingAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == null).Count();
        }

        public int ApproveAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == "Approved").Count();
        }

        public int RejectAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus == "Rejected").Count();
        }
    }
}
