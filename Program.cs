using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WinService.NetCore
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            using (var service = new TService())
            {
                ServiceBase.Run(service);
            }
            //var resultData = TService.GetData().Result;
            //Console.WriteLine(resultData.Current + Environment.NewLine + resultData.Place + Environment.NewLine + resultData.Temperature + resultData.Unit);
        }
    }
}
