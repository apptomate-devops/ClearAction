using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content.Taxonomy;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;
using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    [TableName("activeforums_Tags")]
    [PrimaryKey("TagId")]
    public class Tags //: Term
    {
        public int TagId { get; set; }
        public int Portalid { get; set; }
        public int ModuleId { get; set; }
        public string TagName { get; set; }

        public int Clicks { get; set; }
        public int Items { get; set; }
        public int Priority { get; set; }
        public bool IsCategory { get; set; }
        public int ForumId { get; set; }
        public int ForumGroupId { get; set; }

        public int TopicId { get; set; }


        [ReadOnlyColumn]
        public string GetTagUrl
        {
            get
            {
                if (string.IsNullOrEmpty(TagName)) return "";
                return Helper.TagUrl(TagName);
            }
        }
    }

}
