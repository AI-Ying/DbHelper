using System.Collections.Generic;

namespace DataBaseHelper
{
    public interface IOperate : IMapHelper
    {
        List<T> Query<T>();
        List<T> Query<T>(T entity);
        int Add<T>(T entity);
        int AddRange<T>(List<T> list) where T : class;
        int Delete<T>(T entity);
        int DeleteRange<T>(List<T> list) where T : class;
        int Update<T>(T entity, T prerequisite); 
    }
}
