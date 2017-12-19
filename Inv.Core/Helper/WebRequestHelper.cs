using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Core.Helper
{
    public class WebRequestHelper
    {

        public static string RequestUrlGet(string url, string jsonBody)
        {
            return ReqestUrl(url, jsonBody, "GET");
        }
        public static string RequestUrlPost(string url, string jsonBody)
        {
            return ReqestUrl(url, jsonBody, "POST");
			
			
        }

        private static string ReqestUrl(string url, string jsonBody, string method)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Headers.Add("Content-Type", "application/json");
            //httpWebRequest.Headers.Add("currency", "sgd");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonBody);
                streamWriter.Flush();
                streamWriter.Close();
            }

            string result = string.Empty;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if(httpResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
  
            httpResponse.Close();

            return result;
        }
    }
}
