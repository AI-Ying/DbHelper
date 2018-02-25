using System.Collections.Generic;
using System.Data;


namespace DataBaseHelper
{
    interface IConverData
    {
        #region Json

        string JsonSerializerNonLib<T>(T entity);
        string JsonSerializerNonLib<T>(List<T> list);
        string JsonSerializeNonLib(DataTable dt);
        string JsonSerializerSys<T>(T obj);
        T JsonDeserializerSys<T>(string json) where T : class;
        string JsonSerializerNewtonsoft<T>(T obj);
        T JsonDeserializerNewtonsoft<T>(string json);

        #endregion

        #region List

        List<T> GetTableList<T>(DataTable dt);
        T GetDataRowEntity<T>(DataRow row);

        #endregion

        #region DataTable

        DataTable GetListDataTable<T>(List<T> list);
        DataRow GetEntityDataRow<T>(T entity);

        #endregion

        #region Xml

        string XmlSerializer<T>(T obj);
        T XmlDeserializer<T>(string xml) where T : class;

        #endregion
    }
}
