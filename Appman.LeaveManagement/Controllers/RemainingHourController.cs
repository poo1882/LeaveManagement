using Appman.LeaveManagement.DatabaseContext;
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
<<<<<<< HEAD
        [Route("RemainingHour")]
        [HttpGet]
        public IActionResult ViewRemainingHour(Guid id,string year)
        {
            var sickHours = _remRepo.ViewSickHour(id, year);
            var annualHours = _remRepo.ViewAnnualHour(id, year);
            var lwpHours = _remRepo.ViewLWPHour(id, year);
            return Ok();
=======

        [Route("RemaingHours")]
        [HttpGet]
        public IActionResult ViewRemainingHour(Guid id,string year)
        {
            var sickHours = _remRepo.ViewHour(id, year,"Sick");
            var annualHours = _remRepo.ViewHour(id, year,"Annual");
            var lwpHours = _remRepo.ViewHour(id, year,"LWP");
            return Ok(id);
>>>>>>> 182b1bf49a6fa533ab2f1d3b38104b7f77310a6a
        }
    }
}
