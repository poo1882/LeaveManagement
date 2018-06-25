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

        public void Add(LeaveInfo leaveInfo)
        {
            _dbContext.LeaveInfos.Add(leaveInfo);
            _dbContext.SaveChanges();
        }


        public void Delete(string email)
        {
            //_dbContext.LeaveInfos.Remove(_dbContext.LeaveInfos.FirstOrDefault(x => x.Email == email));
            //_dbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            //var emp = _dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);


            //emp.FirstName = employee.FirstName??emp.FirstName;
            //emp.Lastname = employee.Lastname??emp.Lastname;
            //emp.Email = employee.Email;
            //emp.Role = employee.Role;
            //emp.ProfilePicture = employee.ProfilePicture;


            //_dbContext.Employees.Update(emp);
            //_dbContext.SaveChanges();
        }

    }
}
