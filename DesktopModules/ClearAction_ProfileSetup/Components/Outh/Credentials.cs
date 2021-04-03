
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    /// <summary>
    /// class holdes credentials and endpoints of different social sites
    /// </summary>
    public class Credentials
    {


        public static string ConsumerKey = "";
        public static string ConsumerSecret = "";
        public static string RequestTokenUrl = "https://api.linkedin.com/uas/oauth2/requestToken";
        public static string AuthorizationUrl = "https://www.linkedin.com/uas/oauth2/authorization";
        public static string VerifierUrl = "https://www.linkedin.com/uas/oauth2/authenticate";
        public static string RequestAccessTokenUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
        public static string RequestProfileUrl = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,email-address,headline,summary,phone-numbers,industry,skills,educations,Positions,public-profile-url,location,picture-url)";
       // public static string Scope = "r_basicprofile r_emailaddress";// rw_company_admin w_share";


    }


    
}