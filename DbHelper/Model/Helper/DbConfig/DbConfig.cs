using System.Collections.Generic;
using System.Configuration;

namespace DataBaseHelper
{
    public static class DbConfig
    {
        public static List<string> ConnectionStrings
        {
            get
            {
                List<string> connectionStrings = new List<string>();
                for (int i = 1; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    connectionStrings.Add(ConfigurationManager.ConnectionStrings[i].ConnectionString);
                }
                return connectionStrings;
            }
        }
        public static List<string> ProviderNames
        {
            get
            {
                List<string> providerNames = new List<string>();
                for (int i = 1  ; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    providerNames.Add(ConfigurationManager.ConnectionStrings[i].ProviderName);
                }
                return providerNames;
            }
        }
    }
}
