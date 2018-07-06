using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class ApprobationRepository
    {
        LeaveManagementDbContext _dbContext;
        public ApprobationRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Approbation> ViewAllApprobations()
        {
            var result = _dbContext.Approbations.ToList();
            return result;
        }
    }
}
