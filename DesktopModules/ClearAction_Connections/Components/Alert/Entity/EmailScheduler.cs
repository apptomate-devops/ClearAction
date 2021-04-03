using System;
 

using DotNetNuke.ComponentModel.DataAnnotations;
 
namespace ClearAction.Modules.Connections
{
    [TableName("ClearAction_EmailScheduler")]
    [PrimaryKey("EmailSchedulerId")]
    [Cacheable("ClearAction_EmailSchedulerId", System.Web.Caching.CacheItemPriority.Normal, 20)]
    public class EmailScheduler
    {
        public int EmailSchedulerId { get; set; }
        public string ReceiverEmail { get; set; }
        public string RecieverName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateOnDate { get; set; }
        public bool IsSent { get; set; }
        public DateTime SentOnDate { get; set; }
        public string AttachFileName { get; set; }
        public string AttachFileLink { get; set; }
    }
}