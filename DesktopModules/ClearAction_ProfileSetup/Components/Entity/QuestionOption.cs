using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    [TableName("ClearChoice_QuestionOption")]
    //setup the primary key for table
    [PrimaryKey("QuestionOptionID", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("QuestionOptions", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class QuestionOption
    {

        public int QuestionOptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int RelatedEntityID { get; set; }

    }
}