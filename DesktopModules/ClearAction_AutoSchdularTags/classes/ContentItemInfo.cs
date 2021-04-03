using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Users;
namespace ClearAction_AutoSchdularTags
{
    public class BlogInfo
    {

        public int ContentItemid
        { get; set; }



        private List<GlobalCategory> GlobalCategory;
        public List<GlobalCategory> GetAssoicatedCategory
        {
            get
            {
                if (ContentItemid > 0)
                    GlobalCategory = new DBController().GetGlobalCategoryBlog(ContentItemid).ToList();
                return GlobalCategory;
            }

        }
    }

    public class ForumInfo
    {

        public int TopicId
        { get; set; }


        public int ContentID
        { get; set; }

        private List<GlobalCategory> GlobalCategory;
        public List<GlobalCategory> GetAssoicatedCategory
        {
            get
            {
                if (TopicId > 0)
                    GlobalCategory = new DBController().GetGlobalCategoryForum(TopicId).ToList();
                return GlobalCategory;
            }

        }

    }

    public class SolveSpaceInfo
    {

        public int SolveSpaceID
        { get; set; }


        private List<GlobalCategory> GlobalCategory;
        public List<GlobalCategory> GetAssoicatedCategory
        {
            get
            {
                if (SolveSpaceID > 0)
                    GlobalCategory = new DBController().GetCategoryBySolveSpaceID(SolveSpaceID).ToList();
                return GlobalCategory;
            }

        }

    }




}