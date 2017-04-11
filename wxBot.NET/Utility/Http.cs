using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace wxBot.NET
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class Http
    {
        /// <summary>
        /// 整个Session的cookie
        /// </summary>
        public static CookieContainer CookiesContainer;

        private static HttpClient HttpClient;

        public static HttpClient GetHttpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            if (HttpClient != null)
            {
                return HttpClient;
            }

            if (CookiesContainer == null)
            {
                CookiesContainer = new CookieContainer();
            }
            
            var handler = new HttpClientHandler { CookieContainer = CookiesContainer };
           
            HttpClient = new HttpClient(handler);
            return HttpClient;
       }

        /// <summary>
        /// Session带Cookie的HTTPGET
        /// </summary>
        /// <param name="getUrl"></param>
        /// <returns></returns>
        public static string WebGet(string getUrl)
        {
            string strResult = "";
            try
            {
                var client = GetHttpClient();
                
                    //var content = new FormUrlEncodedContent(new[]
                    //{
                    //    new KeyValuePair<string, string>("foo", "bar"),
                    //    new KeyValuePair<string, string>("baz", "bazinga"),
                    //});
                    //cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                    var result = client.GetStreamAsync(getUrl).Result;
                    var streamReader= new StreamReader(result, Encoding.UTF8);
                    var content = streamReader.ReadToEnd();
                    return content;
                


                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
                //request.Method = "GET";
                //if (CookiesContainer == null)
                //{
                //    CookiesContainer = new CookieContainer();
                //}
                //request.CookieContainer = CookiesContainer;  //启用cookie
                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream stream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                //strResult = reader.ReadToEnd();
                //response.Close();
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }
        /// <summary>
        /// Session带Cookie的HTTPPOIST
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="strPost"></param>
        /// <returns></returns>
        public static string WebPost(string postUrl, string strPost)
        {
            string strResult = "";
            try
            {
                var client = GetHttpClient();
                byte[] bs = Encoding.UTF8.GetBytes(strPost);
                var content = new ByteArrayContent(bs);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


                var xx = client.PostAsync(postUrl, content).Result;
                
                var res = xx.Content.ReadAsStringAsync().Result;
                return res;
                //Encoding encoding = Encoding.UTF8;
                ////encoding.GetBytes(postData);
                //byte[] bs = Encoding.UTF8.GetBytes(strPost);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                string responseData = String.Empty;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
                request.CookieContainer = CookiesContainer;  //启用cookie
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }
        /// <summary>
        /// 新的不带Cookie的HTTPPOIST
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="strPost"></param>
        /// <returns></returns>
        public static string WebPost2(string postUrl, string strPost)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                Encoding encoding = Encoding.UTF8;
                //encoding.GetBytes(postData);
                byte[] bs = Encoding.UTF8.GetBytes(strPost);
                string responseData = String.Empty;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.ContentType = "application/json; charset=UTF-8";
                if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
                request.CookieContainer = CookiesContainer;  //启用cookie
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }
    }
}
