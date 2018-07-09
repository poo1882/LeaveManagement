using Appman.LeaveManagement.DatabaseContext;
using Appman.LeaveManagement.DatabaseContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class EmailController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly LeaveInfoRepository _leaveRepo;

        public EmailController (LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _leaveRepo = new LeaveInfoRepository(_dbContext);

        }
        public bool SendRequestMailToApprover(List<Approbation> approbations, LeaveInfo leaveInfo)
        {
            int i = 0;
            string api = "https://appmanleavemanagement.azurewebsites.net/api/Leaves/ApproveViaEmail?";
            var sb = new StringBuilder();
            string value = String.Format("LEA{0:D5}", leaveInfo.LeaveId);
            var name = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstName;
            var lastName = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastName;
            foreach (var item in approbations)
            {
                i++;
                if (i % 2 == 1)
                {
                    StringBuilder link = new StringBuilder();
                    link.Append(api);
                    link.AppendFormat("refNo=" + item.ApprobationGuid.ToString());
                    sb = new StringBuilder();
                    sb.Append("<body style='margin: 0px;'> ");
                    sb.AppendFormat("<div>เลขที่ใบลา : {0}<br><br>Staff Id : {1}<br><br> ชื่อ : {2} {3} <br><br> วันที่ลา : {4}<br><br>", value, leaveInfo.StaffId, name, lastName, leaveInfo.StartDateTime);
                    if (leaveInfo.StartDateTime != leaveInfo.EndDateTime)
                    {
                        sb.AppendFormat("ถึงวันที่ : {0}<br><br>", leaveInfo.EndDateTime);
                    }
                    sb.AppendFormat("รูปแบบการลา : {0}<br><br> Comment : {1} <br><br></div>", leaveInfo.Type, leaveInfo.Comment);

                    sb.AppendFormat("<div><a href='{0}'>Click here to approve</a></div>", link);
                }
                else
                {
                    StringBuilder link = new StringBuilder();
                    link.Append(api);
                    link.AppendFormat("refNo=" + item.ApprobationGuid.ToString());
                    sb.AppendFormat("<div><a href='{0}'>Click here to reject</a></div>", link);
                    sb.Append("</body>");
                    var content = sb.ToString();
                    SendMail(_dbContext.Employees.FirstOrDefault(x => x.StaffId == item.ApproverId).Email, content);
                }
            }
            return true;
        }


        public bool SendRequestMailToOwner(string email, LeaveInfo leaveInfo)
        {
            var sb = new StringBuilder();
            var name = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstName;
            var lastName = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastName;
            string value = String.Format("LEA{0:D5}", leaveInfo.LeaveId);
            sb.AppendFormat("<div>เลขที่ใบลา : {0}<br><br>Staff Id : {1}<br><br> ชื่อ : {2} {3} <br><br> วันที่ลา : {4}<br><br>", value, leaveInfo.StaffId, name, lastName, leaveInfo.StartDateTime);
            if (leaveInfo.StartDateTime != leaveInfo.EndDateTime)
            {
                sb.AppendFormat("ถึงวันที่ : {0}<br><br>", leaveInfo.EndDateTime);
            }
            sb.AppendFormat("รูปแบบการลา : {0}<br><br> Comment : {1} <br><br></div>", leaveInfo.Type, leaveInfo.Comment);
            var body = sb.ToString();
            if (SendMail(email, body) != "Mail has been successfully sent!")
                return false;
            return true;
        }


        private string SendMail(string receiverMail, string body)
        {
            MailMessage msg = new MailMessage
            {
                From = new MailAddress("Supornthip.s@appman.co.th")
            };
            msg.To.Add(receiverMail);
            msg.Subject = "Hello world! " + DateTime.Now.ToString();

            msg.IsBodyHtml = true;
            msg.Body = body;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("Supornthip.s@appman.co.th", "appMan2004zZ");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return "Mail has been successfully sent!";
            }
            catch (Exception ex)
            {
                return "Fail Has error" + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }
        }

        public void SendResultToOwner(string staffId,int leaveId,string approverId,string status)
        {
            Employee approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
            string staffEmail = _dbContext.Employees.FirstOrDefault(x => x.StaffId == staffId).Email;
            string body = "Your leaving form #" + String.Format("LEA{0:D5} ", leaveId) + "has been already " + status.ToLower() + " by " + approver.FirstName + " " + approver.LastName+".";
            SendMail(staffEmail, body);
        }
    }
}
