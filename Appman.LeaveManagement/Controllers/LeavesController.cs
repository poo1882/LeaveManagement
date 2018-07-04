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
            return Content(JsonConvert.SerializeObject(leaves), "application/json");
        }

        [Route("Leave")] // create leave form 
        [HttpPost]
        public IActionResult CreateLeaveInfo([FromBody] LeaveInfo info)
        {
            bool isCreated = _leaveRepo.CreateLeaveInfo(info);
            if (isCreated)
            {
                EmailController emailSender = new EmailController();
                List<Reporting> reporting = _dbContext.Reportings.Where(x => x.StaffId == info.StaffId).ToList();
                
                foreach (var item in reporting)
                {
                    emailSender.SendRequestMailToApprover(_dbContext.Employees.FirstOrDefault(x => x.StaffId == item.Approver).Email);
                }

                emailSender.SendRequestMailToOwner(_dbContext.Employees.FirstOrDefault(x => x.StaffId == info.StaffId).Email);
                return Ok();
            }
            return new EmptyResult();
            
        }

        [Route("RemainingLeaveInfo")] // ดูว่าใครเหลือกี่ชั่วโมงในแต่ละประเภท
        [HttpGet]
        public IActionResult GetRemaining(string staffId)
        {
            var leave = _leaveRepo.GetRemaining(staffId);
            return Content(JsonConvert.SerializeObject(leave), "application/json");
        }


        [Route("SetStatus")]
        [HttpPut]
        public IActionResult SetStatus([FromQuery]string status,string leaveId,string approverId)
        {
            if(_leaveRepo.SetStatus(status, leaveId, approverId))
            {
                return Ok();
            }
            return new EmptyResult();            
                
        }

    }
}
