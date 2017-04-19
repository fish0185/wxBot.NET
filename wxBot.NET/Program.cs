using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wxBot.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(System.Net.ServicePointManager.SecurityProtocol.ToString());
            //ServicePointManager.DefaultConnectionLimit = 65000;
            //ServicePointManager.MaxServicePoints = 10;
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ////Console.WriteLine(System.Net.ServicePointManager.SecurityProtocol.ToString());
            //ServicePointManager.ServerCertificateValidationCallback +=
            //    (sender, cert, chain, sslPolicyErrors) => true;
            var task1 = Task.Factory.StartNew(() =>
            {
                SimpleWXbot newbot = new SimpleWXbot();
                newbot.run();
            });

            Console.ReadLine();

            var task2 = Task.Factory.StartNew(() =>
            {
                SimpleWXbot newbot = new SimpleWXbot();
                newbot.run();
            });

            Console.ReadLine();


            var task3 = Task.Factory.StartNew(() =>
            {
                SimpleWXbot newbot = new SimpleWXbot();
                newbot.run();
            });

            //SimpleWXbot newbot3 = new SimpleWXbot();
            //newbot3.run();

            Task.WaitAll(task1, task2, task3);
        }
    }
}
