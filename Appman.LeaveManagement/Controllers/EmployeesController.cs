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
    public class EmployeesController : Controller
    {
        private readonly EmployeeRepository _empRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public EmployeesController(LeaveManagementDbContext leaveManagementDbContext)
        {

            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
        }

        [Route("Employees")]
        [HttpGet]
        public IActionResult ViewAllEmployee()
        {
            List<Employee> result = _empRepo.GetEmployees();
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
