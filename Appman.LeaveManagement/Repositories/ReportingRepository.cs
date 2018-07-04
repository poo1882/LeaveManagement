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
    }
}
