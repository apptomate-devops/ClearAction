using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNuke.Modules.ActiveForums
{
    public class LikesDTO
    {


        public int CommentID;
        public int UserID;
        public string LikesCount;
        public bool UserLiked;

        public string ImageName;

        public string DisLikeCount;
        public string DisLikeImageName;
    }

    public class AutoCompleteTag
    {
        public string Name { get; set; }
        //public int VocabularyID { get; set; }
    }


    public class FileStackRefernceDTO
    {
        public long FileId;
        public int ContentId;
        public string filestackurl;
        public string FileName;
    }
}
