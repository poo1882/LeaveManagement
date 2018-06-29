using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
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


        public void Delete(Guid id)
        {
            _dbContext.Employees.FirstOrDefault(x => x.Id == id).IsActive = false;
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

        public Employee GetProfile(Guid id)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> result = _dbContext.Employees.Where(x => x.IsActive == true).ToList();
            return result;
        }
    }
}
