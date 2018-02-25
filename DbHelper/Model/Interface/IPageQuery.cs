using System.Data;

namespace DataBaseHelper
{
    interface IPageQuery : IDbHelper, IMapHelper
    {
        DataTable QueryWhere<T>(T entity);
        DataTable QueryNonWhere<T>();
    }
}
