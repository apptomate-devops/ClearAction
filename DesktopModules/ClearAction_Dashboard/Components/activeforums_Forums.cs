using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ClearAction.Modules.Dashboard.Components
{
    
    [TableName("activeforums_Topics")]
    [PrimaryKey("TopicId")]
    public class Topics
    {


        public int TopicId { get; set; }

        public int ContentId { get; set; }

        public int ViewCount { get; set; }

        public int ReplyCount { get; set; }


        public bool IsLocked { get; set; }

        public int IsPinned { get; set; }

        public string TopicIcon { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public bool IsDeleted { get; set; }


        public bool IsAnnounce { get; set; }

        public bool IsArchived { get; set; }

        public DateTime AnnounceStart { get; set; }
        public DateTime AnnounceEnd { get; set; }

        public int TopicType { get; set; }
        public int Priority { get; set; }

        public string URL { get; set; }

        public string TopicData { get; set; }

        public int NextTopic { get; set; }

        public int PrevTopic { get; set; }

        public int CategoryId { get; set; }

        
        [ReadOnlyColumn]
        public DotNetNuke.Modules.ActiveForums.DAL2.Content GetContent
        {
            get
            {
                if (ContentId > 0)
                    return new DotNetNuke.Modules.ActiveForums.DAL2.ContentController().Get(ContentId);
                return null;
            }
        }

    }

    public class CA_Topics : Topics
    {
        public string Status { get; set; }
        public bool IsAssigned { get; set; }
        public bool HasSeen { get; set; }
        public int AuthorId { get; set; }
    }
}