using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SocialApplication.Services
{
    public class EmailServices : IEmailSender
    {
        public void SendEmail(string email,string to)
        {
            MailMessage message = new MailMessage("kyawe225@gmail.com",to);
            message.Subject = "Verifing for account ";
            message.Body = email;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            NetworkCredential n = new NetworkCredential("c7ba6eac5efa98", "9c0b442769f7c2");
            smtp.Send(message);
        }

        public void SendEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}