using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ClearAction.Modules.Dashboard.Components
{
    [TableName("Blog_Posts")]
    //setup the primary key for table
    [PrimaryKey("ContentItemId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("ContentItemId", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class BlogPosts
    {

        public int ContentItemId { get; set; }
        public int BlogID { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }

        public string Image { get; set; }

        public bool Published { get; set; }
        public DateTime PublishedOnDate { get; set; }
        public bool AllowComments { get; set; }

        public bool DisplayCopyright { get; set; }
        public string Copyright { get; set; }
        public string Locale { get; set; }
        public int ViewCount { get; set; }

        public int CategoryID { get; set; }

        public int CreatedByUserID { get; set; }
        public int UpdatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }
        public DateTime UpdatedOnDate { get; set; }
        public string ShortDescription { get; set; }
    }

    public class CA_BlogPosts : BlogPosts
    {
        public bool HasSeen { get; set; }
        public bool IsAssigned { get; set; }
        public string Status { get; set; }
    }


    public class CA_DigitalEvents : WCC_PostInfo
    {
        public bool HasSeen { get; set; }
        public bool IsAssigned { get; set; }
        public string Status { get; set; }
    }



    [TableName("WCC_Posts")]
    [PrimaryKey("WCCId")]
    public class WCC_PostInfo
    {

        public int WCCId { get; set; }

        public string Title { get; set; }
        public int ComponentID { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string filestackurl { get; set; }

        public int PresenterID { get; set; }

        public string PresenterName { get; set; }

        public string PresenterTitle { get; set; }

        public string PresenterCompany { get; set; }

        public string PresenterProfilePic { get; set; }

        public int ViewCount { get; set; }


        public string RegistrationLink { get; set; }
        public string ExpiredviewLink { get; set; }

        public int CreatedByUserID { get; set; }

        public int UpdatedByUserID { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public DateTime UpdatedOnDate { get; set; }

        public bool IsPublish { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventTime { get; set; }

        public string WebApiKey { get; set; }
        public bool ISVISIBLEREGiSTER
        {
            get
            {
                if (EventDate <= System.DateTime.Now)
                    return true;
                return false;
            }
        }


    }
}