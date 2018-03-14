using MailSender.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Contracts.Types
{
    public class MailSettings
    {

        #region ..Properties..

        public string Usermail { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string Url { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        #endregion

    }
}
