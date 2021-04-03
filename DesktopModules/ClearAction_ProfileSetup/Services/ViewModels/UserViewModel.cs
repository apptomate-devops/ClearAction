using DotNetNuke.Entities.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Services.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserViewModel : DotNetNuke.Entities.Users.UserInfo
    {
        public UserViewModel(UserInfo t)
        {
            if(t==null)
            {
                Id = -1;
                Name = "Unknown";

            }
            Id = t.UserID;
            Name = t.DisplayName;
        }

        public UserViewModel() { }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}