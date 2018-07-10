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

        public FileController(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _empRepo = new EmployeeRepository(_dbContext);
        }

        [Route("File")]
        [HttpGet]
        public FileResult DownloadReport()
        {
            var lstData = _empRepo.GetEmployees();
            var sb = new StringBuilder();
            foreach (var data in lstData)
            {
                sb.AppendLine(data.StaffId + "," + data.FirstName + ","
                    +data.LastName+", "+data.Email+", "+data.Position);
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "export.csv");
        }
    }
}
