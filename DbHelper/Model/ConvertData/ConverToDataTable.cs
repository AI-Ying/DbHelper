using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper
{
    public partial class ConverData : ConverDataCommon
    {
        /// <summary>
        /// 把实体集合转换成DataTable类型
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetListDataTable<T>(List<T> list) where T : new()
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                DataTable dt = map.CreateDataTable<T>(entity.GetType());
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        dt.Rows.Add(GetEntityDataRow<T>(entity));
                    }
                }
                return dt == null ? null : dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 把实体对象转换成DataRow类型
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>返回一个DataRow类型对象</returns>
        public DataRow GetEntityDataRow<T>(T entity)
        {
            try
            {
                Type type = entity.GetType();
                DataRow row = map.CreateDataTable<T>(type).NewRow();
                PropertyInfo[] proInfo = type.GetProperties();
                foreach (var p in proInfo)
                {
                    string fieldName = map.GetFieldAttribute(p).FieldName;
                    row[fieldName] = p.GetValue(entity);
                }
                return row;
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}
