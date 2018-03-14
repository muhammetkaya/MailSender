using MailSender.Clients.Factory;
using MailSender.Contracts.Enums;
using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using System;

namespace MailSender.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            MailClientFactory MyMailClientFactory = new MailClientFactory();

            var exchangeMailSender = MyMailClientFactory.GetMailSender(eMailClientType.Exchange);
            var smtpMailSender = MyMailClientFactory.GetMailSender(eMailClientType.Smtp);

            //Mail will be sended with Exchange Server
            SendMail(exchangeMailSender);

            //Mail will be sended with Smptp Server
            SendMail(smtpMailSender);
        }

        private static void SendMail(IMailSender mailSender)
        {
            MailInfo mailInfo = new MailInfo()
            {
                Body = "This is test body.",
                Subject = "This Test Mail",
                IsBodyHtml = false,
                IsDeliveryReceiptRequest = true,
                IsReadReceiptRequest = true,
                ToReceipts = new string[] { "mami3509@gmail.com" }
            };

            MailSettings credentialSettings = new MailSettings()
            {

            };

            //Client is initializing
            mailSender.InitializeClient(credentialSettings);
            //Mail is sending
            mailSender.SendMail(mailInfo);
        }

    }
}
