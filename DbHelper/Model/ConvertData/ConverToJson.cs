using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace DataBaseHelper
{
    public partial class ConverData : ConverDataHelper, IConverData
    {

        #region 不使用Json库

        /// <summary>
        /// 格式化一个对象类型为字符串
        /// </summary>
        /// <param name="obj">对象值</param>
        /// <param name="objType">对象类型</param>
        /// <returns>返回一个字符串</returns>
        private string FormatTypeValue(object obj, Type objType)
        {
            try
            {
                if (objType == typeof(int) || objType == typeof(Double))
                {
                    return $"{obj},";
                }
                else if (objType == typeof(string))
                {
                    return $"\"{obj}\",";
                }
                else
                {
                    return $"\"null\",";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把实体对象转化成json数据格式
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="entity">实体类型</param>
        /// <returns>返回json字符串</returns>
        private string EntityToJson<T>(T entity)
        {
            try
            {
                StringBuilder json = new StringBuilder();
                Type entityType = entity.GetType();
                PropertyInfo[] proInfo = entityType.GetProperties();
                json.Append("{");
                foreach (var p in proInfo)
                {
                    json.Append($"\"{p.Name}\":");
                    string value = FormatTypeValue(p.GetValue(entity), p.PropertyType);
                    json.Append(value);
                }
                json.Remove(json.Length - 1, 1);
                json.Append("}");
                return json.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把实体对象集合转换成json数据格式
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>返回一个json字符串</returns>
        private string ListToJson<T>(List<T> list)
        {
            try
            {
                StringBuilder json = new StringBuilder();
                json.Append("[");
                foreach (var item in list)
                {
                    json.Append($"{EntityToJson(item)},");
                }
                json.Remove(json.Length - 1, 1);
                json.Append("]");
                return json.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把DataTable转换成json数据格式
        /// </summary>
        /// <param name="dt">DataTable类型数据</param>
        /// <returns>返回一个json字符串</returns>
        private string DataTableToJson(DataTable dt)
        {
            try
            {
                StringBuilder json = new StringBuilder();
                string jsonString = string.Empty;
                json.Append('[');
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    json.Append('{');
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        json.Append($"\"{dt.Columns[j].ColumnName}\":");
                        string value = FormatTypeValue(dt.Rows[i][j], dt.Columns[j].DataType);
                        json.Append(value);
                    }
                    json.Remove(json.Length - 1, 1);
                    json.Append("},");
                }
                json.Remove(json.Length - 1, 1);
                json.Append("]");
                return json.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 对EntityToJson的封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string JsonSerializerNonLib<T>(T entity)
        {
            try
            {
                return EntityToJson(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 对ListToJson的封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public string JsonSerializerNonLib<T>(List<T> list)
        {
            try
            {
                return ListToJson(list);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 对DataTableToJson的封装
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string JsonSerializerNonLib(DataTable dt)
        {
            try
            {
                return DataTableToJson(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region 使用System.Runtime.Serialization.Json库

        /// <summary>
        /// 把数据序列化为Json数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="obj">对象类型</param>
        /// <returns>返回json字符串对象</returns>
        public string JsonSerializerSys<T>(T obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    ser.WriteObject(stream, obj);
                    byte[] json = stream.ToArray();
                    return Encoding.UTF8.GetString(json, 0, json.Length);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 把json数据反序列化为相对应的类型数据（集合或者对象）
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="json">反序列化的json数据</param>
        /// <returns>返回反序列化好的类型数据</returns>
        public T JsonDeserializerSys<T>(string json) where T : class
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    T obj = ser.ReadObject(stream) as T;
                    return obj;
                }          
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region 使用Newtonsoft库
        /// <summary>
        /// 把数据序列化为Json数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string JsonSerializerNewtonsoft<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 把Json数据反序列化为相对应的类型数据（集合或者对象）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T JsonDeserializerNewtonsoft<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
