using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
{
    public class ReportingRepository
    {
        LeaveManagementDbContext _dbContext;
        public ReportingRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void addReportingTo(Reporting reporting)
        {
            _dbContext.Reportings.Add(reporting);
            _dbContext.SaveChanges();
        }

        public void removeReporting(Reporting reporting)
        {
            _dbContext.Reportings.Remove(reporting);
            _dbContext.SaveChanges();
        }
    }
}
