using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        [Route("RemaingHours")]
        [HttpGet]
        public IActionResult RemainingHour(Guid id,string year)
        {
            var sickHour = _remRepo.ViewHour(id, year,"Sick");
            var annualHour = _remRepo.ViewHour(id, year,"Annual");
            var lwpHour = _remRepo.ViewHour(id, year,"LWP");
            RemainingHour hours = new RemainingHour();
            hours.EmployeeId = id;
            hours.Year = year;
            hours.AnnualHours = annualHour;
            hours.SickHours = sickHour;
            hours.LWPHours = lwpHour;
            return Ok(hours);
        }


    }
}
