using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wxBot.NET
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(
                    () =>
                    {
                        SimpleWXbot newbot = new SimpleWXbot();
                        newbot.run();
                    }, TaskCreationOptions.LongRunning);

            Thread.Sleep(20000);

            var task2 = Task.Factory.StartNew(
                    () =>
                    {
                        SimpleWXbot newbot = new SimpleWXbot();
                        newbot.run();
                    }, TaskCreationOptions.LongRunning);
            Console.ReadKey();
        }
    }
}
