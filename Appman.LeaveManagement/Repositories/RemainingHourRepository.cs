using Appman.LeaveManagement.DatabaseContext;
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
        //public int ViewSickHour(Guid id, string year)
        //{
        //    var sickHours = _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).SickHours;
        //    return sickHours;
        //}

        //public int ViewAnnualHour(Guid id, string year)
        //{
        //    var AnnualHours = _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).AnnualHours;
        //    return AnnualHours;
        //}

        //public int ViewLWPHour(Guid id,string year)
        //{
        //    var LWPHours = _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).LWPHours;
        //    return LWPHours;
        //}

        public int ViewHour(Guid id, string year,string type)
        {
            type = type.ToLower();
            switch (type[0])
            {
                case 'a':
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).AnnualHours;
                case 's':
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).SickHours;
                default:
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.EmployeeId == id && x.Year == year).LWPHours;
            }
                
                
        }
    }
}
