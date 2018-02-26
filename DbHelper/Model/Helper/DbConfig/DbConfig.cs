using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper
{
    public static class DbConfig
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static string providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        public static string ConnectionString
        {
            get { return connectionString; }
        }
        public static string ProviderName
        {
            get { return providerName; }
        }

    }
}
