using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
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
    }
}
