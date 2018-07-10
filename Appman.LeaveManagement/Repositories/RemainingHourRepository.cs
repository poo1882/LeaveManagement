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
            if (type[0] == 's')
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).SickHours;
            else if (type[0] == 'a')
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).AnnualHours;
            else
                return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).LWPHours;
        }

        public void DeductRemainHour(string staffId, string type, int totalHours)
        {
            if (type.ToLower()[0] == 'a')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).AnnualHours -= totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower()[0] == 's')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).SickHours -= totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower()[0] == 'l')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).LWPHours -= totalHours;
                _dbContext.SaveChanges();
            }
        }

        public void AddRemainHour(string staffId, string type, int totalHours)
        {
            if (type.ToLower()[0] == 'a')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).AnnualHours += totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower()[0] == 's')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).SickHours += totalHours;
                _dbContext.SaveChanges();
            }
            else if (type.ToLower()[0] == 'l')
            {
                _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId).LWPHours += totalHours;
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
