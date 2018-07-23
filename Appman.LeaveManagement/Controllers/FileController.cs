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
        private readonly ReportingRepository _repRepo;

        public FileController(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _empRepo = new EmployeeRepository(_dbContext);
            _remRepo = new RemainingHourRepository(_dbContext);
            _repRepo = new ReportingRepository(_dbContext);
        }

        [Route("Statistic")]
        [HttpGet]
        public FileResult Statistics(string year)
        {
            var listData = _empRepo.GetEmployees().OrderBy(x=>x.StaffId);
            var sb = new StringBuilder();
            sb.AppendLine("StaffID," + "FirstName," + "LastName," + "Annual," + "USED,"+"LEFT,"+"Sick,"+"USED,"+"LEFT," + "LWP,"+"USED,"+"LEFT");
            foreach (var data in listData)
            {
                RemainingHour remainingHour = _dbContext.RemainingHours.FirstOrDefault(x => x.StaffId == data.StaffId && x.Year == year);

                sb.AppendLine(data.StaffId + ","
                    + data.FirstNameTH + ","
                    + data.LastNameTH + ","
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

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "stats.csv");
        }

        [Route("PendingLeaves")]
        [HttpGet]
        public FileResult PendingLeaves()
        {
            var listData = _leaveRepo.GetHistory().Where(x => x.ApprovalStatus.ToLower() == "pending").OrderBy(x => x.LeaveId);
            var sb = new StringBuilder();
            sb.AppendLine("LeaveID," + "StaffID," + "Approver1," + "Approver2,"+"RequestedDate");
            int approverAmount = 2;
            foreach (var data in listData)
            {
                List<Reporting> reportings = _repRepo.GetApprover(data.StaffId);

                sb.Append(data.LeaveId + "," + data.StaffId + ",");

                for (int i = 0; i < approverAmount; i++)
                {
                    if(reportings[i] != null)
                    {
                        string approverName = _empRepo.GetName(reportings[i].Approver);
                        sb.Append(approverName);
                        if (i != approverAmount - 1)
                            sb.Append(",");
                    }
                    
                }
                sb.Append(","+data.RequestedDateTime);
                sb.AppendLine();
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "pendingLeaves.csv");
        }
    }
}
