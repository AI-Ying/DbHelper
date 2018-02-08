using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelper;


namespace DbHelperTests.EntityTest
{
    [DbTable("Student")]
    [DataContract]
    public class Student
    {
        [DbDataField("StuID")]
        [DataMember]
        public int ID { get; set; }
        [DbDataField("StuAge")]
        [DataMember]
        public int Age { get; set; }
        [DbDataField("StuName")]
        [DataMember]
        public string Name { get; set; }
    }
}
