using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFServiceLibrary.Enum;

namespace TeacherApp.Helper
{
    public class StatusFilterItem
    {
        public string Name { get; set; }
        public TestAttemptStatus? Status { get; set; }
        
    }
}
