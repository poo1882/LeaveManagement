using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
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
    public class HistoryController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public HistoryController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
        }

        [Route("Leaves")] //ดู history
        [HttpGet]
        public IActionResult ViewLeaves()
        {
            List<LeaveInfo> leaves = _leaveRepo.GetHistory();
            var ordered = leaves
               .OrderByDescending(v => v.ApprovalStatus.ToLower() == "pending")
               .ThenByDescending(v => v.ApprovalStatus.ToLower() == "approved")
               .ThenByDescending(v => v.ApprovalStatus.ToLower() == "rejected")
               .ThenByDescending(v => v.ApprovedTime)
               .ThenBy(v => v.LeaveId);
            if (leaves == null)
                return NotFound();

            return Content(JsonConvert.SerializeObject(ordered), "application/json");
        }

        [Route("Info")] //ดู form การลาของแต่ละคน
        [HttpGet]
        public IActionResult ViewLeaveInfo([FromQuery]int leaveId)
        {
            var leave = _leaveRepo.ViewLeaveInfo(leaveId);
            if (leave == null)
                return NotFound();
            return Content(JsonConvert.SerializeObject(leave), "application/json");
        }

        //[Route("History")] // ดู history ของทุกคน
        //[HttpGet]
        //public IActionResult GetHistory()
        //{
        //    var list = _leaveRepo.GetHistory();
        //    return Ok(list);
        //}

        [Route("History")] // ดู history ของแต่ละคน
        [HttpGet]
        public IActionResult GetHistory([FromQuery]string staffId)
        {
            var list = _leaveRepo.GetHistory(staffId);
            var ordered = list
               .OrderByDescending(v => v.ApprovalStatus.ToLower() == "pending")
               .ThenByDescending(v => v.ApprovalStatus.ToLower() == "approved")
               .ThenByDescending(v => v.ApprovalStatus.ToLower() == "rejected")
               .ThenByDescending(v => v.ApprovedTime)
               .ThenBy(v => v.LeaveId);

            if (list == null)
                return NotFound();

            return Content(JsonConvert.SerializeObject(ordered), "application/json");
        }

        [Route("History")]
        [HttpDelete]
        public IActionResult ClearLeaveHistory()
        {
            _leaveRepo.ClearLeaveHistory();
            return Ok();
        }


    }
}
