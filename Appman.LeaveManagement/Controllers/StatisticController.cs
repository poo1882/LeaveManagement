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


        /// <summary>
        ///     List all employees plus the statistic of leaves form (Pending, Approved, Rejected)
        /// </summary>
        /// <returns>
        ///     Ok(List<Statistic>) - A list of everyone who is active
        /// </returns>
        [Route("GetStatistics")]
        [HttpGet]
        public IActionResult GetStatistics()
        {
            List<Statistic> result = new List<Statistic>();
            foreach (var item in _dbContext.Employees)
            {
                if(item.IsActive == true)
                {
                    Statistic stat = new Statistic(item.StaffId, _empRepo, _leaveRepo);
                    result.Add(stat);
                }
            }
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }


        /// <summary>
        ///     Show basic info. of an employee plus the history of leaveing
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     OneStatistic - An instance of name, position, with a leaveing history
        /// </returns>
        [Route("GetLeaveStatistic")]
        [HttpGet]
        public IActionResult GetLeaveStatistic([FromQuery]string staffId)
        {
            OneStatistic result = new OneStatistic(staffId,_empRepo,_remRepo);
            result.Leaves = _leaveRepo.GetHistory(staffId);
            return Content(JsonConvert.SerializeObject(result), "application/json");
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
