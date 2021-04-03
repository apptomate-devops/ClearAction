using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{

    [TableName("ClearChoice_Question")]
    //setup the primary key for table
    [PrimaryKey("QuestionID", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Questions", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("Module")]
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int Size { get; set; }
        public string Hint { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRequire { get; set; }
        public bool IsActive { get; set; }

        public int StepID { get; set; }
        public int QuestionTypeID { get; set; }

        [IgnoreColumn]
        public string RequireStyle
        {
            get
            {
                if (IsRequire)
                    return "1";
                return "0";
            }
        }
    }
}