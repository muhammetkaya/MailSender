using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Clients.Managers
{
    public class ExchangeClient : Singleton<ExchangeClient>, IMailSender
    {
        public void InitializeClient(CredentialSettings Credential)
        {

        }

        public void SendMail(MailInfo MailInfo)
        {

        }
    }
}
