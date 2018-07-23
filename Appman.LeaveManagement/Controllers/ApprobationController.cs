using Appman.LeaveManagement.DatabaseContext;
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
    public class ApprobationController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly ApprobationRepository _appRepo;

        /// <summary>
        ///     Initialize EmployeeController with a specific database context
        /// </summary>
        /// <param name="leaveManagementDbContext">the targeted database context</param>
        public ApprobationController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _appRepo = new ApprobationRepository(_dbContext);
        }

        [Route("Approbation")]
        [HttpGet]
        public IActionResult ViewAllApprobations()
        {
            var result = _appRepo.ViewAllApprobations();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
