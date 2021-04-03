using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    [TableName("ClearChoice_ProfileResponse")]
    //setup the primary key for table
    [PrimaryKey("ProfileResponseId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("ProfileResponses", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class ProfileResponse
    {
        public int ProfileResponseId { get; set; }
        public int UserID { get; set; }
        public int QuestionId { get; set; }
        public int QuestionOptionId { get; set; }
        public string ResponseText { get; set; }
        public DateTime CreatedOnDate { get; set; }

        public DateTime UpdateOnDate { get; set; }

        [ReadOnlyColumn]
        public List<ProfileResponseOption> LstProfileResponseOption { get; set; }

       


    }
}