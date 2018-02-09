namespace DataBaseHelper
{
    public abstract class ConverDataCommon : IDbHelper, IMapHelper
    {
        public virtual MapHelper map { get { return new MapHelper(); } set { } }
        public virtual DbHelper db { get { return new DbHelper(); } set { } }
        public abstract string JsonSerializer<T>(T obj);

    }
}
