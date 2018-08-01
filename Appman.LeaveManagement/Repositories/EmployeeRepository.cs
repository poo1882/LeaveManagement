using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class EmployeeRepository
    {
        LeaveManagementDbContext _dbContext;
        private readonly MdRoleRepository _mdRoleRepository;

        public EmployeeRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _mdRoleRepository = new MdRoleRepository(_dbContext);
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
            if (emp == null)
                return null;
            if (emp.IsActive == false)
                return null;
            return emp;
        }


        public List<Employee> GetEmployees()
        {
            List<Employee> result = _dbContext.Employees.Where(x => x.IsActive == true).OrderByDescending(y => y.StaffId).ToList();
            return result;
        }

        public string GetRole(string staffId)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId);
            var role = _mdRoleRepository.GetRole(employee.RoleCode);
            
            if (role.IsAdmin)
                return "admin";
            if (_dbContext.Reportings.Any(x => x.Approver == staffId))
                return "approver";
            return "normal";
        }

        public string GetName(string staffId)
        {
            var employee = GetProfile(staffId);
            return employee.FirstNameEN + " " + employee.LastNameEN;

        }

        public string GetEmail(string staffId)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId).Email;
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

        public bool InitializeEmployees(string password)
        {
            
            //RemainingHourRepository remRepo = new RemainingHourRepository(_dbContext);
            //AddEmployee(new Employee
            //{
            //    StaffId = "00007",
            //    FirstNameTH = "Gun",
            //    LastNameTH = "Sirapob",
            //    Email = "sirapobmech@gmail.com",
            //    ProfilePicture = null,
            //    RoleCode = "Frontend",
            //    IsActive = true,
            //    GenderCode = "G01",
            //});

            //RemainingHour remain = new RemainingHour(100, "00007", DateTime.Now.Year.ToString());
            //remRepo.GenerateHours(remain);

            if (password != "init")
                return false;
            var lines = File.ReadAllLines("C:\\Users\\poo1882\\Desktop\\employees.csv").Select(a => a.Split(','));
            foreach (var item in lines)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i] == null)
                        item[i] = "";
                }

                Employee employee = new Employee
                {
                    StaffId = item[0],
                    FirstNameTH = item[1],
                    LastNameTH = item[2],
                    FirstNameEN = item[3],
                    LastNameEN = item[4],
                    Nickname = item[5],
                    Email = item[6],
                    GenderCode = item[7],
                    RoleCode = item[8],
                    ProfilePicture = item[9],
                };

                if (item[10].ToLower() == "true")
                    employee.IsActive = true;
                else
                    employee.IsActive = false;
                _dbContext.Employees.Add(employee);
            }

            _dbContext.SaveChanges();
            return true;
        }
    }
}
