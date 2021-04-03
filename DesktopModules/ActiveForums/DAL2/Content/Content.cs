using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    [TableName("activeforums_Content")]
    [PrimaryKey("ContentId")]
    public class Content
    {
        public int ContentId { get; set; }
        public string Subject { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool IsDeleted { get; set; }
        public string IPAddress { get; set; }
        public int ContentItemId { get; set; }
        public int EditedAuthorId { get; set; }
        public string EditedAuthorName { get; set; }

        [ReadOnlyColumn]
        public UserProfileInfo EditedAuthorProfiler
        {
            get
            {
                if (EditedAuthorId > 0)
                    return new UserController().Profiles_Get(-1, ContentId, EditedAuthorId);
                return null;
            }

        }

        [ReadOnlyColumn]
        public UserProfileInfo AuthorProfiler
        {
            get
            {
                if (AuthorId > 0)
                    return new UserController().Profiles_Get(-1, ContentId, AuthorId);
                return null;
            }

        }


        [ReadOnlyColumn]
        public string AuthorSignature
        {
            get
            {
                var o = AuthorProfiler;
                if (o != null)
                {
                    if (o.Signature != null)
                        return o.Signature;
                    else
                        return "";
                }
                return "";
            }
        }

        [ReadOnlyColumn]
        public string AuthorSignatureWithLink
        {
            get
            {
                try
                {
                    var o = AuthorProfiler;
                    if (o != null)
                        return string.Format("<a href='/MyProfile/UserID/{0}' title='{1}'  class='tip'  data-tip='{1}' >{1}</a>", o.UserID, AuthorName);
                    return "";
                }
                catch (Exception ex) { return ""; } 
            }
        }

        [ReadOnlyColumn]
        public string AuthorSubTitle
        {
            get
            {
                try
                {
                    string strSubTitle = AuthorProfiler.UserCaption;
                    if (string.IsNullOrEmpty(strSubTitle))
                        return AuthorSignature;

                    return string.Format("{0}, {1}", AuthorSignature, strSubTitle);
                }
                catch(Exception ex)
                {
                    return "";
                }
            }
        }

        [ReadOnlyColumn]
        public string EditedAuthorLink
        {
            get
            {
                var o = AuthorProfiler;
                if (o != null)
                    return string.Format("<a href='/MyProfile/UserID/{0}'  title='{1}'>{1}</a>", EditedAuthorId, EditedAuthorName);
                return "";
            }
        }


        [ReadOnlyColumn]
        public string AuthorPicture
        {
            get
            {
                var o = AuthorProfiler;
                if (o != null)
                    return o.Avatar;
                return "";
            }
        }

        [ReadOnlyColumn]
        public string EditedAuthorPicture
        {
            get
            {
                var o = EditedAuthorProfiler;
                if (o != null)
                    return o.Avatar;
                return "";
            }
        }

        [ReadOnlyColumn]
        public string ContentLikes
        {
            get
            {
                var olist = new LikesController().GetForPost(ContentId);
                if (olist != null)
                    return olist.Count.ToString();
                return "0";
            }
        }



        [ReadOnlyColumn]
        public string ContentDisLikes
        {
            get
            {
                var olist = new LikesController().ContentDisLikes(ContentId);
                if (olist != null)
                    return olist.Count.ToString();
                return "0";
            }
        }

        [ReadOnlyColumn]

        public string EditDisplayStyle
        {
            get
            {
                if (DateCreated == DateUpdated)
                    return "";
                return string.Format("&nbsp;&nbsp;::&nbsp;&nbsp;Edited by {0} &nbsp;{1}", EditedAuthorName, DateUpdated.ToString("MMM dd-yyyy hh:mm tt"));
            }
        }
    }
}
