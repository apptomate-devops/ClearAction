using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClearAction.Modules.WebCast.Components
{
    [TableName("WCC_Comments")]
    [PrimaryKey("CommentID")]
    public class Comments
    {
        public int CommentID { get; set; }
        public int WCCId { get; set; }
        public int ParentId { get; set; }
        public string Comment { get; set; }
        public bool Approved { get; set; }
        public string Author { get; set; }

        public string Email { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedOnDate { get; set; }

        [ReadOnlyColumn]
        public int Likes { get; set; }


        [ReadOnlyColumn]
        public int Dislikes { get; set; }
        [ReadOnlyColumn]
        public int Reports { get; set; }


        [ReadOnlyColumn]
        public int Liked { get; set; }

        [ReadOnlyColumn]
        public int Disliked { get; set; }

        [ReadOnlyColumn]
        public int Reported { get; set; }


        [ReadOnlyColumn]
        public string AuthorLinkUrl
        {
            get
            {
                DotNetNuke.Entities.Users.UserInfo oData = AuthorDetails;
                if (oData != null)
                {
                    return string.Format("<a href='/MyProfile/Username/{0}' title='click to view {1} profile'>{1}</a>", oData.Username, oData.DisplayName);
                }

                return "";
            }
        }
        [ReadOnlyColumn]
        public DotNetNuke.Entities.Users.UserInfo AuthorDetails
        {
            get
            {
                if (CreatedByUserID > 0)
                {
                    return DotNetNuke.Entities.Users.UserController.GetUserById(0, CreatedByUserID);
                    //  return DotNetNuke.Entities.Users.UserInfo;


                }

                return null;
            }
        }

        [ReadOnlyColumn]
        public string GetCreatedDateHumanFriendly
        {
            get
            {
                if (CreatedOnDate != null)
                {
                    return Util.HumanFriendlyDate(CreatedOnDate);
                }

                return "";
            }
        }


        [ReadOnlyColumn]
        public List<Likes> GetLikes
        {
            get
            {
                List<Likes> ilst = new Components.DBController().GetForPost(CommentID, "Comment");
                return ilst;
            }
        }

        [ReadOnlyColumn]
        public int GetLikesCount
        {
            get
            {
                if (GetLikes != null)
                {
                    return GetLikes.Count;
                }

                return 0;
            }
        }

        [ReadOnlyColumn]

        public List<AttachmentInfo> Attachments
        {
            get
            {
                return new Components.DBController().GetAttachmentByComment(CommentID);
            }
        }

        [ReadOnlyColumn]
        public int TotalAttachment
        {
            get
            {
                var oAttach = Attachments;
                return oAttach == null ? 0 : oAttach.Count;
            }
        }

    }
}