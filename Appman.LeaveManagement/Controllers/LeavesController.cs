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
    public class LeavesController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public LeavesController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
        }

        [Route("Leaves")]
        [HttpGet]
        public IActionResult ViewLeaves()
        {
            List<LeaveInfo> leaves = _leaveRepo.GetHistory();
            return Ok(leaves);
        }

        [Route("Leave")] // create leave form 
        [HttpPost]
        public IActionResult CreateLeaveInfo([FromBody] LeaveInfo info)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.CreateLeaveInfo(info));
            return Created("", emp);
        }

        [Route("RemainingLeaveInfo")] // ดูว่าใครเหลือกี่ชั่วโมงในแต่ละประเภท
        [HttpGet]
        public IActionResult GetRemaining(string staffId)
        {
            var leave = _leaveRepo.GetRemaining(staffId);
            return Ok(leave);
        }

    }
}
