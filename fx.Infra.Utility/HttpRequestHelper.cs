using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace fx.Infra.Utility
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// 带参数的Request请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string SendHttpRequest(string url, Dictionary<string, string> param)
        {
            var reult = string.Empty;
            try
            {
                var strParam = param.Aggregate(string.Empty, (current, pair) => current + (pair.Key + "=" + pair.Value));

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";                

                var post = Encoding.UTF8.GetBytes(strParam);

                request.ContentLength = post.Length;
                var myStream = request.GetRequestStream();
                myStream.Write(post, 0, post.Length);
                myStream.Flush();
                myStream.Close();
                myStream.Dispose();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        reult = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reult;
        }

        /// <summary>
        /// 不带参数的请求。
        /// </summary>
        /// <param name="url"></param>
        public static void SendHttpRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";

            var post = Encoding.UTF8.GetBytes("orderid=1");

            request.ContentLength = post.Length;
            var myStream = request.GetRequestStream();
            myStream.Write(post, 0, post.Length);
            myStream.Flush();
            myStream.Close();
            myStream.Dispose();
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    reader.ReadToEnd();
                    reader.Close();
                }
            }
        }
    }
}
