using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.Clients.Managers
{
    public class SmptClient : Singleton<SmptClient>, IMailSender
    {

        #region ..Fields..

        SmtpClient client;

        #endregion

        #region ..Constructor..

        public SmptClient()
        {
            this.client = new SmtpClient();
        }

        #endregion

        #region IMailSender Implementations

        public void InitializeClient(MailSettings Credential)
        {
            client.Credentials = new NetworkCredential(Credential.Usermail, Credential.Password, Credential.Domain);

            client.Port = Credential.Port;
            client.Host = Credential.Url;
            client.EnableSsl = Credential.EnableSsl;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void SendMail(MailInfo MailInfo)
        {
            try
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(MailInfo.From),
                    Subject = MailInfo.Subject,
                    Body = MailInfo.Body,
                    IsBodyHtml = MailInfo.IsBodyHtml,
                    DeliveryNotificationOptions = MailInfo.IsDeliveryReceiptRequest ? DeliveryNotificationOptions.OnSuccess |
                                                                                      DeliveryNotificationOptions.OnFailure |
                                                                                      DeliveryNotificationOptions.Delay : DeliveryNotificationOptions.OnFailure
                };

                if (MailInfo.IsReadReceiptRequest)
                    message.Headers.Add("You Can add to Header", "value");// for read

                //Adding Mail Addresses

                //Warning: If you add mail address as string, delivery notification cant arrive to from mail. It types must MailAddress.
                if (MailInfo.ToReceipts != null)
                {
                    MailInfo.ToReceipts.ToList().ForEach(to =>
                    {
                        message.To.Add(new MailAddress(to));
                    });
                }

                if (MailInfo.CcReceipts != null)
                {
                    MailInfo.CcReceipts.ToList().ForEach(cc =>
                    {
                        message.CC.Add(new MailAddress(cc));
                    });
                }

                if (MailInfo.BccReceipts != null)
                {
                    MailInfo.BccReceipts.ToList().ForEach(Bcc =>
                    {
                        message.Bcc.Add(new MailAddress(Bcc));
                    });
                }

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
