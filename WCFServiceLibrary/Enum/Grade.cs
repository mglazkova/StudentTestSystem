using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCFServiceLibrary.Enum
{
    [DataContract]
    public enum Grade
    {
        [EnumMember]
        None=0,
        [EnumMember]
        Poor=1,
        [EnumMember]
        Satisfactory=2,
        [EnumMember]
        Good=3,
        [EnumMember]
        Excellent=4
    }
}
