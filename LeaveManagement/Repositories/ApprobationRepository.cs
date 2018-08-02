using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
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

        public void ClearApprobations()
        {
            var approbations = _dbContext.Approbations;
            foreach (var item in approbations)
            {
                approbations.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}
