using System.Collections.Generic;
using System.Data;

namespace DataBaseHelper
{
    public class ConverDataHelper : IMapHelper, IDbHelper
    {
        public virtual MapHelper map { get { return new MapHelper(); } set { } }
        public virtual DbHelper db { get { return new DbHelper(); } set { } }

    }
}
