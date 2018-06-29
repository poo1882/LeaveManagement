using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
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
        public IActionResult addReporting(Reporting reporting)
        {
            _repRepo.addReportingTo(reporting);
            return Created("",reporting);
        }

        [Route("Reporting")]
        [HttpDelete]
        public IActionResult removeReporting(Reporting reporting)
        {
            _repRepo.removeReporting(reporting);
            return Ok();
        }
    }
}
