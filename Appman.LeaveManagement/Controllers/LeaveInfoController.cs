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
    public class LeaveInfoController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveInfoController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
        }


        [Route("LeaveInfo")]
        [HttpGet]
        public IActionResult GetLeaveInfo([FromQuery] Guid id)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.ViewForm(id));
            return Ok(emp);
        }

        [Route("Form")]
        [HttpPost]
        public IActionResult CreateLeaveInfo([FromBody] LeaveInfo info)
        {
            var emp = JsonConvert.SerializeObject(_leaveRepo.CreateForm(info));
            return Created("",emp);
        }

        [Route("Remain")]
        [HttpGet]
        public IActionResult GetRemaining([FromQuery]Guid employeeId)
        {
            var leave = _leaveRepo.GetRemaining(employeeId);
            return Ok(leave);
        }


    }
}