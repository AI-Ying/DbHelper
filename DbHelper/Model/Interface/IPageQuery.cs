using System.Data;
using DataBaseHelper.Helper;
using DataBaseHelper.Map;

namespace DataBaseHelper
{
    interface IPageQuery 
    {
        DbHelper db { get; set; }
        DbEntityMap ado { get; set; }
        DataTable QueryWhere<T>(T entity) where T : new();
        DataTable QueryNonWhere<T>() where T : new();
    }
}
