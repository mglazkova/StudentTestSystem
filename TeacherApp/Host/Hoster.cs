using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFServiceLibrary;

namespace TeacherApp.Host
{
    public static class Hoster
    {
        public static void StartHost()
        {
            ServiceHost host = new ServiceHost(typeof(TestSystemService));
            host.Open();
        }

        public static bool ServiceStatus { get; set; }
    }
}
