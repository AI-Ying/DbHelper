using System.Data;

namespace DataBaseHelper
{
    interface IPageQuery : IMapHelper
    {
        DataTable QueryWhere<T>(T entity);
        DataTable QueryNonWhere<T>();
    }
}
