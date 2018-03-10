using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Contracts.Interfaces
{
    public interface IMailSender
    {
        void InitializeClient(CredentialSettings Credential);

        void SendMail(MailInfo MailInfo);
    }
}
