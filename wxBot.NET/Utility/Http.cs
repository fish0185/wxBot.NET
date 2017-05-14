using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer = CookiesContainer,
                ClientCertificateOptions = ClientCertificateOption.Automatic
            };
            //Origin: https://web.wechat.com
            HttpClient = new HttpClient(handler);
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            HttpClient.DefaultRequestHeaders.ExpectContinue = false;
            HttpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            HttpClient.DefaultRequestHeaders.Add("Origin", "https://web.wechat.com");
            HttpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            HttpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            HttpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.8,zh-CN;q=0.6,zh-TW;q=0.4");
            HttpClient.DefaultRequestHeaders.Add("Referer", "https://web.wechat.com/?&lang=en_US");
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
                var client = GetHttpClient();
                var result = client.GetStreamAsync(getUrl).Result;
                var streamReader = new StreamReader(result, Encoding.UTF8);
                var content = streamReader.ReadToEnd();
                return content;
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
        public string WebPost(string postUrl, string strPost)
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
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }

        public CookieCollection GetAllCookies(CookieContainer cookieJar)
        {
            CookieCollection cookieCollection = new CookieCollection();

            Hashtable table = (Hashtable)cookieJar.GetType().InvokeMember("m_domainTable",
                BindingFlags.NonPublic |
                BindingFlags.GetField |
                BindingFlags.Instance,
                null,
                cookieJar,
                new object[] { });

            foreach (var tableKey in table.Keys)
            {
                String str_tableKey = (string)tableKey;

                if (str_tableKey[0] == '.')
                {
                    str_tableKey = str_tableKey.Substring(1);
                }

                SortedList list = (SortedList)table[tableKey].GetType().InvokeMember("m_list",
                    BindingFlags.NonPublic |
                    BindingFlags.GetField |
                    BindingFlags.Instance,
                    null,
                    table[tableKey],
                    new object[] { });

                foreach (var listKey in list.Keys)
                {
                    String url = "https://" + str_tableKey + (string)listKey;
                    cookieCollection.Add(cookieJar.GetCookies(new Uri(url)));
                }
            }

            return cookieCollection;
        }

        public string WebOption(string postUrl)
        {
            string strResult = "";
            try
            {
                var client = GetHttpClient();
                var request = new HttpRequestMessage(HttpMethod.Options, postUrl);
                var xx = client.SendAsync(request).Result;
                var res = xx.Content.ReadAsStringAsync().Result;
                return res;
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }

        public string Upload(string filePath, string passTicket, string uploadmediarequestJSON, string url)
        {
            var client = GetHttpClient();
            using (var content =
                    new MultipartFormDataContent("----whateverboundary"))
                {
                    var id = new StringContent("WU_FILE_0");
                    //id.Headers.Clear();
                    id.Headers.Add("Content-Disposition", "form-data; name=\"id\"");
                    content.Add(id);
                    var name = new StringContent(filePath);
                   // name.Headers.Clear();
                    name.Headers.Add("Content-Disposition", "form-data; name=\"name\"");
                    content.Add(name);
                    var type = new StringContent("image/png");
                    //type.Headers.Clear();
                    type.Headers.Add("Content-Disposition", "form-data; name=\"type\"");
                    content.Add(type);
                    var lastModifiedDate = new StringContent("Sun Feb 19 2017 20:00:10 GMT+1100 (AUS Eastern Daylight Time)");
                    //lastModifiedDate.Headers.Clear();
                    lastModifiedDate.Headers.Add("Content-Disposition", "form-data; name=\"lastModifieDate\"");
                    content.Add(lastModifiedDate);
                    var size = new StringContent("28547");
                    //size.Headers.Clear();
                    size.Headers.Add("Content-Disposition", "form-data; name=\"size\"");
                    content.Add(size);
                    var mediatype = new StringContent("pic");
                    //mediatype.Headers.Clear();
                    mediatype.Headers.Add("Content-Disposition", "form-data; name=\"mediatype\"");
                    content.Add(mediatype);
                    var uploadmediarequest = new StringContent(uploadmediarequestJSON);
                    //uploadmediarequest.Headers.Clear();
                    uploadmediarequest.Headers.Add("Content-Disposition", "form-data; name=\"uploadmediarequest\"");
                    content.Add(uploadmediarequest);
                    var cookies = GetAllCookies(CookiesContainer);
                    var webwx_data_ticketValue = cookies["webwx_data_ticket"].Value;
                    var webwx_data_ticket = new StringContent(webwx_data_ticketValue);
                    //webwx_data_ticket.Headers.Clear();
                    webwx_data_ticket.Headers.Add("Content-Disposition", "form-data; name=\"webwx_data_ticket\"");
                    content.Add(webwx_data_ticket);
                    var pass_ticket = new StringContent(passTicket);
                    //pass_ticket.Headers.Clear();
                    pass_ticket.Headers.Add("Content-Disposition", "form-data; name=\"pass_ticket\"");
                    content.Add(pass_ticket);
          
                    FileStream fs = File.OpenRead(filePath);

                    var streamContent = new StreamContent(fs);

                    streamContent.Headers.Add("Content-Disposition", "form-data; name=\"filename\"; filename=\"SJBZ5400.png\"");
                    streamContent.Headers.Add("Content-Type", "image/png");
                content.Add(streamContent);


                 var message =
                        client.PostAsync(url, content).Result;                    
                    var input =  message.Content.ReadAsStringAsync().Result;
                    return input;                    
               }
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