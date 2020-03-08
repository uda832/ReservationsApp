using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationsAPI.Utilities
{
    public static class HealthCheckUtility
    {
        private const string LOAD_BALANCER_PATH_ROOT = "C:\\AppHealthCheck";

        public static string GetStatus()
        {
            string status = "";
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string filePath = LOAD_BALANCER_PATH_ROOT + "\\" + assemblyName + ".offline.txt";

            status = System.IO.File.Exists(filePath) 
                ? "offline" 
                : assemblyName + " is online at " + DateTime.UtcNow.ToString();

            return status;
        }
    }
}
