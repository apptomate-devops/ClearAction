using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Users;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public class UProfile : UserInfo
    {

    //    public   string   FirstName { get; set; }
   //     public string LastName { get; set; }
 //       public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string LinkEdin { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string RoleName { get; set; }
        public string MemberSince { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string OverView { get; set; }
        public string Education { get; set; }
        public string WorkHistory { get; set; }
        public string ProfessionalAssociates { get; set; }
        public string HonorsAndAwards { get; set; }
        
    }
}