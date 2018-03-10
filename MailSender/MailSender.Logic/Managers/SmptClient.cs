using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Clients.Managers
{
    public class SmptClient : Singleton<SmptClient>, IMailSender 
    {

        #region ..IMailSender Implements..

        public void InitializeClient(CredentialSettings Credential)
        {
            throw new NotImplementedException();
        }

        public void SendMail(MailInfo MailInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
