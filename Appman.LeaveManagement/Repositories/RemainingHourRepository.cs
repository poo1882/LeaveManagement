using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class RemainingHourRepository
    {
        LeaveManagementDbContext _dbContext;
        public RemainingHourRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int ViewHour(string staffId, string year, string type)
        {
            type = type.ToLower();
            if (type == "sick")
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).SickHours;
            else if (type == "annual")
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).AnnualHours;
            else
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).LWPHours;
        }

        public void UpdateRemainHour(string staffId, string type, int totalHours)
        {
            if (type.ToLower() == "annual")
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).AnnualHours -= totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower() == "sick")
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).SickHours -= totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower() == "lwp")
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).AnnualHours -= totalHours;
                _dbContext.SaveChanges();
            }
        }

        public void generateHours(RemainingHour remaining)
        {
            _dbContext.RemainingHours.Add(remaining);
            _dbContext.SaveChanges();
        }

        public List<RemainingHour> ViewAllRemainingHour()
        {
            return _dbContext.RemainingHours.ToList();
        }
    }
}
