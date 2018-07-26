using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult AddApprover([FromBody]Reporting reporting)
        {
            if(_repRepo.AddApprover(reporting))
                return Created("", reporting);
            return NotFound();

        }

        [Route("Reporting")]
        [HttpDelete]
        public IActionResult RemoveApprover([FromBody]Reporting reporting)
        {
            if (_repRepo.RemoveApprover(reporting))
                return Ok();
            return NotFound();
        }

        [Route("Reporting")]
        [HttpGet]
        public IActionResult ViewAllReporting()
        {
            var result = _repRepo.ViewAllReporting();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Route("Reportings")]
        [HttpPut]
        public IActionResult InitReportings([FromQuery] string password)
        {

            if (!_repRepo.InitReportings(password.ToLower()))
                return Ok("Duplicated initialization was rejected.");
            return Ok("Initialize reportings successfully.");
        }

        [Route("Reportings")]
        [HttpDelete]
        public IActionResult ClearReportings()
        {
            _repRepo.ClearReportings();
            return Ok("Clear reportings successfully.");
        }

    }
}
