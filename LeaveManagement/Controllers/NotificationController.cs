using LeaveManagement.DatabaseContext;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class NotificationController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly EmployeeRepository _empRepo;

        /// <summary>
        ///     Initialize EmployeeController with a specific database context
        /// </summary>
        /// <param name="leaveManagementDbContext">the targeted database context</param>
        public NotificationController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
        }

        [Route("Notification")]
        [HttpGet]
        public IActionResult GetNotification([FromQuery]string staffId)
        {
            var result = _empRepo.GetNotification(staffId);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Route("Notification")]
        [HttpDelete]
        public IActionResult RemoveNotification([FromQuery]string staffId)
        {
            _empRepo.RemoveNotification(staffId);
            return Ok();
        }
    }
}
