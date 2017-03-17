using System.Configuration;
using LP.Api.Shared.Interfaces.Api;

namespace LP.Host.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string FrontEndWebUrl { get { return GetStringSettingValue("FrontEndWebUrl"); } }

        private static int GetIntSettingValue(string appSettingsKey, int defaultValue)
        {
            var setting = ConfigurationManager.AppSettings[appSettingsKey];

            int intToTryParse;

            int.TryParse(setting, out intToTryParse);

            if (int.TryParse(setting, out intToTryParse))
            {
                return intToTryParse;
            }

            return defaultValue;
        }

        private static string GetStringSettingValue(string appSettingsKey)
        {
            return ConfigurationManager.AppSettings[appSettingsKey];

        }
    }
}