using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseHelper
{
    interface IPageQuery 
    {
        DBHelper db { get; set; }
        Ado ado { get; set; }
        DataTable QueryWhere<T>(T entity) where T : new();
        DataTable QueryNonWhere<T>() where T : new();
    }
}
