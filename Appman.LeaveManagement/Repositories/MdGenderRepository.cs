using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class MdGenderRepository
    {
        LeaveManagementDbContext _dbContext;
        public MdGenderRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MdGender mdGender)
        {
            _dbContext.MdGenders.Add(mdGender);
            _dbContext.SaveChanges();
        }
    }
}
