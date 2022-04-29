using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApplication.Services
{
    internal interface IEmailSender
    {
        void SendEmail(string email);
    }
}
