using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    using WebSocketSharp;

    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:8078/socket"))
            {
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                ws.Send("Test");
                Console.ReadKey(true);
            }
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Laputa says: " + e.Data);
        }
    }
}
