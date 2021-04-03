using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotNetNuke.Modules.ActiveForums.DAL2.Attachment;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    public class ContentController
    {
        IDataContext ctx;
        IRepository<Content> repo;

        public ContentController()
        {
            ctx = DataContext.Instance();
            repo = ctx.GetRepository<Content>();
        }

        public Content Get(int contentId)
        {
            var content = repo.GetById(contentId);
            return content;
        }

        
    }

    
     
}
