using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper.Helper.DbConfig
{
    public static class DbConfig
    {
        //private static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //private static string providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        private static string connectionString = @"Data Source=localhost; Initial Catalog=DBProvider; User ID=sa; password=xuan";
        private static string providerName = @"System.Data.SqlClient";
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
