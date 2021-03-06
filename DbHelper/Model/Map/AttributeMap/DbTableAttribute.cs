﻿using System;

namespace DataBaseHelper
{
    /// <summary>
    /// 定义实体类特性，用来反射数据库表名。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableAttribute : Attribute
    {
        public string TableName { get; set; }
        public DbTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
