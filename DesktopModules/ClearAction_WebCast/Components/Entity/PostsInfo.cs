using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction.Modules.WebCast.Components
{

    [TableName("WCC_Posts")]
    [PrimaryKey("WCCId")]
    public class WCC_PostInfo
    {

        public int WCCId { get; set; }

        public string Title { get; set; }
        public int ComponentID { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string filestackurl { get; set; }

        public int PresenterID { get; set; }

        public string PresenterName { get; set; }

        public string PresenterTitle { get; set; }

        public string PresenterCompany { get; set; }

        public string PresenterProfilePic { get; set; }

        public int ViewCount { get; set; }


        public string RegistrationLink { get; set; }
        public string ExpiredviewLink { get; set; }

        public int CreatedByUserID { get; set; }

        public int UpdatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public DateTime UpdatedOnDate { get; set; }

        public bool IsPublish { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventTime { get; set; }

        public string WebApiKey { get; set; }

        [ReadOnlyColumn]
        public string PresenterSubTitle
        {
            get
            {
                string strName = PresenterName;
                //Changes done by @SP
                //string strTemp = PresenterTitle;
                string strTemp = Helper.GetEnumDescription((EventTypeEnum)ComponentID);

                if (!string.IsNullOrEmpty(strTemp))
                    strName = string.Format("{0}, {1}", strName, strTemp);
                strTemp = PresenterCompany;
                if (!string.IsNullOrEmpty(strTemp))
                    strName = string.Format("{0}, {1}", strName, strTemp);
                return strName;
            }

        }


        [ReadOnlyColumn]
        private List<PostCategoryRelationInfo> iCategory;
        [ReadOnlyColumn]
        public List<PostCategoryRelationInfo> GetCategory
        {
            get
            {

                if (WCCId > 0)
                    iCategory = new Components.DBController().GetTopicCategoryRelationOnlyActive(WCCId, true);
                return iCategory;

            }
        }


        [ReadOnlyColumn]
        public string tags
        {
            get
            {
                return new Components.DBController().GetTagNameByPostID(WCCId);

            }
        }

        [ReadOnlyColumn]
        private int m_IsSelfAssigned;
        [ReadOnlyColumn]
        public int IsSelfAssigned
        {
            get
            {
                return m_IsSelfAssigned;
            }
            set
            {
                m_IsSelfAssigned = value;
            }
        }




        [ReadOnlyColumn]
        public string Status
        {
            get; set;
        }


        [ReadOnlyColumn]
        public bool ISVISIBLEREGiSTER
        {
            get
            {
                if (EventDate.Ticks > System.DateTime.Now.Ticks)
                    return true;
                return false;
            }
        }
        [ReadOnlyColumn]
        public string EventDateTimeCST { get; set; }


        [ReadOnlyColumn]
        public int LikesCount
        {
            get
            {
                if (WCCId == -1)
                {
                    return 0;
                }

                return Convert.ToInt32(new DBController().GetForPostCount(WCCId, "Post"));
            }
        }


        [ReadOnlyColumn]
        public int CommentCount
        {
            get
            {
                if (WCCId != -1)
                {
                    var odata = new DBController().GetComments(WCCId);
                    return odata == null ? 0 : odata.Count;
                }

                return 0;//DAL2.BlogContent.BlogContentController().GetCommentsbyContentID_Count(ContentItemId);
            }
        }
        [ReadOnlyColumn]
        public int UnLikesCount
        {
            get
            {
                if (WCCId == -1)
                {
                    return 0;
                }

                return new DBController().GetUnlikeForPost(WCCId, "Post").Count;
            }
        }
    }
}