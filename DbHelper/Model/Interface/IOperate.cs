using System.Collections.Generic;

namespace DataBaseHelper
{
    public interface IOperate : IDbHelper, IMapHelper
    {
        List<T> Query<T>(T entity) where T : new();
        int Add<T>(T entity) where T : new();
        int Delete<T>(T entity) where T : new();
        int Update<T>(T entity, T prerequisite) where T : new();
    }
}
