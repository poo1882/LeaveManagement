using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class EmployeeRepository
    {
        LeaveManagementDbContext _dbContext;
        public EmployeeRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Employee employee)
        {


            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();


        }


        public void Delete(string email)
        {
            //_dbContext.Employees.Remove(_dbContext.Employees.FirstOrDefault(x => x.Email == email));
            _dbContext.Employees.FirstOrDefault(x => x.Email == email).IsActive = false;
            _dbContext.SaveChanges();
        }

        public void Update(Employee employee)
        {
            //var emp = _dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);


            //emp.FirstName = employee.FirstName ?? emp.FirstName;
            //emp.Lastname = employee.Lastname ?? emp.Lastname;
            //emp.Email = employee.Email;
            //emp.Role = employee.Role;
            //emp.ProfilePicture = employee.ProfilePicture;


            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        public Employee GetProfile(string email)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.Email == email);
            return emp;
        }

    }
}
