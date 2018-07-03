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
            return Ok(leaves);
        }

        [Route("{id}/Info")] //ดู form การลาของแต่ละคน
        [HttpGet]
        public IActionResult ViewLeaveInfo(string leaveId)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.ViewLeaveInfo(leaveId));
            return Ok(emp);
        }

        //[Route("History")] // ดู history ของทุกคน
        //[HttpGet]
        //public IActionResult GetHistory()
        //{
        //    var list = _leaveRepo.GetHistory();
        //    return Ok(list);
        //}

        [Route("{employeeId}/History")] // ดู history ของแต่ละคน
        [HttpGet]
        public IActionResult GetHistory(string staffId)
        {
            var list = _leaveRepo.GetHistory(staffId);
            return Ok(list);
        }


    }
}
