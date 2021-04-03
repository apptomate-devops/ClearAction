using DotNetNuke.ComponentModel.DataAnnotations;
using System;
namespace ClearAction.Modules.Connections
{

    [TableName("ClearAction_UserBlock")]
    [PrimaryKey("RequestID")]
    public class UserBlock
    {

        public int RequestID { get; set; }

        public int ToUserID { get; set; }

        public int FromUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }


    }


}
