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
        [Route("AddEmployee")]
        public IActionResult AddEmployee(Employee employee)
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
                IsActive = employee.IsActive
            });

            return Ok(id);

        }

        [Route("DeleteEmployee")]
        [HttpGet]
        public IActionResult DeleteEmployee([FromQuery] string email)
        {


            _empRepo.Delete(email);

            return Ok();

        }

        [Route("GetProfile")]
        public IActionResult GetProfile([FromQuery] string email)
        {
            var emp = JsonConvert.SerializeObject(_empRepo.GetProfile(email));
            return Content(emp,"application/json");
        }





    }
}
