using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class ExportStats
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalAnnualHours { get; set; }
        public int TotalSickHours { get; set; }
        public int TotalLeaveWithoutPay { get; set; }
        public int AnnualLeft { get; set; }
        public int SickLeft { get; set; }
        public int LeaveWithoutPayLeft { get; set; }
        public int AnnualUsed { get; set; }
        public int SickUsed { get; set; }
        public int LeaveWithoutPayUsed { get; set; }


        public ExportStats(string StaffId,string FirstName,string LastName,int TotalAnnualHours,int TotalSickHours, int TotalLeaveWithoutPay,int AnnualLeft,int SickLeft,int LeaveWithoutPayLeft)
        {
            this.StaffId = StaffId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.TotalAnnualHours = TotalAnnualHours;
            this.TotalSickHours = TotalSickHours;
            this.TotalLeaveWithoutPay = TotalLeaveWithoutPay;
            this.AnnualLeft = AnnualLeft;
            this.SickLeft = SickLeft;
            this.LeaveWithoutPayLeft = LeaveWithoutPayLeft;
            AnnualUsed = TotalAnnualHours - AnnualLeft;
            SickUsed = TotalSickHours - SickLeft;
            LeaveWithoutPayUsed = TotalLeaveWithoutPay;
        }
    }
}
