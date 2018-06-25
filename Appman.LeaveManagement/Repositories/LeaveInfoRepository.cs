using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class LeaveInfoRepository
    {
        LeaveManagementDbContext _dbContext;
        public LeaveInfoRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LeaveInfo ViewForm(Guid form)
        {
            var emp = _dbContext.LeaveInfos.ToList();//.FirstOrDefault(x => x.Id == form);
            return emp!=null && emp.Count>0? emp.First(): new LeaveInfo() ;
        }
        public void Add(LeaveInfo leaveInfo)
        {
            _dbContext.LeaveInfos.Add(leaveInfo);
            _dbContext.SaveChanges();
        }

       

    }
}
