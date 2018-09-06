using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace PMapCore.Common.Parse
{
    public static class ParseLogX
    {
        private static string APP_ID = "38ZCHZtDbcGtPjgbSsgl4rD16GL3RbSaqrJjBDya";
        private static string REST_API_KEY = "42jLGdwUF6sP62sEmEGCvR5jPeD2v1zD7mpiNX3R";
        private static string URL = "https://api.parse.com/1/classes/PMapLog";
        private static string URLC = "https://api.parse.com/1/classes/PMapCalls";

        public static void LogToParse(string p_type, DateTime p_timestamp, string p_text)
        {
            //kikötve
            return;
           
          
            PMapLog pl = new PMapLog()
                                {
                                    AppInstance = PMapCommonVars.Instance.AppInstance,
                                    Type = p_type,
                                    PMapTimestamp = p_timestamp.ToString(Global.DATETIMEFORMAT),
                                    Text = p_text,
                                    Value = ""
                                };
            sendToParse(URL, pl);
        }

        public static void CallsToParse(string p_function, string p_result, TimeSpan p_duration, params  object[] p_values)
        {
            //kikötjük...
            //
            return;
            PMapCalls pc = new PMapCalls()
            {
                PMC_LICENCE = "***",
                PMC_IP = PMapCommonVars.Instance.AppInstance,
                PMC_VERSION = String.Format("Product:{0} Ver.:{1}", ApplicationInfo.Title, ApplicationInfo.Version),
                PMC_FUNCTION = p_function,
                PMC_TIMESTAMP = DateTime.Now.ToString(Global.DATETIMEFORMAT),
                PMC_RESULT = p_result, 
                PMC_DURATION = p_duration.ToString(),
                PMC_VALUE1 = p_values.Length > 0 ? p_values[0].ToString() : "",
                PMC_VALUE2 = p_values.Length > 1 ? p_values[1].ToString() : "",
                PMC_VALUE3 = p_values.Length > 2 ? p_values[2].ToString() : "",
                PMC_VALUE4 = p_values.Length > 3 ? p_values[3].ToString() : "",
                PMC_VALUE5 = p_values.Length > 4 ? p_values[4].ToString() : "",
                PMC_VALUE6 = p_values.Length > 5 ? p_values[5].ToString() : "",
                PMC_VALUE7 = p_values.Length > 6 ? p_values[6].ToString() : "",
                PMC_VALUE8 = p_values.Length > 7 ? p_values[7].ToString() : "",
                PMC_VALUE9 = p_values.Length > 8 ? p_values[8].ToString() : "",
                PMC_VALUE10 = p_values.Length > 9 ? p_values[9].ToString() : ""
            };
            sendToParse(URLC, pc);
        }

        private static void sendToParse(string p_url, object p_obj)
        {
            //kikötve...
            return;

            //*****************
            //*      írás     *
            //*****************

            try
            {
                WebHeaderCollection parseHeaders = new WebHeaderCollection();
                parseHeaders.Add("X-Parse-Application-Id", APP_ID);
                parseHeaders.Add("X-Parse-REST-API-Key", REST_API_KEY);


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(p_url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers = parseHeaders;

                String postString = JSONHelper.Serialize<object>(p_obj);
                byte[] postDataArray = Encoding.Default.GetBytes(postString);

                httpWebRequest.ContentLength = postDataArray.Length;
                httpWebRequest.Timeout = 100000;
                httpWebRequest.ContentType = "application/json";

                Stream writeStream = httpWebRequest.GetRequestStream();
                writeStream.Write(postDataArray, 0, postDataArray.Length);
                writeStream.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
            catch (Exception ex)
            {
                Util.ExceptionLog(ex);
            }
        }
    }
}
