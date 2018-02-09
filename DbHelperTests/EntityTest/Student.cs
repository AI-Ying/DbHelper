using System.Runtime.Serialization;
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
