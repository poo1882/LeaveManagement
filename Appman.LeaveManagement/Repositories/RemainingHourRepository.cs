﻿using Appman.LeaveManagement.DatabaseContext;
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

        public int ViewHour(string staffId, string year,string type)
        {
            type = type.ToLower();
            switch (type[0])
            {
                case 'a':
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).AnnualHours;
                case 's':
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).SickHours;
                default:
                    return _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == staffId && x.Year == year).LWPHours;
            }
                
                
        }
    }
}
