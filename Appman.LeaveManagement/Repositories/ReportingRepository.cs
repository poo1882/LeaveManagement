using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class ReportingRepository
    {
        LeaveManagementDbContext _dbContext;
        public ReportingRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Add approver of an employee
        /// </summary>
        /// <param name="reporting">An instance of Reporting containing the employee's id and approver's id</param>
        /// <returns>
        ///     true - if success
        ///     false - if already exist
        /// </returns>
        public bool AddApprover(Reporting reporting)
        {
            if (_dbContext.Reportings.Contains(reporting))
                return false;
            _dbContext.Reportings.Add(reporting);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Remove approver of an employee
        /// </summary>
        /// <param name="reporting">An instance of reporting that wants to delete</param>
        /// <returns>
        ///     true - if success
        ///     false - if no reporting match
        /// </returns>
        public bool RemoveApprover(Reporting reporting)
        {
            if (!_dbContext.Reportings.Contains(reporting))
                return false;
            _dbContext.Reportings.Remove(reporting);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Reporting> GetApprover(string staffId)
        {
            return _dbContext.Reportings.Where(x => x.StaffId == staffId).ToList();
        }

        public List<Reporting> ViewAllReporting()
        {
            return _dbContext.Reportings.ToList();
        }

        public bool InitializeReportings(string password)
        {
            if (password != "init")
                return false;
            AddApprover(new Reporting
            {
                StaffId = "00007",
                Approver = "00006"
            });
            AddApprover(new Reporting
            {
                StaffId = "00007",
                Approver = "00005"
            });
            AddApprover(new Reporting
            {
                StaffId = "00008",
                Approver = "00007"
            });
            AddApprover(new Reporting
            {
                StaffId = "00008",
                Approver = "00004"
            });
            AddApprover(new Reporting
            {
                StaffId = "00001",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00003",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00004",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00005",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00006",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00007",
                Approver = "00002"
            });
            AddApprover(new Reporting
            {
                StaffId = "00002",
                Approver = "00006"
            });
            AddApprover(new Reporting
            {
                StaffId = "00005",
                Approver = "00004"
            });
            AddApprover(new Reporting
            {
                StaffId = "00003",
                Approver = "00004"
            });
            AddApprover(new Reporting
            {
                StaffId = "00009",
                Approver = "00005"
            });
            AddApprover(new Reporting
            {
                StaffId = "00009",
                Approver = "00008"
            });
            AddApprover(new Reporting
            {
                StaffId = "00001",
                Approver = "00003"
            });
            AddApprover(new Reporting
            {
                StaffId = "00002",
                Approver = "00009"
            });
            AddApprover(new Reporting
            {
                StaffId = "00004",
                Approver = "00001"
            });
            AddApprover(new Reporting
            {
                StaffId = "00006",
                Approver = "00007"
            });
            return true;
        }

        public void ClearReportings()
        {
            var reportings = _dbContext.Reportings;
            foreach (var item in reportings)
            {
                reportings.Remove(item);
            }
            _dbContext.SaveChanges();
        }


    }
}
