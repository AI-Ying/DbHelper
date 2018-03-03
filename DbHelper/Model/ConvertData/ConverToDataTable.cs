using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DataBaseHelper
{
    public partial class ConverData : ConverDataHelper, IConverData
    {
        /// <summary>
        /// 把实体集合转换成DataTable类型
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回一个DataTable</returns>
        public DataTable GetListDataTable<T>(List<T> list)
        {
            try
            {
                T entity = Activator.CreateInstance<T>();
                PropertyInfo[] proInfo = entity.GetType().GetProperties();
                DataTable dt = map.CreateDataTable<T>(entity.GetType());
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        foreach (var p in proInfo)
                        {
                            row[p.Name] = p.GetValue(list[i]);
                        }
                        dt.Rows.Add(row);
                    }
                }
                return dt == null ? null : dt;
            }
            catch (Exception e)
            {
                Log.Error("实体集合转换成DataTable类型失败", e);
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
                    row[p.Name] = p.GetValue(entity);
                }
                return row;
            }
            catch (Exception e)
            {
                Log.Error("把实体对象转换成DataRow类型失败", e);
                throw e;
            }
        }

    }
}
