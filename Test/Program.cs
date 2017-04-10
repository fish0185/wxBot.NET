using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    using System.IO;
    using System.Net;

    class Program
    {
        private CookieContainer _cookieContainer;

        public string UserAgent =>
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

        public CookieContainer CurrentCookieContainer => this._cookieContainer;

        static void Main(string[] args)
        {
            var id = new Program().Get(
                "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=1491739754588");
            var id2 = new Program().Get(
               "https://www.google.com");
            var id4 = new Program().Get(
               "https://www.google.com");
            var id3 = new Program().Get(
               "https://login.weixin.qq.com/jslogin?appid=wx782c26e4c19acffb&fun=new&lang=zh_CN&_=1491739754587");
        }

        public string Get(string url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                request.UserAgent = this.UserAgent;
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
