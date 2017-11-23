using Newtonsoft.Json;
using PMap.Common;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            byte[] bytes = Encoding.Default.GetBytes(crypted);
            string base64 = Convert.ToBase64String(bytes);

            Console.WriteLine(base64);


            string url = @"http://mplastwebtest.azurewebsites.net/Auth/GenerateTempUserToken?tokencontent=" + base64;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string html = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
                ret = JsonConvert.DeserializeObject<PMToken>(html);
                
            }

            return ret;
            //http://mplastwebtest.azurewebsites.net/Auth/TokenLoginRedirect?token=P6w/g1SU1wb/F6cJBwYDF9Ct/9Zw0hGbBosLMnTAq0ZYImQBKW7QsRJ5brMqiYBr

        }
        public static async void SendNotificationMail( string p_emailAddr, PMToken p_token)
        {
            var apiKey = PMapCommonVars.Instance.AzureSendGridApiKey;

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("xxx.xx.com", "paraméterbe!");
            var subject = "Web követés belépés";
            var to = new EmailAddress(p_emailAddr, "");
            var plainTextContent = "";
            var htmlContent = Util.FileToString(PMapIniParams.Instance.WebLoginTemplate);
            htmlContent.Replace("@@token", p_token.temporaryUserToken);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            // var response = client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg);

        }

    }
}
