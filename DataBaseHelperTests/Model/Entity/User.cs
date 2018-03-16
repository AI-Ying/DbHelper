using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataBaseHelper;

namespace DataBaseHelperTests.Model.Entity
{
    [DbTable("UserInfo")]
    public class User
    {
        [DbDataField("UName")]
        [DataMember]
        public string UserName { get; set; }
        [DbDataField("UPassword")]
        [DataMember]
        public string UserPassword { get; set; }
    }
}
