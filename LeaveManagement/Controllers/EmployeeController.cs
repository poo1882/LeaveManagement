using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using LeaveManagement.Models;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _empRepo;
        private readonly LeaveManagementDbContext _dbContext;
        private readonly RemainingHourRepository _remRepo;
        private readonly MdRoleRepository _mdRoleRepo;

        /// <summary>
        ///     Initialize EmployeeController with a specific database context
        /// </summary>
        /// <param name="leaveManagementDbContext">the targeted database context</param>
        public EmployeeController(LeaveManagementDbContext leaveManagementDbContext)
        {

            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
            _remRepo = new RemainingHourRepository(_dbContext);
            _mdRoleRepo = new MdRoleRepository(_dbContext);
        }

        /// <summary>
        ///     Add an employee to database
        /// </summary>
        /// <param name="employee">An employee to be added</param>
        /// <returns>
        ///     Ok - if success
        ///     null - if the employee is already exist
        /// </returns>
        [Route("Employee")]
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (_empRepo.AddEmployee(employee))
            {
                return Ok(employee.StaffId);
            }
            return NotFound();
        }

        /// <summary>
        ///     Delete an employee (Actually set IsActive to false)
        /// </summary>
        /// <param name="staffId">An id of the one who will be deleted</param>
        /// <returns>
        ///     Ok - if success
        ///     null - if no employee was found
        /// </returns>
        [Route("Employee")]
        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery] string staffId)
        {
            if (_empRepo.DeleteEmployee(staffId))
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Get basic information of an employee
        /// </summary>
        /// <param name="staffId">An id of the employee</param>
        /// <returns>
        ///     Employee - An instance of the employee
        ///     null - if no id match
        /// </returns>
        [Route("Employee")]
        [HttpGet]
        public IActionResult GetProfile([FromQuery] string staffId)
        {
            var emp = _empRepo.GetProfile(staffId);
            //var emp = JsonConvert.SerializeObject();
            if (emp == null)
                return NotFound();
            return Content(JsonConvert.SerializeObject(emp), "application/json");

        }

        /// <summary>
        ///     Update the information of an employee
        /// </summary>
        /// <param name="employee">The targeted employee</param>
        /// <returns>
        ///     Ok - if success
        ///     EmptyResult - if no employee found
        /// </returns>
        [Route("Employee")]
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] Employee employee)
        {
            if (_empRepo.UpdateProfile(employee))
                return Ok("Updated successfully");
            return NotFound();
        }

        [Route("GetEmployeeId")]
        [HttpGet]
        public IActionResult GetEmployeeId([FromQuery]string email)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            if (emp != null)
                return Content(JsonConvert.SerializeObject(emp.StaffId), "application/json");
            else
                return NotFound();
        }

        [Route("Role")]
        [HttpGet]
        public IActionResult GetRole([FromQuery] string staffId)
        {
            return Content(JsonConvert.SerializeObject(_empRepo.GetRole(staffId)),"application/json");
        }

        [Route("Header")]
        [HttpGet]
        public IActionResult GetHeader ([FromQuery] string email)
        {
            string staffId = _dbContext.Employees.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()).StaffId;
            Header result = new Header();
            var emp = _empRepo.GetProfile(staffId);
            var roleSet = _mdRoleRepo.GetRole(emp.RoleCode);
            result.ProfilePicture = emp.ProfilePicture;
            result.Name = _empRepo.GetName(staffId);
            result.Position = roleSet.Position;
            result.Department = roleSet.Department;
            result.StaffID = staffId;
            result.Role = _empRepo.GetRole(staffId);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

    }
}
