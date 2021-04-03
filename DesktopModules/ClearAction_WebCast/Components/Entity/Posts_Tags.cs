using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClearAction.Modules.WebCast.Components
{
    [TableName("WCC_Posts_Tags")]
     [PrimaryKey("CategoryId")]
    public class Posts_Tags
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}