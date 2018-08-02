using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
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

        [Route("RemainingHour")]
        [HttpGet]
        public IActionResult RemainingHour([FromQuery]string staffId)
        {
            string year = DateTime.UtcNow.Year.ToString();
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

        [Route("RemainingHours")]
        [HttpPut]
        public IActionResult InitRemainingHours(string password)
        {
            if (!_remRepo.InitRemainingHours(password.ToLower()))
                return NotFound();
            return Ok();
        }

        [Route("RemainingHours")]
        [HttpGet]
        public IActionResult GetRemainingHours()
        {
            var result = _remRepo.GetRemainingHours();
            if (result == null)
                return NotFound();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Route("RemainingHours")]
        [HttpDelete]
        public IActionResult ClearRemainingHours()
        {
            _remRepo.ClearRemainingHours();
            return Ok();
        }


    }
}
