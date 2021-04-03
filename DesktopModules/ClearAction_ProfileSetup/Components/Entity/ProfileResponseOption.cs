using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    [TableName("ClearChoice_ProfileResponseOption")]
    //setup the primary key for table
    [PrimaryKey("ProfileResponseOptionId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("ProfileResponseOptions", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class ProfileResponseOption
    {
        public int ProfileResponseOptionId { get; set; }
        public int QuestionOptionID { get; set; }
        public int QuestionID { get; set; }

        public int ProfileResponseID { get; set; }

        public bool IsActive { get; set; }
    }
}