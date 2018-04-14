using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFServiceLibrary;

namespace ServiceHostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Host opening....");
                var serviceHost = new ServiceHost(typeof(TestSystemService));
                serviceHost.Open();
                var address = serviceHost.BaseAddresses.FirstOrDefault();
                Console.WriteLine("Host opened! Address = "+address);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
