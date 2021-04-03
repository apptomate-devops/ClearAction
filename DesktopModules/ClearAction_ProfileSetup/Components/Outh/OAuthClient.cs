using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public class OAuthLinkedInClient
    {
        public string BeginAuthentication(string returnUrl, string _ConsumerKey, string _ConsumerSecret)
        {
            var oContext = new OAuthContext(returnUrl, _ConsumerKey, _ConsumerSecret)
            {
                ConsumerKey = _ConsumerKey,
                ConsumerSecret = _ConsumerSecret,
                RequestTokenUrl = Credentials.RequestAccessTokenUrl,
                VerifierUrl = Credentials.VerifierUrl,
                RequestAccessTokenUrl = Credentials.RequestAccessTokenUrl,
                RequestProfileUrl = Credentials.RequestProfileUrl,
                Realm = returnUrl,
                Scope = "r_basicprofile r_emailaddress",
                OAuthVersion = OAuthVersion.V2,
                SocialSiteName = "LinkedIn"
            };


            oContext.GetRequestToken();
            HttpContext.Current.Session["OContext"] = oContext;
            return oContext.ObtainVerifier(true);
            ///return "";
        }
    }
}