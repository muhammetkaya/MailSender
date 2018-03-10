using MailSender.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Contracts.Types
{
    public class CredentialSettings
    {

        #region ..Properties..

        public string Username { get; set; }

        public string Password { get; set; }

        public eMailClientType ClientType { get; set; }
        
        #endregion

    }
}
