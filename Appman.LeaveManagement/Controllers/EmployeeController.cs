using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
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
        public EmployeeController(LeaveManagementDbContext leaveManagementDbContext)
        {

            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
        }
        
        [Route("Employee")]
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            _empRepo.Add(new Employee
            {
                StaffId = employee.StaffId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                ProfilePicture = employee.ProfilePicture,
                Position = employee.Position,
                IsActive = employee.IsActive
            });

            return Ok(employee.StaffId);

        }

        [Route("Employee")]
        [HttpDelete]
        public IActionResult DeleteEmployee([FromBody] string staffId)
        {


            _empRepo.Delete(staffId);

            return Ok();

        }

        [Route("Employee")]
        [HttpGet]
        public IActionResult GetProfile([FromBody] string staffId)
        {
            var emp = JsonConvert.SerializeObject(_empRepo.GetProfile(staffId));
            return Content(emp, "application/json");
        }

    }
}
