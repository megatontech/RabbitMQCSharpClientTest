using System;
using System.IO;
using System.Net;
using System.Text;

namespace TJAP.Common.Tools.Net
{
    public static class WebApiHelper
    {
        /// <summary>
        /// {
        ///   "url": "http://WWW.HGSOFT.COM",
        ///   "name": "HGSOFT",
        ///   "pwd": "HGSOFT",
        ///   "token": "e12adf50a03d9d16849c2466291ed1b5",
        ///   "project": "HGTEST01"
        /// }
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonParameter"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string PostJson(string url, string jsonParameter)
        {
            try
            {
                string jsonStr = "";
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "application/text";
                req.Method = "POST";
                byte[] btBodys = Encoding.UTF8.GetBytes(jsonParameter);
                req.ContentLength = btBodys.Length;
                req.GetRequestStream().Write(btBodys, 0, btBodys.Length);
                HttpWebResponse rep = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(rep.GetResponseStream(), Encoding.UTF8);
                jsonStr = reader.ReadToEnd().Trim();
                reader.Close();
                return jsonStr;
            }
            catch (Exception exception)
            {
            }
            return string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetJson(string url)
        {
            try
            {
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "application/xml";
                req.Method = "GET";
                HttpWebResponse rep = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(rep.GetResponseStream(), Encoding.UTF8);
                string str = reader.ReadToEnd().Trim();
                reader.Close();
                return str;
            }
            catch (Exception exception)
            {
            }
            return string.Empty;
        }
    }
}