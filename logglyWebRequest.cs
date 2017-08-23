using LogglyWebRequest.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LogglyWebRequest
{
    public class logglyWebRequest
    {
        private const string logglyServer = "http://logs-01.loggly.com";
        private const string token = "YourToken";
        public clsResult Log2Server(clsLogglyModel model, string tag = "")
        {
            string targetURL = string.Format("{0}/inputs/{1}/", logglyServer, token);
            if (tag != "") targetURL = string.Format("{0}tag/{1}", targetURL, tag);
            return postRequest(targetURL, model);
        }
        private clsResult postRequest(string url, clsLogglyModel model)
        {
            try
            {
                string jsonData = model.toLogglyJsonString();

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
                httpWebRequest.ContentLength = byteArray.Length;
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                return TransferHttpResponseToResult(httpResponse);
            }
            catch (Exception ex)
            {
                return new clsResult() { Success = false, ErrorMessage = ex.Message };
            }
        }
        private clsResult TransferHttpResponseToResult(HttpWebResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return new clsResult() { Success = true, ErrorMessage = "" };
            else
            {
                string responseText = "";
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    responseText = reader.ReadToEnd();
                }
                return new clsResult() { Success = false, ErrorMessage = responseText };
            }
        }
    }
   
}
