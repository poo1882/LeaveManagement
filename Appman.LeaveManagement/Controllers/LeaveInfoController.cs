using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class LeaveController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
        }


        [Route("{id}/Info")]
        [HttpGet]
        public IActionResult ViewLeaveInfo(Guid id)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.ViewLeaveInfo(id));
            return Ok(emp);
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult CreateLeaveInfo([FromBody] LeaveInfo info)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.CreateLeaveInfo(info));
            return Created("",emp);
        }

        [Route("{employeeId}/RemainingLeaveInfo")]
        [HttpGet]
        public IActionResult GetRemaining(Guid employeeId)
        {
            var leave = _leaveRepo.GetRemaining(employeeId);
            return Ok(leave);
        }

        [Route("History")]
        [HttpGet]
        public IActionResult GetHistory()
        {
            var list = _leaveRepo.GetHistory();
            return Ok(list);
        }

        [Route("{employeeId}/History")]
        [HttpGet]
        public IActionResult GetHistory(Guid employeeId)
        {
            var list = _leaveRepo.GetHistory(employeeId);
            return Ok(list);
        }
        
        

    }
}