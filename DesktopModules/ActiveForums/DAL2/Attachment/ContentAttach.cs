using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DotNetNuke.Modules.ActiveForums.DAL2.Attachment
{
    
    public class ContentAttach
    {
        public int ContentId { get; set; }

        public int AttachId { get; set; }

        public string FileURL { get; set; }

        public string FileName { get; set; }

        public int UserId { get; set; }
        
    }
}
