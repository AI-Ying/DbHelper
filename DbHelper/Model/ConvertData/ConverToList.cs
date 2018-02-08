using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Common;

namespace DataBaseHelper
{
    public partial class ConverData
    {
        /// <summary>
        /// 把DataTable转换成实体类集合。
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="dt">DataTable对象</param>
        /// <returns>返回一个实体类集合</returns>
        public List<T> GetTableList<T>(DataTable dt)
        {
            try
            {
                List<T> list = new List<T>();
                foreach (DataRow row in dt.Rows)
                {                    
                    list.Add(GetDataRowEntity<T>(row));
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把DataRow转换成实体类对象
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="row">DataRow</param>
        /// <returns>返回一个T类型的实体对象</returns>
        public T GetDataRowEntity<T>(DataRow row)
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                Type entityType = entity.GetType();
                PropertyInfo[] proInfo = entityType.GetProperties();
                foreach (PropertyInfo p in proInfo)
                {
                    string fieldName = p.Name;
                    if (row.Table.Columns.Contains(fieldName))
                    {
                        if (row[fieldName] != null)
                        {
                            p.SetValue(entity, row[fieldName]);
                        }
                    }
                }
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
