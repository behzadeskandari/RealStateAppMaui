using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp
{
    public static class AppSettings
    {
        public static string ApiUrl =  DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:7022/" : "http://localhost:7022/";// "http://localhost:7022/";
    }
}
