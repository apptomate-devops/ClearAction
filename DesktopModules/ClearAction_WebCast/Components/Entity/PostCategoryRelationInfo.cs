using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction.Modules.WebCast.Components
{

    [TableName("WCC_PostCategoryRelation")]
    [PrimaryKey("WPostCategoryId")]
    public class PostCategoryRelationInfo
    {

        public int WPostCategoryId { get; set; }


        public int CategoryId { get; set; }

        public int WCCId { get; set; }

        public bool IsActive { get; set; }


    }
}