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


        public void Delete(string staffId)
        {
            _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId).IsActive = false;
            _dbContext.SaveChanges();
        }

        //public void Update(Employee employee)
        //{
        //    //var emp = _dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);


        //    //emp.FirstName = employee.FirstName ?? emp.FirstName;
        //    //emp.Lastname = employee.Lastname ?? emp.Lastname;
        //    //emp.Email = employee.Email;
        //    //emp.Role = employee.Role;
        //    //emp.ProfilePicture = employee.ProfilePicture;


        //    _dbContext.Employees.Update(employee);
        //    _dbContext.SaveChanges();
        //}

        public Employee GetProfile(string staffId)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId);
            if (emp.IsActive == false)
                return null;
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> result = _dbContext.Employees.Where(x => x.IsActive == true).ToList();
            return result;
        }
    }
}
