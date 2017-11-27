using Newtonsoft.Json;
using PMap.Common;
using PMap.Localize;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMap.WebTrace
{
    public static class NotificationMail
    {

        public static PMToken GetToken (List<PMTracedTour> p_tracedTour)
        {
            /*
            < add key = "AuthTokenCryptAESKey" value = "VhHe1F6DExaWl1T0bcOxdok58CyIXnjwCDQmojbwpH4=" />
            < add key = "AuthTokenCryptAESIV" value = "GFXXSSi7IQFN0bgbwuuVng==" />
            */
            //   var AuthTokenCryptAESKey = "VhHe1F6DExaWl1T0bcOxdok58CyIXnjwCDQmojbwpH4=";
            //  var AuthTokenCryptAESIV = "GFXXSSi7IQFN0bgbwuuVng==";
            PMToken ret = new PMToken();

            JsonSerializerSettings jsonsettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var jsonRegno = JsonConvert.SerializeObject(p_tracedTour, jsonsettings);

            var crypted = AESCryptoHelper.EncryptString(jsonRegno, PMapIniParams.Instance.AuthTokenCryptAESKey, PMapIniParams.Instance.AuthTokenCryptAESIV);
            var ori = AESCryptoHelper.DecryptString(crypted, PMapIniParams.Instance.AuthTokenCryptAESKey, PMapIniParams.Instance.AuthTokenCryptAESIV);

            byte[] bytes = Encoding.UTF8.GetBytes(crypted);
            string base64 = Convert.ToBase64String(bytes);

            Console.WriteLine(base64);


            string baseurl = @"http://mplastwebtest.azurewebsites.net/Auth/GenerateTempUserToken";
            var builder = new UriBuilder(baseurl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["tokenContent"] = crypted;
            builder.Query = query.ToString();
            string url = builder.ToString();

            HttpClient client = new HttpClient();
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(res))
                {
                    ret = JsonConvert.DeserializeObject<PMToken>(res);
                }

            }

     

            return ret;
            //http://mplastwebtest.azurewebsites.net/Auth/TokenLoginRedirect?token=P6w/g1SU1wb/F6cJBwYDF9Ct/9Zw0hGbBosLMnTAq0ZYImQBKW7QsRJ5brMqiYBr

        }
        public static async void SendNotificationMail( string p_emailAddr, PMToken p_token)
        {
           // var apiKey = "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";
            var apiKey = PMapCommonVars.Instance.AzureSendGridApiKey;
            
            var client = new SendGridClient(apiKey);
            //érvénytelen karakterek kiszedése
            p_emailAddr = p_emailAddr.Replace(" ", "");
            p_emailAddr = p_emailAddr.Replace("\"", "");
            p_emailAddr = p_emailAddr.Replace("'", "");
            p_emailAddr = p_emailAddr.Replace(",", ";");

            string[] addr = p_emailAddr.Split(';');

            foreach (var toEmail in addr)
            {
                var from = new EmailAddress(PMapIniParams.Instance.WebLoginSenderEmail, "");
                var subject = "Web követés belépés";
                var to = new EmailAddress(toEmail, "");
                var plainTextContent = "";
                var htmlContent = Util.FileToString(PMapIniParams.Instance.WebLoginTemplate);
                htmlContent = htmlContent.Replace("@@token", HttpUtility.UrlEncode( p_token.temporaryUserToken));
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                // var response = client.SendEmailAsync(msg);
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Accepted)
                    throw new Exception(String.Format(PMapMessages.E_SNDEMAIL_FAILED, p_emailAddr));
            }
        }

    }
}
