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
            if (employee.IsAdmin)
                return "Admin";
            if (_dbContext.Reportings.Any(x => x.Approver == staffId))
                return "Approver";
            return "Normal";
        }

        public string GetName(string staffId)
        {
            var employee = GetProfile(staffId);
            return employee.FirstNameTH + " " + employee.LastNameTH;

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
            var reportings = _dbContext.Reportings;
            foreach (var item in _dbContext.Reportings)
            {
                reportings.Remove(item);
            }
            var remainingHours = _dbContext.RemainingHours;
            foreach (var item in remainingHours)
            {
                remainingHours.Remove(item);
            }

            _dbContext.SaveChanges();
        }

        public bool InitializeEmployees(string password)
        {
            if (password != "init")
                return false;
            RemainingHourRepository remRepo = new RemainingHourRepository(_dbContext);
            AddEmployee(new Employee
            {
                StaffId = "00007",
                FirstNameTH = "Gun",
                LastNameTH = "Sirapob",
                Email = "sirapobmech@gmail.com",
                ProfilePicture = null,
                RoleCode = "Frontend",
                IsActive = true,
                GenderCode = "G01",
            });

            RemainingHour remain = new RemainingHour(100, "00007", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00008",
                FirstNameTH = "Knack",
                LastNameTH = "Kasidis",
                Email = "kasidis.sr@mail.kmutt.ac.th",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00008", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00006",
                FirstNameTH = "Poo",
                LastNameTH = "Siriwimon",
                Email = "poo_poo1882@hotmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00006", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00005",
                FirstNameTH = "Jenny",
                LastNameTH = "Supornthip",
                Email = "supornthip.s@appman.co.th",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00005", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00001",
                FirstNameTH = "Tangkwa",
                LastNameTH = "Puttachart",
                Email = "psrisuwankum@gmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00001", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00002",
                FirstNameTH = "Jill",
                LastNameTH = "Titinan",
                Email = "jilltitinan@gmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00002", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00003",
                FirstNameTH = "Got",
                LastNameTH = "Supakit",
                Email = "supakit.dha@hotmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00003", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00004",
                FirstNameTH = "Stamp",
                LastNameTH = "Notphattri",
                Email = "notphattri.stamp@hotmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00004", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);

            AddEmployee(new Employee
            {
                StaffId = "00009",
                FirstNameTH = "Puen",
                LastNameTH = "Nuttasit",
                Email = "nuttasit10@gmail.com",
                ProfilePicture = null,
                RoleCode = "Business Analyst",
                IsActive = true,
                GenderCode = "G01",
            });
            remain = new RemainingHour(100, "00009", DateTime.Now.Year.ToString());
            remRepo.GenerateHours(remain);
            return true;
        }
    }
}
