using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
        public CookieContainer CookiesContainer;

        private HttpClient HttpClient;

        public HttpClient GetHttpClient()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            //ServicePointManager.ServerCertificateValidationCallback +=
            //    (sender, cert, chain, sslPolicyErrors) => true;
            if (HttpClient != null)
            {
                return HttpClient;
            }

            if (CookiesContainer == null)
            {
                CookiesContainer = new CookieContainer();
            }
            
            var handler = new HttpClientHandler
            {
                CookieContainer = CookiesContainer,
            };
           
            HttpClient = new HttpClient(handler);
            return HttpClient;
       }

        /// <summary>
        /// Session带Cookie的HTTPGET
        /// </summary>
        /// <param name="getUrl"></param>
        /// <returns></returns>
        public string WebGet(string getUrl)
        {
            string strResult = "";
            
            try
            {
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
                request.Method = "GET";
                request.KeepAlive = true;
                X509Certificate2Collection certificates = new X509Certificate2Collection();
                certificates.Import(@"C:\cer\FiddlerRoot.cer", "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                request.ClientCertificates = certificates;
                if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
                request.CookieContainer = CookiesContainer;  //启用cookie
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
                //request.Abort();
            }
            return strResult;
        }
        /// <summary>
        /// Session带Cookie的HTTPPOIST
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="strPost"></param>
        /// <returns></returns>
        public string WebPost(string postUrl, string strPost)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                X509Certificate2Collection certificates = new X509Certificate2Collection();
                certificates.Import(@"C:\cer\FiddlerRoot.cer", "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                request.ClientCertificates = certificates;
                Encoding encoding = Encoding.UTF8;
                //encoding.GetBytes(postData);
                byte[] bs = Encoding.UTF8.GetBytes(strPost);
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
        public string WebPost2(string postUrl, string strPost)
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
