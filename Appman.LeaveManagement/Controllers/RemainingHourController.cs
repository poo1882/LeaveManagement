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
    public class RemainingHourController : Controller
    {
        private readonly RemainingHourRepository _remRepo;
        private readonly LeaveManagementDbContext _dbContext;
        public RemainingHourController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _remRepo = new RemainingHourRepository(_dbContext);
        }

        [Route("RemaingHour")]
        [HttpGet]
        public IActionResult RemainingHour([FromQuery]string staffId,string year)
        {
            var sickHour = _remRepo.ViewHour(staffId, year, "sick");
            var annualHour = _remRepo.ViewHour(staffId, year, "annual");
            var lwpHour = _remRepo.ViewHour(staffId, year, "lwp");
            RemainingHour hours = new RemainingHour
            {
                StaffId = staffId,
                Year = year,
                AnnualHours = annualHour,
                SickHours = sickHour,
                LWPHours = lwpHour,
            };
            
            return Content(JsonConvert.SerializeObject(hours), "application/json");
        }

        [Route("RemainingHour")]
        [HttpGet]
        public IActionResult ViewAllReporting()
        {
            var result = _remRepo.ViewAllRemainingHour();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        


    }
}
