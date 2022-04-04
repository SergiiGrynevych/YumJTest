using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YumJTest.Controllers
{
    public static class AdditionalProcessing
    {
        public static HttpWebResponse RequestResponce(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            return (HttpWebResponse)httpRequest.GetResponse();
        }
        public static void StreamRead(HttpWebResponse httpResponse, out string result)
        {
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                result = streamReader.ReadToEnd();
        }
        public static void StreamWrite(HttpWebRequest httpRequest, string data)
        {
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                streamWriter.Write(data);
        }
    }
}
