using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class MdRoleRepository
    {
        LeaveManagementDbContext _dbContext;
        public MdRoleRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MdRole mdRole)
        {
            _dbContext.MdRoles.Add(mdRole);
            _dbContext.SaveChanges();
        }

        public MdRole GetRole (string roleCode)
        {
            return _dbContext.MdRoles.FirstOrDefault(x => x.RoleCode == roleCode);
        }
    }
}
