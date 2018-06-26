using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LeaveInfo ViewLeaveInfo(Guid form)
        {
            var emp = _dbContext.LeaveInfos.FirstOrDefault(x => x.Id == form);
            return emp;
        }
        
        public Boolean CreateLeaveInfo(LeaveInfo info)
        {
            var remain = new RemainingHourRepository(_dbContext);
            if(info.EndDateTime == null)
            {
                if ( remain.ViewHour(info.EmployeeId,info.StartDateTime.Year.ToString(),info.Type) >= info.HoursStartDate)
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
                if (remain.ViewHour(info.EmployeeId, info.StartDateTime.Year.ToString(), info.Type) >= totalHours)
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

        public List<LeaveInfo> GetHistory(Guid employeeId)
        {

            return _dbContext.LeaveInfos.Where(x => x.EmployeeId == employeeId).ToList();
        }

        public List<LeaveInfo> GetRemaining(Guid employeeId)
        {

            var result = from reportlist in _dbContext.Reportings
                         join leaveinfo in _dbContext.LeaveInfos on reportlist.EmployeeId equals leaveinfo.EmployeeId
                         where reportlist.ReportingTo == employeeId
                         select leaveinfo;
            return result.ToList();
        }

       




    }
}
