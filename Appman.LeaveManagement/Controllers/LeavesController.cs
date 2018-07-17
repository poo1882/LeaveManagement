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
        
        //[Route("Leaves")]
        //[HttpGet]
        //public IActionResult ViewLeaves()
        //{
        //    List<LeaveInfo> leaves = _leaveRepo.GetHistory();
        //    return Content(JsonConvert.SerializeObject(leaves), "application/json");
        //}

        [Route("Leave")] // create leave form 
        [HttpPost]
        public IActionResult CreateLeaveInfo([FromBody] LeaveInfo info)
        {
            bool isCreated = _leaveRepo.CreateLeaveInfo(info);
            if (isCreated)
            {
                List<Approbation> approbations = new List<Approbation>();
                approbations = _leaveRepo.CreateApprobationSet(info);
                _leaveRepo.AddApprobation(approbations);
                EmailController emailSender = new EmailController(_dbContext);
                emailSender.SendRequestMailToApprover(approbations,info);
                emailSender.SendRequestMailToOwner(_dbContext.Employees.FirstOrDefault(x => x.StaffId == info.StaffId).Email, info);
                return Ok();
            }
            return NotFound();
            
        }

        [Route("RemainingLeaveInfo")] // ดูใบที่รอการอนุมัติ
        [HttpGet]
        public IActionResult GetRemaining([FromQuery]string staffId)
        {
            var leave = _leaveRepo.GetRemaining(staffId);
            if (leave == null)
                return NotFound();
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
            return NotFound();            
                
        }

        [Route("ApproveViaEmail")]
        [HttpGet]
        public IActionResult ApproveViaEmail([FromQuery] string refNo)
        {
            Approbation approbation = _dbContext.Approbations.FirstOrDefault(x => x.ApprobationGuid.ToString() == refNo);
            
            int leaveId = approbation.LeaveId;
            string approverId = approbation.ApproverId;
            string status = approbation.Status;
            if (_leaveRepo.SetStatus(status, leaveId, approverId))
            {
                EmailController emailSender = new EmailController(_dbContext);
                emailSender.SendResultToOwner(_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).StaffId,leaveId,approverId,status);
                if(status.ToLower()[0] == 'a')
                    return Ok("Approved successfully");
                return Ok("Rejected successfully");
            }
                
            else
                return Ok("Sorry, this leaving form has been already approved/rejected by another approver.");
        }

    }
}
