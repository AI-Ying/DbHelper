using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelper;

namespace DataBaseHelper
{
    public interface IOperate 
    {
        DBHelper db { get; set; }
        Ado ado { get; set; }
        List<T> Query<T>(T entity) where T : new();
        int Add<T>(T entity) where T : new();
        int Delete<T>(T entity) where T : new();
        int Update<T>(T entity, T prerequisite) where T : new();
    }
}
