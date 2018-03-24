using MailSender.Contracts.Enums;
using MailSender.Contracts.Types;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MailSender.Logic.Managers
{
    public class MailSettingsManager
    {
        #region ..Fields..

        IConfiguration Configuration;
        Dictionary<eMailClientType, MailSettings> MailSettingsCache;
        #endregion

        #region ..Constructor..

        public MailSettingsManager(string path, string filename) 
        {
            MailSettingsCache = new Dictionary<eMailClientType, MailSettings>();

            var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile(filename);

            Configuration = builder.Build();
        }

        #endregion

        #region ..Public..

        public MailSettings GetMailSettings(eMailClientType clientType)
        {
            if (MailSettingsCache.ContainsKey(clientType))
                return MailSettingsCache[clientType];

            MailSettings clientMailSettings = GenerateMailSettingsFromJsonFile(GetMailSection(clientType));
            MailSettingsCache.Add(clientType, clientMailSettings);

            return MailSettingsCache[clientType];
        }

        #endregion

        #region ..Privates..

        /// <summary>
        /// Generating MailSettings from Settings JSON file
        /// </summary>
        /// <param name="mailSection">Smtp/Exchange Section Name</param>
        /// <returns></returns>
        private MailSettings GenerateMailSettingsFromJsonFile(string mailSection)
        {
            return new MailSettings()
            {
                Port = Convert.ToInt32(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.Port))]),
                Domain = Convert.ToString(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.Domain))]),
                EnableSsl = Convert.ToBoolean(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.EnableSsl))]),
                Password = Convert.ToString(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.Password))]),
                Url = Convert.ToString(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.Url))]),
                Usermail = Convert.ToString(Configuration[string.Concat(mailSection, ":", nameof(MailSettings.Usermail))]),
            };
        }

        private string GetMailSection(eMailClientType clientType)
        {
            switch (clientType)
            {
                case eMailClientType.Exchange:
                    return Constants.ExchangeMailSection;
                case eMailClientType.Smtp:
                default:
                    return Constants.SmtpMailSection;
            }
        }

        #endregion

    }
}
