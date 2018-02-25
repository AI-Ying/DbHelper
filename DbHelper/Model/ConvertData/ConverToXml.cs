using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataBaseHelper
{
    public partial class ConverData : ConverDataHelper, IConverData
    {

        /// <summary>
        /// 把相对应的数据格式转换成Xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string XmlSerializer<T>(T obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    ser.Serialize(stream, obj);
                    byte[] xml = stream.ToArray();
                    return Encoding.UTF8.GetString(xml, 0, xml.Length);
                }                   
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 将Xml数据转换成对应的数据格式（实体、集合等）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T XmlDeserializer<T>(string xml) where T : class
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    T obj = ser.Deserialize(stream) as T;
                    return obj;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
