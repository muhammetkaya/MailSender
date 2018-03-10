using MailSender.Clients.Factory;
using MailSender.Contracts.Enums;
using System;

namespace MailSender.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MailClientFactory My_MailClientFactory = new MailClientFactory();
            var exchangeMailSender = My_MailClientFactory.GetMailSender(eMailClientType.Exchange);
        }
    }
}
