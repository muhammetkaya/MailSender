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

        public string[] ToReceipts { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }

        public string MyProperty { get; set; }

        public bool IsDeliveryReceiptRequest { get; set; }

        public bool IsReadReceiptRequest { get; set; }

        public bool IsContentHtml { get; set; }

        #endregion

    }
}
