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
    public class RemainingHourController
    {
        private readonly RemainingHourRepository _remRepo;
        private readonly LeaveManagementDbContext _dbContext;
        public RemainingHourController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _remRepo = new RemainingHourRepository(_dbContext);
        }

        public IActionResult ViewRemainingHour(Guid id,string year)
        {
            var sickHours = _remRepo.ViewSickHour(id, year);
            var annualHours = _remRepo.ViewAnnualHour(id, year);
            var lwpHours = _remRepo.ViewLWPHour(id, year);
            return Ok(id);
        }
    }
}
