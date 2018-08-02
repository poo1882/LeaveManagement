using LeaveManagement.DatabaseContext;
using LeaveManagement.DatabaseContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LeaveManagement.Repositories
{
    public class EmailController : Controller
    {
        private readonly LeaveManagementDbContext _dbContext;
        private readonly LeaveInfoRepository _leaveRepo;
        private readonly ReportingRepository _repRepo;
        private readonly EmployeeRepository _empRepo;

        public EmailController(LeaveManagementDbContext dbContext)
        {
            _dbContext = dbContext;
            _leaveRepo = new LeaveInfoRepository(_dbContext);
            _repRepo = new ReportingRepository(_dbContext);
            _empRepo = new EmployeeRepository(_dbContext);

        }
        public bool SendRequestMailToApprover(List<Approbation> approbations, LeaveInfo leaveInfo)
        {
            int i = 0;
            string api = "https://appmanleavemanagement20180718055046.azurewebsites.net/api/Leaves/ApproveViaEmail?";
            var sb = new StringBuilder();
            string url = "https://scontent.fbkk1-5.fna.fbcdn.net/v/t1.0-9/11070664_902255393167706_2830773750406557759_n.png?_nc_fx=fbkk1-3&_nc_cat=0&oh=7721fcafb6bbd5d172efbbfd88c9544f&oe=5BE284C3";
            string style = "width: 100px; height: 100px;";
            string value = String.Format("LEA{0:D5}", leaveInfo.LeaveId);
            string subject = "Leaving application form #" + String.Format("LEA{0:D5} ", leaveInfo.LeaveId) + " is waiting for you to approve.";
            var name = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstNameTH;
            var lastName = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastNameTH;
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
                    sb.AppendFormat("<div>Leaving application form No. : {0}<br><br>Staff Id : {1}<br><br> Name : {2} {3} <br><br> Start Date : {4}<br><br> Hours Start Date: {5}<br><br>", value, leaveInfo.StaffId, name, lastName, leaveInfo.StartDateTime, leaveInfo.HoursStartDate);
                    if (leaveInfo.StartDateTime != leaveInfo.EndDateTime)
                    {
                        sb.AppendFormat("End Date : {0}<br><br> Hours End Date : {1}<br><br>", leaveInfo.EndDateTime, leaveInfo.HoursEndDate);
                    }
                    sb.AppendFormat("Type : {0}<br><br>", leaveInfo.Type);
                    if (leaveInfo.Comment.Length > 0)
                    {
                        sb.AppendFormat("Comment : {0} <br><br>", leaveInfo.Comment);
                    }
                    if (leaveInfo.AttachedFile1.Length > 0)
                    {
                        sb.AppendFormat("See the attached file in website <br><br></div>");
                    }
                    sb.AppendFormat("<div><a href='{0}'>Click here to approve</a></div>", link);
                }
                else
                {
                    StringBuilder link = new StringBuilder();
                    link.Append(api);
                    link.AppendFormat("refNo=" + item.ApprobationGuid.ToString());
                    sb.AppendFormat("<div><a href='{0}'>Click here to reject</a></div>", link);
                    sb.AppendFormat("<img src='{0}' style='{1}'>", url, style);
                    sb.AppendFormat("<div>AppMan Co.,Ltd<br>52 / 25 Pan road,<br>Silom Bangrak<br>Bangkok<br>10500<br>Tax invoice: 0105554076903<br>Tel: (+66)2 635 2874<br>www.appman.co.th</div>");
                    sb.Append("</body>");
                    var content = sb.ToString();
                    SendMail(_dbContext.Employees.FirstOrDefault(x => x.StaffId == item.ApproverId).Email, content, subject);
                }
            }
            return true;
        }


        public bool SendRequestMailToOwner(string email, LeaveInfo leaveInfo)
        {
            var sb = new StringBuilder();
            var name = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstNameTH;
            var lastName = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastNameTH;
            string url = "https://scontent.fbkk1-5.fna.fbcdn.net/v/t1.0-9/11070664_902255393167706_2830773750406557759_n.png?_nc_fx=fbkk1-3&_nc_cat=0&oh=7721fcafb6bbd5d172efbbfd88c9544f&oe=5BE284C3";
            string style = "width: 100px; height: 100px;";
            string value = String.Format("LEA{0:D5}", leaveInfo.LeaveId);
            string subject = "Your Leaving application form #" + String.Format("LEA{0:D5} ", leaveInfo.LeaveId) + " has been created ";
            sb.AppendFormat("<div>Leaving application form No. : {0}<br><br>Staff Id : {1}<br><br> Name : {2} {3} <br><br> Start Date : {4}<br><br> Hours Start Date : {5}<br><br>", value, leaveInfo.StaffId, name, lastName, leaveInfo.StartDateTime, leaveInfo.HoursStartDate);
            if (leaveInfo.StartDateTime != leaveInfo.EndDateTime)
            {
                sb.AppendFormat("End Date : {0}<br><br> Hours End Date : {1}<br><br>", leaveInfo.EndDateTime, leaveInfo.HoursEndDate); 
            }
            sb.AppendFormat("Type : {0}<br><br>", leaveInfo.Type);
            if (leaveInfo.Comment.Length > 0)
            {
                sb.AppendFormat("Comment : {0} <br><br>", leaveInfo.Comment);
            }
            if (leaveInfo.AttachedFile1.Length > 0 )
            {
                sb.AppendFormat("See the attached file in website <br><br></div>");
            }
            sb.AppendFormat("<img src='{0}' style='{1}'>", url, style);
            sb.AppendFormat("<div>AppMan Co.,Ltd<br>52 / 25 Pan road,<br>Silom Bangrak<br>Bangkok<br>10500<br>Tax invoice: 0105554076903<br>Tel: (+66)2 635 2874<br>www.appman.co.th</div>");
            var body = sb.ToString();
            if (SendMail(email, body, subject) != "Mail has been successfully sent!")
                return false;
            return true;
        }


        private string SendMail(string receiverMail, string body, string subject)
        {
            MailMessage msg = new MailMessage
            {
                From = new MailAddress("supornthip.s@appman.co.th")
            };
            msg.To.Add(receiverMail);
            msg.Subject = subject + DateTime.Now.ToString();

            msg.IsBodyHtml = true;
            msg.Body = body;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("supornthip.s@appman.co.th", "appMan2004zZ");
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

        public void SendResultToOwner(string approverId, string status, LeaveInfo leaveInfo)
        {
            Employee approver = _dbContext.Employees.FirstOrDefault(x => x.StaffId == approverId);
            var approvers = _repRepo.GetApprover(leaveInfo.StaffId);
            string staffEmail = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).Email;
            string firstname = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstNameEN;
            string lastname = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastNameEN;
            string subject = "Leaving application form #" + String.Format("LEA{0:D5} ", leaveInfo.LeaveId) + " has already been " + status.ToLower() + " ";
            string body = String.Format("LEA{0:D5} ", leaveInfo.LeaveId) + " sent leave application form by" + firstname + " " + lastname
               + "has been already " + status.ToLower() + " by "
               + approver.FirstNameTH + " " + approver.LastNameTH + ".";
            var sb = new StringBuilder();
            var name = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).FirstNameEN;
            var lastName = _dbContext.Employees.FirstOrDefault(x => x.StaffId == leaveInfo.StaffId).LastNameEN;
            string url = "https://scontent.fbkk1-5.fna.fbcdn.net/v/t1.0-9/11070664_902255393167706_2830773750406557759_n.png?_nc_fx=fbkk1-3&_nc_cat=0&oh=7721fcafb6bbd5d172efbbfd88c9544f&oe=5BE284C3";
            string style = "width: 100px; height: 100px;";
            string value = String.Format("LEA{0:D5}", leaveInfo.LeaveId);
            sb.AppendFormat("<div>Leaving application form No. : {0}<br><br>Staff Id : {1}<br><br> Name : {2} {3} <br><br> Start Date : {4}<br><br> Hours Start Date : {5}<br><br>", value, leaveInfo.StaffId, name, lastName, leaveInfo.StartDateTime, leaveInfo.HoursStartDate);
            if (leaveInfo.StartDateTime != leaveInfo.EndDateTime)
            {
                sb.AppendFormat("End Date : {0}<br><br> Hours End Date : {1}<br><br>", leaveInfo.EndDateTime, leaveInfo.HoursEndDate);
            }
            sb.AppendFormat("Type : {0}<br><br>", leaveInfo.Type);
            if (leaveInfo.Comment.Length > 0)
            {
                sb.AppendFormat("Comment : {0} <br><br>", leaveInfo.Comment);
            }
            if (leaveInfo.AttachedFile1.Length > 0)
            {
                sb.AppendFormat("See the attached file in website <br><br></div>");
            }
            sb.AppendFormat("<img src='{0}' style='{1}'>", url, style);
            sb.AppendFormat("<div>AppMan Co.,Ltd<br>52 / 25 Pan road,<br>Silom Bangrak<br>Bangkok<br>10500<br>Tax invoice: 0105554076903<br>Tel: (+66)2 635 2874<br>www.appman.co.th</div>");
            var body2 = sb.ToString();
            var body3 = body + body2;
            foreach (var item in approvers)
            {
                SendMail(_empRepo.GetEmail(item.Approver), body3, subject);
            }
            SendMail(staffEmail, body3, subject);


        }
    }
}
