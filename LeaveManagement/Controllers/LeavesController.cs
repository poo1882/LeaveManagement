using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class LeavesController : Controller
    {
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly LeaveManagementDbContext _dbContext;
        private readonly EmployeeRepository _empRepo;

        public LeavesController(LeaveManagementDbContext leaveInfo)
        {
            _dbContext = leaveInfo;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _empRepo = new EmployeeRepository(_dbContext);
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
                emailSender.SendRequestMailToApprover(approbations, info);
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
        public IActionResult SetStatus([FromQuery]string status, int leaveId, string approverId)
        {
            if (status.ToLower() != "approved" && status.ToLower() != "rejected")
                return NotFound();
            if (_leaveRepo.SetStatus(status.ToLower(),leaveId, approverId))
            {
                EmailController emailSender = new EmailController(_dbContext);
                emailSender.SendResultToOwner(approverId, status,_leaveRepo.ViewLeaveInfo(leaveId));
                if (status.ToLower()[0] == 'a')
                    return Ok("Approved successfully");
                return Ok("Rejected successfully");
            }

            else
            {
                LeaveInfo leave = _leaveRepo.ViewLeaveInfo(leaveId);
                string approverName = _empRepo.GetName(leave.ApprovedBy);
                return Ok("Sorry, this leaving form has been already approved/rejected by " + approverName + ".");
            }
        }

        [Route("DeleteByAdmin")]
        [HttpPut]
        public IActionResult SetToDeleted([FromQuery]int leaveId, string commentByAdmin)
        {
            if (_leaveRepo.SetDeleted(leaveId, commentByAdmin))
                return Ok();
            return NotFound();

        }

        [Route("ApproveViaEmail")]
        [HttpGet]
        public IActionResult ApproveViaEmail([FromQuery] string refNo)
        {
            Approbation approbation = _dbContext.Approbations.FirstOrDefault(x => x.ApprobationGuid.ToString() == refNo);

            if (approbation == null)
                return Ok("Sorry, this leaving form has been already approved/rejected by another approver.");
            var leaveInfo = _leaveRepo.ViewLeaveInfo(approbation.LeaveId);
            string approverId = approbation.ApproverId;
            string status = approbation.Status;
            if (_leaveRepo.SetStatus(status, leaveInfo.LeaveId, approverId))
            {
                EmailController emailSender = new EmailController(_dbContext);
                emailSender.SendResultToOwner(approverId, status, leaveInfo);
                if (status.ToLower()[0] == 'a')
                    return Ok("Approved successfully");
                return Ok("Rejected successfully");
            }

            else
            {
                LeaveInfo leave = _leaveRepo.ViewLeaveInfo(leaveInfo.LeaveId);
                string approverName = _empRepo.GetName(leave.ApprovedBy);
                return Ok("Sorry, this leaving form has been already approved/rejected by " + approverName + ".");
            }

        }

    }
}
