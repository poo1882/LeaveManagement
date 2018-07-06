using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                EmailController emailSender = new EmailController(_dbContext);
                List<Reporting> reporting = _dbContext.Reportings.Where(x => x.StaffId == info.StaffId).ToList();
                
                foreach (var item in reporting)
                {
                    emailSender.SendRequestMailToApprover(_dbContext.Employees.FirstOrDefault(x => x.StaffId == item.Approver).Email,info.LeaveId);
                }

                emailSender.SendRequestMailToOwner(_dbContext.Employees.FirstOrDefault(x => x.StaffId == info.StaffId).Email);
                return Ok();
            }
            return new EmptyResult();
            
        }

        [Route("RemainingLeaveInfo")] // ดูว่าใครเหลือกี่ชั่วโมงในแต่ละประเภท
        [HttpGet]
        public IActionResult GetRemaining([FromQuery]string staffId)
        {
            var leave = _leaveRepo.GetRemaining(staffId);
            return Content(JsonConvert.SerializeObject(leave), "application/json");
        }


        [Route("SetStatus")]
        [HttpPut]
        public IActionResult SetStatus([FromQuery]string status,int leaveId,string approverId)
        {
            if(_leaveRepo.SetStatus(status, leaveId, approverId))
            {
                return Ok();
            }
            return new EmptyResult();            
                
        }

        [Route("ApproveViaEmail")]
        [HttpGet]
        public IActionResult ApproveViaEmail([FromQuery] string refNo1,string refNo2,string refNo3)
        {
            string status = refNo3;
            int leaveId = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveGuid.ToString() == refNo1).LeaveId;
            string approverId = _dbContext.Employees.FirstOrDefault(x => x.StaffGuId.ToString() == refNo2).StaffId;
            if (_leaveRepo.SetStatus(status, leaveId, approverId))
                return Ok();
            else
                return new EmptyResult();
        }
        

    }
}
