using ClearAction.Modules.ProfileClearAction_ProfileSetup.Linkedin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Cryptography;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
    public class WebController
    {

        public static string GetAuthorization(string LinkedinKey, string returnurl,string Scope)
        {
            return string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}&scope={3}&state=STATE", Credentials.AuthorizationUrl, LinkedinKey, returnurl, Scope);
        }

        public LinkEdin GetAuthorizeCode(string Code, string returnurl, string LinkedinKey, string LinkedSecret, OAuthContext oContext)
        {
            string PostData = string.Format("grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}", Code, returnurl, LinkedinKey, LinkedSecret);


            string strTokenJson = PostMessage(PostData, Credentials.RequestAccessTokenUrl, true);

            if (string.IsNullOrEmpty(strTokenJson))
                return null;
            return GetProfileResponse(strTokenJson);
        }
        private LinkEdin GetProfileResponse(string Token)
        {

            string linkeidnProfile = GetAPI_ProfileData(string.Format("oauth2_access_token={0}&format=json", HelperClass.GetToken(Token).access_token),Credentials.RequestProfileUrl);
           
            if (string.IsNullOrEmpty(linkeidnProfile)) return null;

            return HelperClass.GetParseLinkdin(linkeidnProfile);
        }
  
        private string GetAPI_ProfileData(string body, string url)
        {
            
            try
            {

                string strPostUrl = url + "?" + body;
            
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strPostUrl);//
                request.Method = "Get";
                request.KeepAlive = true;
                request.ContentType = "appication/json";
              //  request.Headers.Add("Content-Type", "appication/json");
                //request.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }
                return myResponse;

            }

            catch (Exception ex)
            {

            }
            return "";


        }

        private string PostMessage(string body, string url, bool IsPost)
        {
            string strOut = string.Empty;
            try
            {
                string formatter = "?";
                HttpWebRequest webRequest = WebRequest.Create(url + (IsPost ? formatter : "&") + body) as HttpWebRequest;
                if (IsPost)
                {
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                }
                else
                {

                    webRequest.Headers.Add("Authorization", body);
                    webRequest.Method = "POST";
                    webRequest.Host = "www.linkedin.com";
                    webRequest.ProtocolVersion = HttpVersion.Version11;
                }
                Stream dataStream = webRequest.GetRequestStream();

                String postData = String.Empty;
                byte[] postArray = Encoding.ASCII.GetBytes(postData);

                dataStream.Write(postArray, 0, postArray.Length);
                dataStream.Close();

                WebResponse response = webRequest.GetResponse();
                dataStream = response.GetResponseStream();


                StreamReader responseReader = new StreamReader(dataStream);
                String returnVal = responseReader.ReadToEnd().ToString();
                responseReader.Close();
                dataStream.Close();
                response.Close();
                return returnVal;

            }

            catch (Exception ex)
            {

            }
            return strOut;

        }




    }
}