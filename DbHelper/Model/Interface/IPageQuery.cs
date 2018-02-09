using System.Data;

namespace DataBaseHelper
{
    interface IPageQuery : IDbHelper, IMapHelper
    {
        DataTable QueryWhere<T>(T entity) where T : new();
        DataTable QueryNonWhere<T>() where T : new();
    }
}
