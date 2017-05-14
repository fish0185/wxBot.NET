using System;
using System.Collections.Generic;
using System.IO;
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
            var fileInfo = new FileInfo(@"C:\Users\Gary\Desktop\SJBZ5400.png");
            var fileLen = fileInfo.Length;

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
