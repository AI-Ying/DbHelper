using System.Collections.Generic;
using DataBaseHelper.Helper;
using DataBaseHelper.Map;

namespace DataBaseHelper
{
    public interface IOperate 
    {
        DbHelper db { get; set; }
        DbEntityMap ado { get; set; }
        List<T> Query<T>(T entity) where T : new();
        int Add<T>(T entity) where T : new();
        int Delete<T>(T entity) where T : new();
        int Update<T>(T entity, T prerequisite) where T : new();
    }
}
