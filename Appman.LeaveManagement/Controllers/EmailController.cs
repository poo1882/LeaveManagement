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
        public bool SendRequestMailToApprover(string approverEmail,int leaveId)
        {
            int i = 0;
            List<string> approveLinks = _leaveRepo.GenerateReferenceCode(leaveId);
            var sb = new StringBuilder();
            foreach (var item in approveLinks)
            {
                
                
                i++;
                if(i%2 == 1)
                {
                    sb = new StringBuilder();
                     sb.Append("<body style='margin: 0px;'>");
                    // Add your link
                    sb.AppendFormat("<div><a href='{0}'>Click here to approve</a></div>", item);
                    // Close the body element
                }
                else
                {
                    sb.AppendFormat("<div><a href='{0}'>Click here to reject</a></div>", item);
                    sb.Append("</body>");
                    // Get your e-mail contents using the ToString() method
                    var content = sb.ToString();
                    SendMail(approverEmail, content);
                }
            }
            return true;
        }
        

        public bool SendRequestMailToOwner(string email)
        {
            string body = "";
            if (SendMail(email, body) != "Mail has been successfully sent!")
                return false;
            return true;
        }
        

        private string SendMail(string receiverMail, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("Supornthip.s@appman.co.th");
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
    }
}
