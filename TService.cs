using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WinService.NetCore
{
    internal class TService : ServiceBase
    {
        private static readonly HttpClient client = new HttpClient();
        System.Timers.Timer timer = new System.Timers.Timer();
        private bool resetInterval = true;
        private string filename;
        public TService()
        {
            ServiceName = "TestService";
        }

        protected override void OnStart(string[] args)
        {
            filename = CheckFileExists();
            //File.AppendAllText(filename, $"{DateTime.Now} started.{Environment.NewLine}");

            var minutesAfterHourToStart = 5;  // Start at 5 minutes after the hour

            var now = DateTime.Now;
            var minutesToStart = 60 - (now.Minute - (minutesAfterHourToStart - 1));
            if (minutesToStart > 60) minutesToStart -= 60;
            var secondsToStart = 60 - now.Second + (minutesToStart * 60);

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = TimeSpan.FromSeconds(secondsToStart).TotalMilliseconds;
            timer.Enabled = true;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            if (resetInterval)
            {
                timer.Interval = TimeSpan.FromHours(1).TotalMilliseconds;
                resetInterval = false;
            }

            //File.AppendAllText(filename, $"{DateTime.Now} OnElapsedTime.{Environment.NewLine}");
            var resultData = GetData().Result;
            File.WriteAllText(filename, resultData.Current + Environment.NewLine +
                                        resultData.Place + Environment.NewLine +
                                        resultData.Temperature + resultData.Unit, Encoding.GetEncoding(1200));
        }

        protected override void OnStop()
        {
            //File.AppendAllText(filename, $"{DateTime.Now} stopped.{Environment.NewLine}");
        }

        private static string CheckFileExists()
        {
            string filename = @"C:\Users\denis.poropat\OneDrive - Business Solutions d.o.o\Documents\Rainmeter\Skins\StickyNote\Notes\notes.txt";
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            return filename;
        }

        public static async Task<Repository> GetData()
        {
            var serializer = new DataContractJsonSerializer(typeof(Repository));

            //var streamTask = await client.GetAsync("https://weather-tmin.herokuapp.com/api/wdata/tmin");
            var streamTask = await client.GetAsync("https://weather-tmin.herokuapp.com/api/wdata/solkan");
            streamTask.EnsureSuccessStatusCode();
            var responseBody = await streamTask.Content.ReadAsStreamAsync();
            var repositorie = serializer.ReadObject(responseBody) as Repository;
            return repositorie;
        }
    }
}
