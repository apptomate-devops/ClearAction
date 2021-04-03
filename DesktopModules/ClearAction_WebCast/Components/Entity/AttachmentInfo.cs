using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ClearAction.Modules.WebCast.Components
{
    [TableName("WCC_Attachments")]
    [PrimaryKey("AttachId")]
    public class AttachmentInfo
    {
        public int AttachId { get; set; }
        public int WCCId { get; set; }
        //    public int CommentId { get; set; }
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public string FileName { get; set; }
        public DateTime DateAdded { get; set; }
        public string ContentType { get; set; }
        public int FileSize { get; set; }
        public string FileUrl { get; set; }
         
    }

    public class AttachmentData
    {
        public string CommentId { get; set; }
        public string currentCommentId { get; set; }
        public string attachmentURL { get; set; }
        public string size { get; set; }
        public string contenttype { get; set; }
        public string filename { get; set; }

    }

   


}