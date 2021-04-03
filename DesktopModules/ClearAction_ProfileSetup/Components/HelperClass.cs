using ClearAction.Modules.ProfileClearAction_ProfileSetup.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public class HelperClass
    {

        public static LinkEdin GetParseLinkdin(string JsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LinkEdin>(JsonString);
        }

        public static LinkedinAccess GetToken(string JsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedinAccess>(JsonString);
        }




    }
}