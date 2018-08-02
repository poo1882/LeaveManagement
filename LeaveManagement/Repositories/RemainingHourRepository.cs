using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
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
            RemainingHour remainingHours = _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId);
            if (type.ToLower()[0] == 'a')
            {
                remainingHours.AnnualHours -= totalHours;
            }
            else if (type.ToLower()[0] == 's')
            {
                remainingHours.SickHours -= totalHours;
            }
            else if (type.ToLower()[0] == 'l')
            {
                remainingHours.LWPHours -= totalHours;
            }
            _dbContext.SaveChanges();
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

        public void GenerateHours(RemainingHour remaining)
        {
            _dbContext.RemainingHours.Add(remaining);
            _dbContext.SaveChanges();
        }
        

        public bool InitRemainingHours(string password)
        {
            if (password != "init")
                return false;
            var lines = File.ReadAllLines("C:\\Users\\poo1882\\Desktop\\remainingHours.csv").Select(a => a.Split(','));
            foreach (var item in lines)
            {
                RemainingHour remainingHour = new RemainingHour(item[0], item[1], Convert.ToInt32(item[2]), Convert.ToInt32(item[3]));
                _dbContext.RemainingHours.Add(remainingHour);
            }

            _dbContext.SaveChanges();
            return true;
        }

        public List<RemainingHour> GetRemainingHours()
        {
            var result = from remaininglist in _dbContext.RemainingHours
                         join employeelist in _dbContext.Employees on remaininglist.StaffId equals employeelist.StaffId
                         where employeelist.IsActive == true
                         select remaininglist;
            return result.OrderByDescending(x => x.StaffId).ToList();
        }

        public void ClearRemainingHours()
        {
            var remainingHours = _dbContext.RemainingHours;
            foreach (var item in remainingHours)
            {
                remainingHours.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}
