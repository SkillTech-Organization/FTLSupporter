using Newtonsoft.Json;
using PMapCore.BO;
using PMapCore.Common;
using PMapCore.Strings;
using PMapCore.WebTrace;
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

namespace PMapUI.Common
{
    public static class NotificationMail
    {

        public static PMToken GetToken(List<PMTracedTour> p_tracedTour)
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
        public static async void SendNotificationMail(string p_emailAddr, PMToken p_token, string p_msgOk = "")
        {

            // var apiKey = "SG.R0amttRbT4qIzgqnuWJaSg.itt csak egz pont van.Fc17tbkdeD5c9xsASTDyDZ-z2yaXcnSj0JhQqFp7-yI";
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
                if (Util.IsValidEmail(toEmail))
                {
                    var from = new EmailAddress(PMapIniParams.Instance.WebLoginSenderEmail, PMapIniParams.Instance.WebLoginSenderName);
                    var subject = PMapIniParams.Instance.WebLoginSubject;
                    var to = new EmailAddress(toEmail, "");
                    var plainTextContent = "";
                    var htmlContent = Util.FileToString(PMapIniParams.Instance.WebLoginTemplate);
                    htmlContent = htmlContent.Replace("@@token", HttpUtility.UrlEncode(p_token.temporaryUserToken));
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    // var response = client.SendEmailAsync(msg);
                    var response = await client.SendEmailAsync(msg);
                    if (response.StatusCode == HttpStatusCode.Accepted)
                    {
                        if (!String.IsNullOrEmpty(p_msgOk))
                            UI.Message(p_msgOk, toEmail);
                    }
                    else
                        UI.Message(String.Format(PMapMessages.E_SNDEMAIL_FAILED, p_emailAddr));
                }
                else
                {
                    UI.Message(String.Format(PMapMessages.E_SNDEMAIL_FAILED, p_emailAddr));
                }
            }
        }


        public static async void SendNotificationMailDrv(string p_emailAddr, PMToken p_token, boPlanTour p_boPlanTour, string p_msgOk = "")
        {
            // var apiKey = "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";
            var apiKey = PMapCommonVars.Instance.AzureSendGridApiKey;
            var client = new SendGridClient(apiKey);


            if (Util.IsValidEmail(p_emailAddr))
            {
                var from = new EmailAddress(PMapIniParams.Instance.WebLoginSenderEmail, "");
                var subject = "Web túrateljesítés belépés";
                var to = new EmailAddress(p_emailAddr, "");
                var plainTextContent = "";
                var htmlContent = Util.FileToString(PMapIniParams.Instance.WebDriverTemplate);
                htmlContent = htmlContent.Replace("@@token", HttpUtility.UrlEncode(p_token.temporaryUserToken));
                htmlContent = Util.ReplaceTokensInContent(htmlContent, p_boPlanTour);

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                // var response = client.SendEmailAsync(msg);
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    if (!String.IsNullOrEmpty(p_msgOk))
                        UI.Message(p_msgOk, p_emailAddr);
                }
                else
                    UI.Message(String.Format(PMapMessages.E_SNDEMAIL_FAILED, p_emailAddr));
            }
            else
            {
                UI.Message(String.Format(PMapMessages.E_SNDEMAIL_FAILED, p_emailAddr));
            }
        }
    }
}
