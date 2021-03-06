﻿using System;

namespace DataBaseHelper
{
    /// <summary>
    /// 定义实体类属性特性，用来反射数据库表的字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbDataFieldAttribute : Attribute
    {
        public string FieldName { get; set; }
        public DbDataFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
