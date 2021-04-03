using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    [TableName("activeforums_TopicCategoryRelation")]
    [PrimaryKey("TopicCategoryId")]

    public class TopicCategoryRelationInfo
    {
        public int TopicCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }
        public bool IsActive { get; set; }

        public bool IsGlobalCategory { get; set; }


        private GlobalCategory oGlobalCategory;

        [ReadOnlyColumn]
        public GlobalCategory GetCategoryInfo

        {
            get
            {
                if (CategoryId > 0)
                {
                    oGlobalCategory = new Dal2Controller().GetGlobalCategoryID(CategoryId);
                }
                return oGlobalCategory;

            }
        }
      


        [ReadOnlyColumn]
        public string Categoryname

        {
            get
            {
                if (GetCategoryInfo != null)
                {
                    return GetCategoryInfo.CategoryName;
                }
                return "";

            }
        }


        [ReadOnlyColumn]
        public string ActiveCss
        {
            get
            {
                if (IsActive)
                {
                    return "Active";
                }
                return "";

            }
        }
    }
}
