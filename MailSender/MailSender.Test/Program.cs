using MailSender.Contracts.Enums;
using MailSender.Contracts.Interfaces;
using MailSender.Contracts.Types;
using MailSender.Factory;
using MailSender.Logic.Managers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MailSender.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string filename = "AppSettings.json";

            MailSettingsManager mailSettingsManager = new MailSettingsManager(path, filename);
            MailClientFactory myMailClientFactory = new MailClientFactory();

            var exchangeMailSender = myMailClientFactory.GetMailSender(eMailClientType.Exchange);
            var smtpMailSender = myMailClientFactory.GetMailSender(eMailClientType.Smtp);

            //Mail will be sended with Exchange Server
            //SendMail(exchangeMailSender, mailSettingsManager.GetMailSettings(eMailClientType.Exchange));

            //Mail will be sended with Smptp Server
            SendMail(smtpMailSender, mailSettingsManager.GetMailSettings(eMailClientType.Smtp));
        }

        private static void SendMail(IMailSender mailSender, MailSettings credentialSettings)
        {
            MailInfo mailInfo = new MailInfo()
            {
                From = "Muhammet Kaya",
                Body = "This is test body.",
                Subject = "This Test Mail",
                IsBodyHtml = false,
                IsDeliveryReceiptRequest = true,
                IsReadReceiptRequest = true,
                ToRecipients = new string[] { "muhammetkaya3509@hotmail.com" }
            };


            //Client is initializing
            mailSender.InitializeClient(credentialSettings);
            //Mail is sending
            mailSender.SendMail(mailInfo);
        }

        private static void GetSettings()
        {
            var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json");

        }

    }
}
