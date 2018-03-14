using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Contracts.Types
{
    public class MailInfo
    {

        #region ..Constructor..

        public MailInfo()
        {

        }

        #endregion

        #region ..Properties..
        public string From { get; set; }

        public string[] ToReceipts { get; set; }

        public string[] CcReceipts { get; set; }

        public string[] BccReceipts { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsDeliveryReceiptRequest { get; set; }

        public bool IsReadReceiptRequest { get; set; }

        public bool IsBodyHtml { get; set; }

        #endregion

    }
}
