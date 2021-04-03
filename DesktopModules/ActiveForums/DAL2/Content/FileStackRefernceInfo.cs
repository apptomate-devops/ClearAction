using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    [TableName("Activeforum_FileStackRefernce")]
    [PrimaryKey("FileId")]
    public class FileStackRefernceInfo
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string filestackurl { get; set; }
        public int ContentId { get; set; }
    }
}

///Ccoment form nothin
