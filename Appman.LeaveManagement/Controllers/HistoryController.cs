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
            if (leaves == null)
                return new EmptyResult();
            return Ok(JsonConvert.SerializeObject(leaves));
        }

        [Route("Info")] //ดู form การลาของแต่ละคน
        [HttpGet]
        public IActionResult ViewLeaveInfo([FromQuery]string leaveId)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.ViewLeaveInfo(leaveId));
            if (emp == null)
                return new EmptyResult();
            return Ok(JsonConvert.SerializeObject(emp));
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
            if (list == null)
                return new EmptyResult();
            return Ok(JsonConvert.SerializeObject(list));
        }


    }
}
