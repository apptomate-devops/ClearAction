using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction_Chat.Models
{
    public class DNNUserList
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserPhotoURL { get; set; }
        public Boolean IsUserOnline { get; set; }

    }
}