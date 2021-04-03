using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction.Modules.WebCast.Components
{
    [TableName("ClearAction_ComponentMaster")]
    [PrimaryKey("ComponentID")]
    public class ComponentMaster
    {

        public int ComponentID { get; set; }

        public string ComponentName { get; set; }

       
    }
}
