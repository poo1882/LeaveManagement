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
        [HttpPost]
        [Route("Employee")]
        public IActionResult AddEmployee([FromQuery] Employee employee)
        {

            var id = Guid.NewGuid();
            employee.Id = id;
            _empRepo.Add(new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                ProfilePicture = employee.ProfilePicture,
                Position = employee.Position,
                IsActive = true
            });

            return Ok(id);

        }

        [Route("Employee")]
        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery] Guid id)
        {


            _empRepo.Delete(id);

            return Ok();

        }

        [Route("Employee")]
        [HttpGet]
        public IActionResult GetProfile([FromQuery] Guid id)
        {
            var emp = JsonConvert.SerializeObject(_empRepo.GetProfile(id));
            return Content(emp, "application/json");
        }

    }
}
