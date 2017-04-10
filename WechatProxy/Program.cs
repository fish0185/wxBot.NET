using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatProxy
{
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class ProxyServer : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "Test" ? "I've been test already...." : "I am not available now.";
            Send(msg);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://localhost:8078");
            wssv.AddWebSocketService<ProxyServer>("/socket");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
