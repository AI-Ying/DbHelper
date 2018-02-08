using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper
{
    public class Ado
    {
        public DbParameter GetParameters(Type type)
        {
            PropertyInfo[] proInfo = type.GetProperties();
            foreach (var p in proInfo)
            {
                object obj = p.GetCustomAttribute(typeof(DbDataFieldAttribute), true);

            }
        }
    }
}
