using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;


namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    [TableName("ClearChoice_QuestionCategory")]
    //setup the primary key for table
    [PrimaryKey("CategoryID", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("CategoryID", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class QuestionCategory
    {
        public int CategoryID { get; set; }
        public int ParentID { get; set; }
        public string CategoryName { get; set; }
        public bool ISActive { get; set; }

        public int CategoryOrder { get; set; }
    }
}