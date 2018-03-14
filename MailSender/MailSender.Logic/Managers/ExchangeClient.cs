using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Exchange.WebServices.Data;

namespace MailSender.Clients.Managers
{
    public class ExchangeClient : Singleton<ExchangeClient>, IMailSender
    {

        #region ..Fields..

        ExchangeService client;

        #endregion

        #region ..Constructor..

        public ExchangeClient()
        {
            this.client = new ExchangeService();
        }

        #endregion

        #region IMailSender Implementations

        public void InitializeClient(MailSettings Credential)
        {
            client.Url = new Uri(Credential.Url);
            client.UseDefaultCredentials = false;

            client.Credentials =new WebCredentials(Credential.Usermail, Credential.Password, Credential.Domain);

            client.UseDefaultCredentials = true;

            client.AutodiscoverUrl(Credential.Usermail);
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
