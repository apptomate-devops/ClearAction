using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNuke.Modules.ActiveForums.DAL2;

namespace DotNetNuke.Modules.ActiveForums
{
    [TableName("activeforums_Topics")]
    [PrimaryKey("TopicId")]
    public class Topics
    {


        public int TopicId { get; set; }

        public int ContentId { get; set; }

        public int ViewCount { get; set; }

        public int ReplyCount { get; set; }


        public bool IsLocked { get; set; }

        public int IsPinned { get; set; }

        public string TopicIcon { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public bool IsDeleted { get; set; }


        public bool IsAnnounce { get; set; }

        public bool IsArchived { get; set; }

        public DateTime AnnounceStart { get; set; }
        public DateTime AnnounceEnd { get; set; }

        public int TopicType { get; set; }
        public int Priority { get; set; }

        public string URL { get; set; }

        public string TopicData { get; set; }

        public int NextTopic { get; set; }

        public int PrevTopic { get; set; }

        public int CategoryId { get; set; }

        [ReadOnlyColumn]
        public DotNetNuke.Modules.ActiveForums.DAL2.Content GetContent
        {
            get
            {
                if (ContentId > 0)
                    return new DotNetNuke.Modules.ActiveForums.DAL2.ContentController().Get(ContentId);
                return null;
            }
        }


        private List<TopicCategoryRelationInfo> oRelation;

        private List<TopicCategoryRelationInfo> oAudienceGlass;

        [ReadOnlyColumn]
        public List<TopicCategoryRelationInfo> GetTopicCategoryRelation

        {
            get
            {
                if (TopicId > 0)
                {
                    oRelation = new Dal2Controller().GetTopicCategoryRelation(TopicId, true, true).ToList();
                }
                return oRelation;

            }
        }


        [ReadOnlyColumn]
        public List<TopicCategoryRelationInfo> GetTopicAudienceclass

        {
            get
            {
                if (TopicId > 0)
                {
                    oRelation = new Dal2Controller().GetTopicCategoryRelation(TopicId, true, false).ToList();
                }
                return oAudienceGlass;

            }
        }

        [ReadOnlyColumn]
        public string TopicTags
        {
            get
            {

                var oTags = new DAL2.Dal2Controller().GetTags(TopicId);
                if (oTags != null)
                {
                    System.Text.StringBuilder strTags = new StringBuilder();
                    foreach (Tags strTagLink in oTags)
                    {
                        strTags.Append(strTagLink.GetTagUrl + "&nbsp;");
                    }
                    return strTags.ToString();
                }
                return "";
            }
        }

        [ReadOnlyColumn]
        public string GetTopicCategoriesName
        {
            get
            {
                string cat = "";
                var oData = GetTopicCategoryRelation;
                if (oData != null) {
                    int iCount=0;
                    foreach (TopicCategoryRelationInfo c in oData.Where(x => x.IsActive))
                    {
                        if (iCount > 3) break; // only three categgories need to be shown
                        cat = cat + ", " + c.Categoryname;
                        iCount++;
                    }
                }
                if (cat.Length > 0)
                    return "Category :: "+cat.Substring(1, cat.Length - 1);
                return cat;

            }
        }



    }

    /// <summary>
    /// Added by Sachin for custom requirement to show the Status of Activity with forum for Done/TO-Do etc
    /// </summary>
    public class CA_TopicsEx : Topics
    {
        public string Status { get; set; }
        public bool IsAssigned { get; set; }
        public bool HasSeen { get; set; }
        public int AuthorId { get; set; }
        public int IsSelfAssigned { get; set; }
    }

}
