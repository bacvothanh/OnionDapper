using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using Dapper.Infrastructure;

namespace Dapper.Web.Helpers
{
    public class Mailer : MailerBase
    {
        public MvcMailMessage ForgetPassword(string mail, string url)
        {
            var mailMessage = new MvcMailMessage { Subject ="Quên mật khẩu" };
#if DEBUG
            mailMessage.To.Add(ApplicationConstant.DebugMail);
#else
             mailMessage.To.Add(mail);
#endif
            ViewBag.Url = url;
            PopulateBody(mailMessage, viewName: "ForgetPassword");
            return mailMessage;
        }

        public MvcMailMessage ActiveAccount(string mail, string url)
        {
            var mailMessage = new MvcMailMessage { Subject = "Xác nhận tài khoản" };
#if DEBUG
            mailMessage.To.Add(ApplicationConstant.DebugMail);
#else
             mailMessage.To.Add(mail);
#endif
            ViewBag.Url = url;
            PopulateBody(mailMessage, viewName: "ActiveAccount");
            return mailMessage;
        }
    }
}