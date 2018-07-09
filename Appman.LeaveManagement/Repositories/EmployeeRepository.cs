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

        /// <summary>
        ///     Add employee to database
        /// </summary>
        /// <param name="employee">An employee to be added</param>
        /// <returns>
        ///     true - if success
        ///     false - if already exist
        /// </returns>
        public bool AddEmployee(Employee employee)
        {
            if (_dbContext.Employees.Contains(employee))
                return false;
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Delete employee in database
        /// </summary>
        /// <param name="staffId">An id of the one who will be deleted</param>
        /// <returns>
        ///     true - if success
        ///     false - if no id found
        /// </returns>
        public bool DeleteEmployee(string staffId)
        {
            if (_dbContext.Employees.Where(x => x.StaffId == staffId) == null)
                return false;
            _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId).IsActive = false;
            _dbContext.SaveChanges();
            return true;
        }


        /// <summary>
        ///     Update information of employee
        /// </summary>
        /// <param name="employee">A new employee's information to replace</param>
        /// <returns>
        ///     true - if success
        ///     false - if no employee found to be updated
        /// </returns>
        public bool UpdateProfile(Employee employee)
        {
            if (!_dbContext.Employees.Contains(employee))
                return false;
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return true;
        }


        /// <summary>
        ///     Get basic information of the employee
        /// </summary>
        /// <param name="staffId">An id of the employee</param>
        /// <returns>
        ///     Employee - An instance of the employee
        ///     null - if no employee found
        /// </returns>
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

        public void ClearEmployees()
        {
            var employees = _dbContext.Employees;
            foreach (var item in employees)
            {
                employees.Remove(item);
            }
            _dbContext.SaveChanges();
        }
    }
}
