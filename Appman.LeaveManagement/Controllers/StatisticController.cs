using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Models;
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
    public class StatisticController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly EmployeeRepository _empRepo;
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly RemainingHourRepository _remRepo;

        public StatisticController(LeaveManagementDbContext leaveManagementDbContext)
        {
            _dbContext = leaveManagementDbContext;
            _empRepo = new EmployeeRepository(_dbContext);
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _remRepo = new RemainingHourRepository(_dbContext);
        }
        [Route("GetStatistics")]
        [HttpGet]
        public IActionResult GetStatistics()
        {
            List<Statistic> result = new List<Statistic>();
            foreach (var item in _dbContext.Employees)
            {
                Statistic stat = new Statistic(item.StaffId,_empRepo,_leaveRepo);
                result.Add(stat);
            }
            return Ok(JsonConvert.SerializeObject(result));
        }

        [Route("GetLeaveStatistic")]
        [HttpGet]
        public IActionResult GetLeaveStatistic(string staffId)
        {
            OneStatistic result = new OneStatistic(staffId,_empRepo,_leaveRepo,_remRepo);
            return Ok(JsonConvert.SerializeObject(result));
        }

        //[Route("GetLeaveInfo")]
        //[HttpGet]
        //public IActionResult GetLeaveInfo(int LeaveId)
        //{
        //    LeaveInfo result = _leaveRepo.ViewLeaveInfo(LeaveId);
        //    return Ok(JsonConvert.SerializeObject(result));
        //}
    }
}
