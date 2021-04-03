using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction_Chat.Models
{
    public class UserUnreadMessageCountModel
    {
        public long LoginUserId { get; set; }
        public long SelectedUserId { get; set; }
        public int UnreadMessageCount { get; set; }
    }
}