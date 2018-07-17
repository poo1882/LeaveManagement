using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Appman.LeaveManagement.Repositories;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Appman.LeaveManagement.Controllers
{
    public class FileController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly EmployeeRepository _empRepo;
        private readonly RemainingHourRepository _remRepo;

        public FileController(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _empRepo = new EmployeeRepository(_dbContext);
            _remRepo = new RemainingHourRepository(_dbContext);
        }

        [Route("File")]
        [HttpGet]
        public FileResult DownloadReport(string year)
        {
            var listData = _empRepo.GetEmployees().OrderBy(x=>x.StaffId);
            var sb = new StringBuilder();
            sb.AppendLine("StaffID," + "FirstName," + "LastName," + "Annual," + "USED,"+"LEFT,"+"Sick,"+"USED,"+"LEFT," + "LWP,"+"USED,"+"LEFT");
            foreach (var data in listData)
            {
                RemainingHour remainingHour = _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == data.StaffId && x.Year == year);

                sb.AppendLine(data.StaffId + ","
                    + data.FirstName + ","
                    + data.LastName + ","
                    + remainingHour.TotalAnnualHours.ToString() + ","
                    + (remainingHour.TotalAnnualHours - remainingHour.AnnualHours).ToString() + ","
                    +remainingHour.AnnualHours.ToString() + ","
                    + remainingHour.TotalSickHours.ToString() + ","
                    + (remainingHour.TotalSickHours - remainingHour.SickHours).ToString() + ","
                    + remainingHour.SickHours.ToString() + ","
                    + remainingHour.TotalLWPHours.ToString() + ","
                    + (remainingHour.TotalLWPHours - remainingHour.LWPHours).ToString() + ","
                    + remainingHour.LWPHours.ToString());
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "export.csv");
        }
    }
}
