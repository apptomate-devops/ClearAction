using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.WebCast.Components
{
    public enum EventTypeEnum
    {
        [Description("Community Call")]
        Community_Calls = 4,

        [Description("Webcast Conversation")]
        Webcast_Conversations = 5,

        [Description("Roundtable")]
        Roundtable = 6,

        [Description("Virtual Conference")]
        Virtual_Conference = 7,

        [Description("Fireside Chat")]
        Fireside_Chat = 8,

        [Description("Expert Presentation")]
        Expert_Presentation = 9

    }

}