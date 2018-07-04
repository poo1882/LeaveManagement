using Appman.LeaveManagement.DatabaseContext.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Appman.LeaveManagement.Repositories
{
    public class EmailController : Controller
    {
        
        public bool SendRequestMailToApprover(string approverEmail)
        {
            string body = "";
            if (SendMail(approverEmail, body) != "Mail has been successfully sent!")
                return false;
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
