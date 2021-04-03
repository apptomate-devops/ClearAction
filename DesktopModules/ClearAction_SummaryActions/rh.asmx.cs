using ClearAction.Modules.Dashboard.Components;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using DotNetNuke.Modules.ActiveForums;
using DotNetNuke.Entities.Portals;
using ClearAction.Modules.SummaryActions.Components;
using System.Web.Configuration;
using DotNetNuke.Modules.ActiveForums.DAL2;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
namespace ClearAction.Modules.SummaryActions
{
    /// <summary>
    /// Summary description for rh
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class rh : System.Web.Services.WebService
    {

        [WebMethod]
        public string SendOverEmail(string data,string csvIDs,int senderID)
        {
            string Subject = "Summary Shared";
            string ContentTemplate = "Hey {0} I just took this solvespace. Please find the attachment. I think you would be interested in it.";
            //Create PDF
            var fileName = CreatePDFFile(data);
            string[] aryIDs = csvIDs.Split(',');
            UserInfo userSender = DotNetNuke.Entities.Users.UserController.GetUserById(0, senderID);
            // iterate through each user and send email with attachment to them
            foreach (string uID in aryIDs){
                if (uID.Trim() != "")
                {
                    int ID = int.Parse(uID);
                    UserInfo toUser = DotNetNuke.Entities.Users.UserController.GetUserById(0, ID);
                    // Add user name 
                  string  Content = string.Format(ContentTemplate, toUser.DisplayName);
                    // Create Attachment for E-mail
                    List<System.Net.Mail.Attachment> lstAttachments = new List<System.Net.Mail.Attachment>();
                    System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                    lstAttachments.Add(new System.Net.Mail.Attachment(new FileStream(fileName, FileMode.Open), fi.Name, "application/pdf"));
                    // Send Email with attachment
                     DotNetNuke.Services.Mail.Mail.SendEmail(userSender.Email, "members@clearaction.com", toUser.Email, Subject, Content, lstAttachments);
                    // DotNetNuke.Services.Mail.Mail.SendEmail(userSender.Email, "members@clearaction.com", "sachinmcsd@gmail.com", Subject, Content, lstAttachments);
                }
            }
            // Now delete the PDF after attaching and sending it as in email
            System.IO.File.Delete(fileName);
            return "Done";
        }
        [WebMethod]
        public string ShareLink(string link, string csvEmails, int senderID)
        {
            // Subject = "Summary Shared";
            //Content = "Hey {0} I just took this solvespace. I just took this solvespace. GO here <link> to add it to your vault. I think you would be interested in taking it.";
            string[] aryEmails = csvEmails.Split(',');
            UserInfo userSender = DotNetNuke.Entities.Users.UserController.GetUserById(0, senderID);

            foreach (string uEmail in aryEmails)
            {
                if (uEmail.Trim() != "")
                {
                    UserInfo toUser = DotNetNuke.Entities.Users.UserController.GetUserByEmail(0, uEmail);
                }
            }
            return "Done";
        }

        [WebMethod]
        public List<SA_UserRelationshipInfo> GetUserConnections(int UserID)
        {
            List<SA_UserRelationshipInfo> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteQuery<SA_UserRelationshipInfo>(System.Data.CommandType.StoredProcedure, "CA_GetConnectionsByUserID", UserID).ToList<SA_UserRelationshipInfo>();
            }
//            t = CBO.FillCollection<UserRelationshipInfo>(SqlDataProvider.Instance().ExecuteReader("CA_GetConnectionsByUserID", UserID));
            return t;
        }
       
        [WebMethod]
        public string GetJoinDiscussionLink(int SSID)
        {
            string rValue = "";
            SolveSpaces.Components.SolveSpaceController oSCtrl = new SolveSpaces.Components.SolveSpaceController();
            SolveSpaces.Components.SolveSpaceInfo oSInfo = oSCtrl.GetSolveSpacesByID(SSID);
            if (oSInfo != null)
            {
                TopicsController oCtrl = new TopicsController();
                TopicInfo oTopic = oCtrl.Topics_Get(0, -1, oSInfo.ForumTopicID);
                //Forum/CategoryId/-1/SortBy/-1/TopicId/57/PI/1/ContentID/57
                rValue = "/discussion/CategoryId/-99/SortBy/-1/TopicId/" + oTopic.TopicId.ToString() + "/PI/1/ContentID/" + oTopic.ContentId.ToString();
            }
            return rValue;
        }
        [WebMethod]
        public int GetForumCommentCount(int SSID)
        {
            int rValue = 0;
            SolveSpaces.Components.SolveSpaceController oSCtrl = new SolveSpaces.Components.SolveSpaceController();
            // load the solvespace information
            SolveSpaces.Components.SolveSpaceInfo oSInfo = oSCtrl.GetSolveSpacesByID(SSID);
            if (oSInfo != null)
            {
                TopicsController oCtrl = new TopicsController();
                // get related private forum detail
                TopicInfo oTopic = oCtrl.Topics_Get(0, -1, oSInfo.ForumTopicID);
                Dal2Controller oDCtrl = new Dal2Controller();
                //get the count detail
                rValue = oDCtrl.GetTopicReplyCount(oTopic.TopicId);
            }
            return rValue;
        }

        [WebMethod]
        public CA_UserInActiveInfo GetUserInactiveInfo()
        {
            CA_UserInActiveInfo oInfo = new CA_UserInActiveInfo();
            return oInfo;
        }

        private string CreatePDFFile(string HTMLString){
            #region iTextSharp Implementation
                StringReader sr = new StringReader(HTMLString.Trim());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                var dir = Server.MapPath("~/DesktopModules/ClearAction_SummaryActions/");

                if (!Directory.Exists(dir))        Directory.CreateDirectory(dir);
                var _TempFileName = DateTime.Now.ToString("yyyyMMdd-HHMMss") + ".pdf";
                var fileName = dir + "\\Solve-Space-" + _TempFileName;
        
                using (MemoryStream memoryStream = new MemoryStream()){
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(new StringReader(HTMLString.Trim()));
                // close the doc object
                    pdfDoc.Close();
                // convert from memoryStream array into bytes
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                // write the bytes into the physical file
                    System.IO.File.WriteAllBytes(fileName, bytes);
                }
                // Return the filename with Path
                return fileName;
            #endregion
        }

        private string EncodeTo64(string toEncode){
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
