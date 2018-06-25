using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class LeaveInfoController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveInfoController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("GetLeaveInfo")]
        public IActionResult GetLeaveInfo([FromQuery] Guid id)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.ViewForm(id));
            return Content(emp, "application/json");
        }
    }
}