using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Linq;

namespace MailSender.Logic.Clients
{
    public class ExchangeClient : Singleton<ExchangeClient>, IMailSender
    {

        #region ..Fields..

        ExchangeService client;
        MailSettings _settings;

        #endregion

        #region ..Constructor..

        public ExchangeClient()
        {
            this.client = new ExchangeService();
        }

        #endregion

        #region IMailSender Implementations

        public void InitializeClient(MailSettings settings)
        {
            _settings = settings;
            client.Url = new Uri(settings.Url);
            client.UseDefaultCredentials = false;
            client.Credentials =new WebCredentials(settings.Usermail, settings.Password, settings.Domain);

            //client.AutodiscoverUrl(Credential.Usermail);
        }

        public void SendMail(MailInfo MailInfo)
        {
            try
            {
                EmailMessage message = new EmailMessage(client)
                {
                    Subject = MailInfo.Subject,
                    Body = new MessageBody(MailInfo.IsBodyHtml ? BodyType.HTML : BodyType.Text, MailInfo.Body),
                };

                message.From = new EmailAddress(_settings.Usermail);
                message.IsDeliveryReceiptRequested = MailInfo.IsDeliveryReceiptRequest;

                //Warning: If you add mail address as string, delivery notification cant arrive to from mail. It types must EmailAddress.
                if (MailInfo.ToRecipients != null)
                {
                    MailInfo.ToRecipients.ToList().ForEach(to =>
                    {
                        message.ToRecipients.Add(new EmailAddress(to));
                    });
                }

                if (MailInfo.CcRecipients != null)
                {
                    MailInfo.CcRecipients.ToList().ForEach(cc =>
                    {
                        message.CcRecipients.Add(new EmailAddress(cc));
                    });
                }

                if (MailInfo.BccRecipients != null)
                {
                    MailInfo.BccRecipients.ToList().ForEach(Bcc =>
                    {
                        message.BccRecipients.Add(new EmailAddress(Bcc));
                    });
                }

                message.Send();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
