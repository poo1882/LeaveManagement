using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Appman.LeaveManagement.Controllers
{
    [Route("/api/[Controller]")]
    public class ReportingController : Controller
    {
        private readonly ReportingRepository _repRepo;
        private readonly LeaveManagementDbContext _dbContext;
        public ReportingController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _repRepo = new ReportingRepository(_dbContext);
        }

        [Route("Reporting")]
        [HttpPost]
        public IActionResult AddApprover(Reporting reporting)
        {
            _repRepo.AddApprover(reporting);
            return Created("",reporting);
        }

        [Route("Reporting")]
        [HttpDelete]
        public IActionResult RemoveApprover(Reporting reporting)
        {
            _repRepo.RemoveApprover(reporting);
            return Ok();
        }
    }
}
