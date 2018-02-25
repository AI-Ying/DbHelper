using System.Collections.Generic;

namespace DataBaseHelper
{
    public interface IOperate : IDbHelper, IMapHelper
    {
        List<T> Query<T>(T entity);
        int Add<T>(T entity);
        int Delete<T>(T entity);
        int Update<T>(T entity, T prerequisite); 
    }
}
