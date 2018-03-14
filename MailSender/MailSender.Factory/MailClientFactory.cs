using MailSender.Clients.Managers;
using MailSender.Contracts.Enums;
using MailSender.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Clients.Factory
{
    public class MailClientFactory
    {

        #region ..Fields..

        private Dictionary<eMailClientType, IMailSender> MailClients;

        #endregion

        #region ..Constructor..

        public MailClientFactory()
        {
            MailClients = new Dictionary<eMailClientType, IMailSender>();
        }

        #endregion

        #region ..Publics..

        public IMailSender GetMailSender(eMailClientType clientType)
        {
            if (!MailClients.ContainsKey(clientType))
                MailClients.Add(clientType, GenerateClient(clientType));

            return MailClients[clientType];
        }

        #endregion

        #region ..Privates..

        private IMailSender GenerateClient(eMailClientType clientType)
        {
            switch (clientType)
            {
                case eMailClientType.Smtp:
                default:
                    return SmptClient.Instance;
                case eMailClientType.Exchange:
                    return ExchangeClient.Instance;
            }
        }

        #endregion

    }
}
