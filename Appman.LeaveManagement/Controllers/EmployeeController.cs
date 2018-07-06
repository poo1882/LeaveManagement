using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _empRepo;
        private readonly LeaveManagementDbContext _dbContext;
        private readonly RemainingHourRepository _remRepo;

        /// <summary>
        ///     Initialize EmployeeController with a specific database context
        /// </summary>
        /// <param name="leaveManagementDbContext">the targeted database context</param>
        public EmployeeController(LeaveManagementDbContext leaveManagementDbContext)
        {

            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
            _remRepo = new RemainingHourRepository(_dbContext);
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
            if(_empRepo.AddEmployee(employee))
            {
                RemainingHour remain = new RemainingHour(100, employee.StaffId, DateTime.Now.Year.ToString());
                _remRepo.generateHours(remain);
                return Ok(employee.StaffId);
            }
            return new EmptyResult();
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
            return new EmptyResult();
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
                return new EmptyResult();
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
                return Ok();
            return new EmptyResult();
        }

    }
}
