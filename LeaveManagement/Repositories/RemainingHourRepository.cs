using LeaveManagement.DatabaseContext;
using System;
using System.Collections.Generic;
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
