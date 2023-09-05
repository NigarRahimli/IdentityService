using System;

namespace IdentityService.Core.Utilities.MailHelper
{
    public interface IMailSender
    {
        bool SendMail(string mailAddress, string message, bool bodyHtml);
    }
}

