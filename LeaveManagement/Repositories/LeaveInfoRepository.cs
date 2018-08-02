using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Repositories
{
    public class LeaveInfoRepository
    {
        private int totalHours = 0;
        LeaveManagementDbContext _dbContext;
        private readonly EmployeeRepository _empRepo;
        private readonly ReportingRepository _repRepo;
        private readonly MdRoleRepository _mdRoleRepo;
        public LeaveInfoRepository(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _empRepo = new EmployeeRepository(_dbContext);
            _repRepo = new ReportingRepository(_dbContext);
            _mdRoleRepo = new MdRoleRepository(_dbContext);
        }

        /// <summary>
        ///     View a leave's detail
        /// </summary>
        /// <param name="leaveId">An id of the form</param>
        /// <returns>
        ///     LeaveInfo - An information of the form
        ///     null - if no form found
        /// </returns>
        public LeaveInfo ViewLeaveInfo(int leaveId)
        {
            var emp = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
            if (emp == null)
                return null;
            return emp;
        }

        public bool CreateLeaveInfo(LeaveInfo info)
        {
            if (DateTime.UtcNow.Day == 31 && DateTime.UtcNow.Month == 12)
                return false;
            info.ApprovalStatus = "Pending";


            info.ApprovedBy = null;
            info.ApprovedTime = null;
            info.RequestedDateTime = DateTime.UtcNow;
            info.IsExisting = true;
            var thisYear = DateTime.UtcNow.Year;
            if (info.RequestedDateTime.Year != thisYear || info.StartDateTime.Year != thisYear || info.EndDateTime.Year != thisYear)
                return false;
            RemainingHourRepository remain = new RemainingHourRepository(_dbContext);
            int totalHours = 0;
            int totalHoursFirstDay = 0;
            int totalHoursLastDay = 0;
            totalHoursFirstDay = _dbContext.LeaveInfos.Where(x => x.StaffId == info.StaffId && x.StartDateTime == info.StartDateTime).Sum(x => x.HoursStartDate);
            totalHoursFirstDay = _dbContext.LeaveInfos.Where(x => x.StaffId == info.StaffId && x.EndDateTime == info.EndDateTime).Sum(x => x.HoursEndDate);
            if (info.EndDateTime.ToString() == info.StartDateTime.ToString())
            {
                if (totalHoursFirstDay + info.HoursStartDate <= 8)
                {
                    if (info.Type.ToLower()[0] == 'l' || remain.ViewHour(info.StaffId, info.StartDateTime.Year.ToString(), info.Type) >= info.HoursStartDate )
                    {
                        _dbContext.LeaveInfos.Add(info);
                        _dbContext.SaveChanges();
                        totalHours = info.HoursStartDate;
                        if (info.Type.ToLower()[0] == 'l')
                            remain.AddRemainHour(info.StaffId, info.Type, totalHours);
                        else
                            remain.DeductRemainHour(info.StaffId, info.Type, totalHours);
                        return true;
                    }
                    return false;
                }
                else return false;
            }
            else
            {
                if (totalHoursFirstDay+info.HoursStartDate <= 8 && totalHoursLastDay +info.HoursEndDate <=8)
                {
                    int totalDays = (info.EndDateTime - info.StartDateTime).Days;
                    totalHours = (totalDays - 1) * 8 + info.HoursStartDate + info.HoursEndDate;
                    if (info.Type.ToLower()[0] == 'l' ||  remain.ViewHour(info.StaffId, info.StartDateTime.Year.ToString(), info.Type) >= totalHours)
                    {
                        //UpdateRemainHour(info.StaffId, info.Type, totalHours);
                        _dbContext.LeaveInfos.Add(info);
                        _dbContext.SaveChanges();
                        if (info.Type.ToLower()[0] == 'l')
                            remain.AddRemainHour(info.StaffId, info.Type, totalHours);
                        else
                            remain.DeductRemainHour(info.StaffId, info.Type, totalHours);
                        return true;
                    }
                    return false;
                }
                else return false;
            }          

        }

        public bool SetDeleted(int leaveId, string commentByAdmin)
        {
            var leave = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
            if (leave == null)
                return false;
            leave.IsExisting = false;
            leave.CommentByAdmin = commentByAdmin;
            if (leave.ApprovalStatus.ToLower() != "rejected")
            {
                RemainingHourRepository remaining = new RemainingHourRepository(_dbContext);
                if (leave.StartDateTime.ToString() == leave.EndDateTime.ToString())
                    totalHours = leave.HoursStartDate;
                else
                {
                    int totalDays = (leave.EndDateTime - leave.StartDateTime).Days;
                    totalHours = (totalDays - 1) * 8 + leave.HoursStartDate + leave.HoursEndDate;
                }
                if (leave.Type.ToLower()[0] == 'l')
                    remaining.DeductRemainHour(leave.StaffId, leave.Type, totalHours);
                else
                    remaining.AddRemainHour(leave.StaffId, leave.Type, totalHours);
            }
            
            _dbContext.SaveChanges();
            return true;
        }

        public List<LeaveInfo> GetHistory()
        {
            var result = from employeelist in _dbContext.Employees
                         join leaveinfo in _dbContext.LeaveInfos on employeelist.StaffId equals leaveinfo.StaffId
                         where employeelist.IsActive == true
                         select leaveinfo;
            return result.OrderBy(x => x.StaffId).ToList();
        }


        /// <summary>
        ///     Get all created leave forms of an employee
        /// </summary>
        /// <param name="staffId">An employee id</param>
        /// <returns>
        ///     List<LeaveInfo> - List of leave forms in db
        ///     null - if no form created
        /// </returns>
        public List<LeaveInfo> GetHistory(string staffId)
        {
            var list = _dbContext.LeaveInfos;
            var result = list.Where(x => x.StaffId == staffId).OrderByDescending(x => x.LeaveId).Where(x => x.RequestedDateTime.Year == DateTime.Now.Year);
            return result.OrderBy(x => x.LeaveId).ToList();
        }


        /// <summary>
        ///     Get all remaining forms waiting for approve of an approver
        /// </summary>
        /// <param name="staffId">An approver's id</param>
        /// <returns>
        ///     List<LeaveInfo> - List of remaining leave forms
        ///     null - if no form remaining
        /// </returns>
        public List<LeaveInfo> GetRemaining(string staffId)
        {
            if (_empRepo.GetRole(staffId).ToLower() == "admin")
            {
                return _dbContext.LeaveInfos.Where(x => x.ApprovalStatus.ToLower() == "pending" && x.StaffId != staffId).OrderByDescending(y => y.LeaveId).ToList();
                //var result = 
                //foreach (var item in result)
                //{
                //    if (item.StaffId == staffId)
                //        result.Remove(item);
                //}
                //return result;
            }
            else
            {
                var result = from reportlist in _dbContext.Reportings
                             join leaveinfo in _dbContext.LeaveInfos on reportlist.StaffId equals leaveinfo.StaffId
                             where reportlist.Approver == staffId
                             select leaveinfo;
                return result.Where(x => x.ApprovalStatus.ToLower() == "pending").OrderByDescending(y => y.LeaveId).ToList();
            }
        }

        /// <summary>
        ///     Set status of form to "Approved"
        /// </summary>
        /// <param name="leaveId">id of an approved form</param>
        /// <param name="approverId">id of an approver</param>
        /// <returns>
        ///     true - if success
        ///     false - if form is already approved or rejected, or no form found
        /// </returns>
        public bool SetStatus(string status,int leaveId,string approverId)
        {
            var leave = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
            //var approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
            Reporting report = new Reporting { StaffId=leave.StaffId, Approver=approverId};
            var approverRole = _empRepo.GetRole(approverId);
            if (leave == null)
                return false;

            if (leave.ApprovalStatus.ToLower() != "pending")
                    return false;
            bool isInReport = _dbContext.Reportings.Any(x => x.Approver == approverId && x.StaffId == leave.StaffId);
            //bool isInReport = reportings.Contains(report);
            if (!isInReport && approverRole.ToLower() != "admin")
                    return false;
            
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedBy = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).FirstName;
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovedTime = DateTime.Now;
            //_dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId).ApprovalStatus = status;
            
            leave.ApprovedTime = DateTime.Now;
            leave.ApprovedBy = approverId;
            leave.ApprovalStatus = status.ToLower();
            if(status.ToLower() == "rejected")
            {
                var info = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
                RemainingHourRepository remaining = new RemainingHourRepository(_dbContext);
                if (info.StartDateTime.ToString() == info.EndDateTime.ToString())
                    totalHours = info.HoursStartDate;
                else
                {
                    int totalDays = (info.EndDateTime - info.StartDateTime).Days;
                    totalHours = (totalDays - 1) * 8 + info.HoursStartDate + info.HoursEndDate;
                }
                if (leave.Type.ToLower()[0] == 'l')
                    remaining.DeductRemainHour(leave.StaffId, leave.Type, totalHours);
                else
                    remaining.AddRemainHour(leave.StaffId, leave.Type,totalHours);
            }
            var approbationsLeft = _dbContext.Approbations;
            foreach (var item in approbationsLeft)
            {
                if (item.LeaveId == leaveId)
                    approbationsLeft.Remove(item);
            }
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Set status of form to "Rejected"
        /// </summary>
        /// <param name="leaveId">id of an rejected form</param>
        /// <param name="approverId">id of an approver</param>
        /// /// <returns>
        ///     true - if success
        ///     false - if form is already approved or rejected, or no form found
        /// </returns>
        //public bool Reject(string leaveId,string approverId)
        //{
        //    var leave = _dbContext.LeaveInfos.FirstOrDefault(x => x.LeaveId == leaveId);
        //    var approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
        //    Reporting report = new Reporting { StaffId = leave.StaffId, Approver = approverId };

        //    if (leave == null)
        //        return false;

        //    if (leave.ApprovalStatus != "Pending")
        //        return false;

        //    if (!_dbContext.Reportings.Contains(report) && approver.IsSuperHr == false)
        //        return false;

        //    leave.ApprovalStatus = "Rejected";
        //    leave.ApprovedBy = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId).FirstName;
        //    _dbContext.SaveChanges();
        //    return true;
        //}

        /// <summary>
        ///     Calculate the amount of pending leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of pending forms
        /// </returns>
        public int PendingAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus.ToLower() == "pending").Count();
        }

        /// <summary>
        ///     Calculate the amount of approved leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of approved forms
        /// </returns>
        public int ApprovedAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus.ToLower() == "approved").Count();
        }

        /// <summary>
        ///     Calculate the amount of rejected leaves of an employee
        /// </summary>
        /// <param name="staffId">An employee's id</param>
        /// <returns>
        ///     int - the number of rejected forms
        /// </returns>
        public int RejectedAmount(string staffId)
        {
            var leaves = _dbContext.LeaveInfos.Where(x => x.StaffId == staffId);
            return leaves.Where(x => x.ApprovalStatus.ToLower() == "rejected").Count();
        }

        /// <summary>
        ///     Calculste the amount of forms
        /// </summary>
        /// <returns>
        ///     int - the number of leaves form
        /// </returns>
        private int LeaveCount()
        {
            return _dbContext.LeaveInfos.Count();
        }

        public string GenerateLeaveId()
        {
            return "LE" + LeaveCount().ToString("D10");
        }

        //public string ExportLeaveAsHtml(LeaveInfo leave)
        //{
        //    var sb = new StringBuilder("<body style='margin: 0px;'>");
        //    sb.Append("แจ้งเตือนการสร้างใบลาใหม่")
        //    sb.AppendFormat("<b>มีการสร้างใบลาใหม่จากคุณ {0}</b>",_dbContext.Employees.FirstOrDefault(leave.Ap));
        //    sb.AppendFormat();
        //    sb.Append("</body>");
        //    return "";
        //}

        public List<string> GenerateReferenceCode(int leaveId)
        {
            List<Approbation> approbations = _dbContext.Approbations.Where(x => x.LeaveId == leaveId).ToList();
            List<string> result = new List<string>();
            string api = "https://appmanleavemanagement.azurewebsites.net/api/Leaves/ApproveViaEmail?";
            foreach (var item in approbations)
            {
                var sb = new StringBuilder(api);
                sb.AppendFormat("refNo="+item.ApprobationGuid.ToString());
                result.Add(sb.ToString());
            }
            //LeaveInfo leave = ViewLeaveInfo(leaveId);
            //List<string> result = new List<String>();
            //
            //List<Reporting> reportings = _repRepo.GetApprover(leave.StaffId);
            
            //foreach (var item in reportings)
            //{
                
            //    for (int j = 0; j < 2; j++)
            //    {
            //        var sb = new StringBuilder(api);
            //        sb.AppendFormat("refNo1=" + leave.LeaveGuid.ToString() + "&");
            //        sb.AppendFormat("refNo2=" + _empRepo.GetProfile(item.Approver).StaffGuId.ToString() + "&");

            //        if (j == 0)
            //            sb.Append("refNo3=Approved");
            //        else
            //            sb.Append("refNo3=Rejected");
            //        result.Add(sb.ToString());
            //    }
            //}
            return result;
        }

        public void AddApprobation (List<Approbation> approbations)
        {
            foreach (var item in approbations)
            {
                _dbContext.Approbations.Add(item);
                _dbContext.SaveChanges();
            }
            
        }

        public List<Approbation> CreateApprobationSet(LeaveInfo leave)
        {
            List<Approbation> result = new List<Approbation>();
            List<Reporting> reportings = new List<Reporting>();
            reportings = _repRepo.GetApprover(leave.StaffId);
            foreach (var item in reportings)
            {
                for(int i=0;i<2;i++)
                {
                    if (i == 0)
                        result.Add(new Approbation { ApprobationGuid = new Guid(), LeaveId = leave.LeaveId, ApproverId = item.Approver, Status = "Approved" });
                        
                    else
                        result.Add(new Approbation { ApprobationGuid = new Guid(), LeaveId = leave.LeaveId, ApproverId = item.Approver, Status = "Rejected" });
                }
                
            }
            return result;
        }

        public void ClearLeaveHistory()
        {
            var leaves = _dbContext.LeaveInfos;
            foreach (var item in leaves)
            {
                leaves.Remove(item);
            }
            _dbContext.SaveChanges();
        }

        public int GetTotalHours(int leaveId)
        {
            LeaveInfo info = ViewLeaveInfo(leaveId);
            if (info.StartDateTime.ToString() == info.EndDateTime.ToString())
                totalHours = info.HoursStartDate;
            else
            {
                int totalDays = (info.EndDateTime - info.StartDateTime).Days;
                totalHours = (totalDays - 1) * 8 + info.HoursStartDate + info.HoursEndDate;
            }
            return totalHours;
        }
    }
}
