using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper
{
    public partial class ConverData
    {
        public Ado ado { get { return new Ado(); } set { } }
        public DBHelper db { get { return new DBHelper(); } set { } }

        /// <summary>
        /// 根据实体，映射DataTable架构
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="type">实体类型</param>
        /// <returns>返回一个DataTable类型</returns>
        public DataTable CreateDataTable<T>(Type type)
        {
            try
            {
                string tableName = ado.GetTableName(type);
                DataTable dt = new DataTable(tableName);
                PropertyInfo[] proInfo = type.GetProperties();
                foreach (var p in proInfo)
                {
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
