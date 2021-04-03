using System;

using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Users;

namespace ClearAction.Modules.Dashboard.Components
{

    [TableName("UserRelationships")]
    [PrimaryKey("UserRelationshipID")]

    public class UserRelationshipInfo
    {

        public int UserRelationshipID { get; set; }
        public int UserID { get; set; }
        public int RelatedUserID { get; set; }
        public int RelationshipID { get; set; }
        public string ProfilePic
        {
            get
            {
                string rValue = "";
                try
                {
                    rValue = UserController.GetUserById(0, this.RelatedUserID).Profile.PhotoURL;
                }
                catch(Exception ex)
                {
                    rValue = "";
                }
                return rValue;
            }
        }
        public int Status { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedOnDate { get; set; }
        [ReadOnlyColumn]
        private UserInfo RelatedUser
        {
            get
            {
                return UserController.GetUserById(0, this.RelatedUserID);
            }
        }
        public string RelatedFullName { get { return this.RelatedUser.DisplayName; } }
        public string RelatedEmail { get { return this.RelatedUser.Email; } }
    }


    public class DTOUser
    {
       public int UserID { get; set; }
    }

    public class DTOResponse
    {
        public int UserID { get; set; }
        public string ResponseText{ get; set; }
        public int QuestionID{ get; set; }
    }

}