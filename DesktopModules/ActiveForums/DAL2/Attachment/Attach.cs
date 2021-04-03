using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DotNetNuke.Modules.ActiveForums.DAL2.Attachment
{
    [TableName("activeforums_Attachments")]
    [PrimaryKey("AttachID")]
    public class Attach
    {
        public int AttachID { get; set; }

        public int ContentId { get; set; }

        public int UserId { get; set; }

        public string FileName { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }

        public long FileSize { get; set; }

        public string ContentType { get; set; }        

        public byte[] FileData { get; set; }

        public bool AllowDownload { get; set; }

        public bool DisplayInline { get; set; }

        public int DownloadCount { get; set; }
        public int ParentAttachId { get; set; }
        public int FileID { get; set; }

        public string filestackurl { get; set; }



    }
}
