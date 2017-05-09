using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DixonsCarphone.WorkforceManagement.Business.Helpers
{
    public class Utils
    {
        public static bool SendEmail(string to, string body, string subject)
        {
            var fromAddress = new MailAddress("dxscpw@gmail.com", "Carphone Intranet");
            var toAddress = new MailAddress(to);
            const string fromPassword = "Carph0ne";
  
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return true;
        }
    }
}
