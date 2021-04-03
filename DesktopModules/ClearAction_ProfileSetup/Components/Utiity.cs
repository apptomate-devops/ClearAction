using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public static class Utiity
    {

        //If this key updated dashboard/connections and all respective module may need update.
        public static string LinkedinAPIKey = "LinkedinAPIKey";
        public static string LinkedinToken = "LinkedinToken";
        public static string LinkedinScope = "LinkedinScope";

        public static string Linkedin = "Linkedin";
        public static string Facebook = "Facebook";
        public static string Twitter = "Twitter";
        public static string Rank1 = "Rank1";
        public static string Rank2 = "Rank2";
        public static string FirstName = "FirstName";
        public static string LastName = "LastName";
        public static string Phone = "Phone";

        public static string Location = "Location";
        public static string Education = "Education";
        public static string HonorsReward = "HonorsReward";
        public static string Overview = "Overview";
        public static string ProfessionalAssociates = "ProfessionalAssociates";
        public static string Title = "Title";
        public static string WorkHistory = "WorkHistory";
        public static string CompanyName = "CompanyName";

        public static string Linkedin_SessionKey = "lnkProfileData";

        public enum ControlType
        {
            TextBox = 1,
            SocialMedia = 2,
            DropDown = 3,
            RadioButton = 4,
            MultiTextBox = 5,
            MultiSelect = 6,
            GlobalCategory = 7,
            GlobalRole=8,
            GlobalCompany=9
        }
        public enum Steps
        {
            Step1 = 1,//For genearic
            Step2 = 2,//Step2 with letter/and other
            Step3 = 3,//Step 3 with combination
            Step4 = 4,// Top selection on step 3
            Step5 = 5,//Multiselect option on step5
            Step6 = 6, // Bottom list on step5
            Step7 = 7 //Radio button ranks on step5
        }


        public static string Linkedin_AuthorizationUrl = "https://www.linkedin.com/uas/oauth2/authorization";
        public static string Linkedin_AuthorizationTokenUrl = "https://www.linkedin.com/uas/oauth2/accessToken";
        public static string Linkedin_AuthorizationProfile = " https://api.linkedin.com/v1/people/~:(firstName,lastName,picture-url)?oauth2_access_token=[TOKEN]&format=json";
        //public static string Linedin_Scope = "r_basicprofile r_emailaddress rw_company_admin w_share";
    }

}