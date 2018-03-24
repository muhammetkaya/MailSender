using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.Logic.Clients
{
    public class SmptClient : Singleton<SmptClient>, IMailSender
    {

        #region ..Fields..

        SmtpClient client;
        MailSettings _settings;

        #endregion

        #region ..Constructor..

        public SmptClient()
        {
            this.client = new SmtpClient();
        }

        #endregion

        #region IMailSender Implementations

        public void InitializeClient(MailSettings settings)
        {
            _settings = settings;
            client.Credentials = new NetworkCredential(settings.Usermail, settings.Password, settings.Domain);

            client.Port = settings.Port;
            client.Host = settings.Url;
            client.EnableSsl = settings.EnableSsl;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void SendMail(MailInfo MailInfo)
        {
            try
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(_settings.Usermail),
                    Subject = MailInfo.Subject,
                    Body = MailInfo.Body,
                    IsBodyHtml = MailInfo.IsBodyHtml
                };

                if (MailInfo.IsReadReceiptRequest)
                {
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;
                    message.Headers.Add("Return-Receipt-To", _settings.Usermail);
                }

                //Warning: If you add mail address as string, delivery notification cant arrive to from mail. It types must MailAddress.
                if (MailInfo.ToRecipients != null)
                {
                    MailInfo.ToRecipients.ToList().ForEach(to =>
                    {
                        message.To.Add(new MailAddress(to));
                    });
                }

                if (MailInfo.CcRecipients != null)
                {
                    MailInfo.CcRecipients.ToList().ForEach(cc =>
                    {
                        message.CC.Add(new MailAddress(cc));
                    });
                }

                if (MailInfo.BccRecipients != null)
                {
                    MailInfo.BccRecipients.ToList().ForEach(Bcc =>
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
