using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.WebCast.Components.Entity
{
    public class Webinar_User_RegistrationDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? WCCId { get; set; }
        public int? ComponentId { get; set; }
        public string WebinarKey { get; set; }
        public string UserWebinarRegistrationKey { get; set; }
        public string UserWebinarJoinURL { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool? IsAttendWebinar { get; set; }
    }
}