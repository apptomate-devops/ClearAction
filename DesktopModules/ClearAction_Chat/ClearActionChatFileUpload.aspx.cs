using System;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ClearAction_Chat
{
    public partial class ClearActionChatFileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (string file in Request.Files)
            {

            }
        }

        [WebMethod(EnableSession = true)]
      
        public static string UploadedImages()
        {

            if (HttpContext.Current.Request.Files != null)
            {
                var file = HttpContext.Current.Request.Files[0];
            }

            //foreach (string file in  Request.Files)
            //{
            //    var fileContent = Request.Files[file];
            //    if (fileContent != null && fileContent.ContentLength > 0)
            //    {
            //        var stream = fileContent.InputStream;
            //        string stringFileName = "";
            //        var fileName = fileContent.FileName;
            //    }
            //}

            return "success12";
        }
    }
}