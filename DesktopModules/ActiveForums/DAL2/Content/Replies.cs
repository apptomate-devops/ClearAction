using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    [TableName("activeforums_Replies")]
    [PrimaryKey("ReplyId")]
    public class Replies
    {
        public int ReplyId
        { get; set; }

        public int TopicId
        { get; set; }

        public int ReplyToId
        { get; set; }

        public int ContentId
        { get; set; }

        public bool IsApproved
        { get; set; }

        public bool IsRejected
        { get; set; }

        public int StatusId
        { get; set; }

        public bool IsDeleted
        { get; set; }

        [ReadOnlyColumn]
        public string Subject { get; set; }

        [ReadOnlyColumn]
        public string Summary { get; set; }

        [ReadOnlyColumn]
        public string Body { get; set; }

        [ReadOnlyColumn]
        public DateTime DateCreated { get; set; }

        [ReadOnlyColumn]
        public DateTime DateUpdated { get; set; }


        [ReadOnlyColumn]
        public int AuthorId { get; set; }

        [ReadOnlyColumn]
        public string AuthorName { get; set; }


        [ReadOnlyColumn]
        public int? EditedAuthorId { get; set; }

        [ReadOnlyColumn]
        public string EditedAuthorName { get; set; }


        [ReadOnlyColumn]
        public string ForatedCreateDate
        {
            get
            {
             //  return Utilities.HumanFriendlyDate(DateCreated, 1, 1);
                return Utilities.ToRelativeDateString(DateCreated, true);
            }

        }

        [ReadOnlyColumn]
        public Content GetContent
        {
            get
            {
                if (ContentId > 0)
                    return new DotNetNuke.Modules.ActiveForums.DAL2.ContentController().Get(ContentId);
                return null;
            }

        }


        [ReadOnlyColumn]
        public int ReplyCount
        {
            get
            {
                if (ReplyId > 0)
                    return new DAL2.Dal2Controller().GetDiscussionCount(ReplyId);
                return 0;
            }

        }
     
    }
   
}
