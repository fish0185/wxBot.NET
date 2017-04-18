using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    using System.IO;
    using System.Net;
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
               var res = client.GetStringAsync(
                    "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=1491825899172").Result;
                var re2s = client.GetStringAsync(
                    "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=1491825899172").Result;
            }
            //var r1 =
            //    Get("https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?tip=1&uuid=4bL2GHZKxA==&_=1491825899172");
            //var r2 =
            //    Get("https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=1491739754587");
        }

        public static string Get(string url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";
                request.KeepAlive = false;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
                //Todo
            }
            return strResult;
        }
    }
}
