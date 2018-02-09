using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Text;

namespace DataBaseHelper
{
    public partial class ConverData
    {


        //#region OldSerialize

        ///// <summary>
        ///// 格式化一个对象类型为字符串
        ///// </summary>
        ///// <param name="obj">对象值</param>
        ///// <param name="objType">对象类型</param>
        ///// <returns>返回一个字符串</returns>
        //public string FormatTypeValue(object obj, Type objType)
        //{
        //    try
        //    {
        //        if (objType == typeof(int) || objType == typeof(Double))
        //        {
        //            return $"{obj},";
        //        }
        //        else if (objType == typeof(string))
        //        {
        //            return $"\"{obj}\",";
        //        }
        //        else
        //        {
        //            return $"\"null\",";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 把实体对象转化成json数据格式
        ///// </summary>
        ///// <typeparam name="T">泛型实体</typeparam>
        ///// <param name="entity">实体类型</param>
        ///// <returns>返回json字符串</returns>
        //public string EntityToJson<T>(T entity) where T : new()
        //{
        //    try
        //    {
        //        StringBuilder json = new StringBuilder();
        //        Type entityType = entity.GetType();
        //        PropertyInfo[] proInfo = entityType.GetProperties();
        //        json.Append("{");
        //        foreach (var p in proInfo)
        //        {
        //            json.Append($"\"{p.Name}\":");
        //            string value = FormatTypeValue(p.GetValue(entity), p.PropertyType);
        //            json.Append(value);
        //        }
        //        json.Remove(json.Length - 1, 1);
        //        json.Append("}");
        //        return json.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 把实体对象集合转换成json数据格式
        ///// </summary>
        ///// <typeparam name="T">泛型实体</typeparam>
        ///// <param name="list">实体集合</param>
        ///// <returns>返回一个json字符串</returns>
        //public string ListToJson<T>(List<T> list) where T : new()
        //{
        //    try
        //    {
        //        StringBuilder json = new StringBuilder();
        //        json.Append("[");
        //        foreach (var item in list)
        //        {
        //            json.Append($"{EntityToJson(item)},");
        //        }
        //        json.Remove(json.Length - 1, 1);
        //        json.Append("]");
        //        return json.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 把DataTable转换成json数据格式
        ///// </summary>
        ///// <param name="dt">DataTable类型数据</param>
        ///// <returns>返回一个json字符串</returns>
        //public string DataTableToJson(DataTable dt)
        //{
        //    try
        //    {
        //        StringBuilder json = new StringBuilder();
        //        string jsonString = string.Empty;
        //        json.Append('[');
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            json.Append('{');
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                json.Append($"\"{dt.Columns[j].ColumnName}\":");
        //                string value = FormatTypeValue(dt.Rows[i][j], dt.Columns[j].DataType);
        //                json.Append(value);
        //            }
        //            json.Remove(json.Length - 1, 1);
        //            json.Append("},");
        //        }
        //        json.Remove(json.Length - 1, 1);
        //        json.Append("]");
        //        return json.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        ///// <summary>
        ///// 对EntityToJson的封装
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public string JsonSerialize<T>(T entity) where T : new()
        //{
        //    try
        //    {
        //        return EntityToJson(entity);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 对ListToJson的封装
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public string JsonSerialize<T>(List<T> list) where T : new()
        //{
        //    try
        //    {
        //        return ListToJson(list);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 对DataTableToJson的封装
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public string JsonSerialize(DataTable dt)
        //{
        //    try
        //    {
        //        return DataTableToJson(dt);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //#endregion

        #region New Serialize
        
        /// <summary>
        /// 把数据序列化为Json数据对象
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="obj">对象类型</param>
        /// <returns>返回json字符串对象</returns>
        public override string JsonSerializer<T>(T obj)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream, obj);
                byte[] json = stream.ToArray();
                stream.Close();
                
                return Encoding.UTF8.GetString(json, 0, json.Length);                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///// <summary>
        ///// 格式化一个对象类型为字符串
        ///// </summary>
        ///// <param name="obj">对象值</param>
        ///// <param name="objType">对象类型</param>
        ///// <returns>返回一个字符串</returns>
        //public string FormatTypeValue(object obj, Type objType)
        //{
        //    try
        //    {
        //        if (objType == typeof(int) || objType == typeof(Double))
        //        {
        //            return $"{obj},";
        //        }
        //        else if (objType == typeof(string))
        //        {
        //            return $"\"{obj}\",";
        //        }
        //        else
        //        {
        //            return $"\"null\",";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 把DataTable转换成json数据格式
        ///// </summary>
        ///// <param name="dt">DataTable类型数据</param>
        ///// <returns>返回一个json字符串</returns>
        //public string DataTableToJson(DataTable dt)
        //{
        //    try
        //    {
        //        StringBuilder jsonString = new StringBuilder();
        //        jsonString.Append($"[\"{dt.TableName}\":[");
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            jsonString.Append('{');
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                jsonString.Append($"\"{dt.Columns[j].ColumnName}\":");
        //                string value = FormatTypeValue(dt.Rows[i][j], dt.Columns[j].DataType);
        //                jsonString.Append(value);
        //            }
        //            jsonString.Remove(jsonString.Length - 1, 1);
        //            jsonString.Append("},");
        //        }
        //        jsonString.Remove(jsonString.Length - 1, 1);
        //        jsonString.Append("]]");
        //        byte[] json = Encoding.UTF8.GetBytes(jsonString.ToString());
        //        return Encoding.UTF8.GetString(json, 0, json.Length);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>
        ///// 把DataTable转换成json数据格式
        ///// </summary>
        ///// <param name="dt">DataTable类型数据</param>
        ///// <returns>返回一个json字符串</returns>
        //public string JsonSerializer(DataTable dt)
        //{
        //    try
        //    {
        //        return DataTableToJson(dt);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        #endregion

        #region Deserialize

        /// <summary>
        /// 把json数据反序列化为相对应的类型数据（集合或者对象）
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="json">反序列化的json数据</param>
        /// <returns>返回反序列化好的类型数据</returns>
        public T JsonDeserializer<T>(string json) where T : class
        {
            try
            {
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                T obj = ser.ReadObject(stream) as T;
                stream.Close();
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public DataTable JsonToDatatable<T>(string json)
        //{
        //    try
        //    {
        //        DataTable dt = null; 

        //        return dt;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        #endregion
    }
}
