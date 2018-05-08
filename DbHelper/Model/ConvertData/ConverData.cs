using System.Collections.Generic;
using System.Data;

namespace DataBaseHelper
{
    public class ConverDataHelper : IMapHelper
    {
        public virtual MapHelper map { get { return new MapHelper(); } set { } }
    }
}
