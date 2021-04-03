using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction.Modules.WebCast.Components
{
    [TableName("ClearAction_GlobalCategory")]
    [PrimaryKey("CategoryId")]
    public class GlobalCategory
    {

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public bool IsActive { get; set; }


        public int ComponentId { get; set; }


        public string DisplayName { get; set; }
        public int OptionOrder { get; set; }
    }
}
