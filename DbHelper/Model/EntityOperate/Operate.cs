﻿using System;
using System.Collections.Generic;

namespace DataBaseHelper
{
    public class Operate : IOperate
    {
        public MapHelper map { get { return new MapHelper(); } set { } }
        public DbHelper db { get { return new DbHelper(); } set { } }


        //public string InsertSqlString<T>(T entity)
        //{
        //    try
        //    {
        //        Type type = entity.GetType();

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public int Add<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(T entity) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity, T prerequisite) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
