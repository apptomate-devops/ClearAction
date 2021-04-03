
using DotNetNuke.Web.Api;
using LogMeIn.GoToCoreLib.Api;
using LogMeIn.GoToWebinar.Api;
using LogMeIn.GoToWebinar.Api.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ClearAction.Modules.WebCast
{

    [AllowAnonymous]
    public class WebCastController : DnnApiController
    {


        [HttpGet()]
        public HttpResponseMessage HelloWorld()
        {
            try
            {
                string helloWorld__1 = "Hello World!";
                return Request.CreateResponse(HttpStatusCode.OK, helloWorld__1);
            }
            catch (System.Exception ex)
            {
                //Log to DotNetNuke and reply with Error

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetLikeContentIDCount(string ContentPostID, string ItemType)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                int tmpLikes = js.Deserialize<int>(ContentPostID);
                int CurrentUserid = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;
                if (CurrentUserid > 0)
                {
                    var dal2 = new Components.DBController();
                    string oUserlike = dal2.Post(tmpLikes, CurrentUserid, ItemType);
                    string ilist = dal2.GetForPostCount(tmpLikes, ItemType);
                    string iUnLikeCount = dal2.GetUnlikeForPost(tmpLikes, ItemType).Count.ToString();
                    LikesDTO dto = new LikesDTO();
                    dto.CommentID = tmpLikes;
                    dto.UserID = CurrentUserid;
                    dto.LikesCount = ilist;
                    dto.UserLiked = oUserlike;
                    dto.UnLikeCount = iUnLikeCount;
                    if ((dto.UserLiked == "AddedLike"))
                    {
                        dto.ImageName = "forum-recommended.png";
                    }
                    else
                    {
                        dto.ImageName = "forum-recommend.png";
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(dto));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "0");
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        public HttpResponseMessage GotoWebinarRegisterAPI(string WebInarKey, string WCCId, string LoginUserId, string ComponentId)
        {
            int status = 1;
            try
            {

                string userName = ConfigurationManager.AppSettings["GotoWebinarLoginEmailAddress"];
                string userPassword = ConfigurationManager.AppSettings["GotoWebinarLoginPassword"];
                string consumerKey = ConfigurationManager.AppSettings["GotoWebinarConsumerKey"];

                var authApi = new AuthenticationApi();
                var response = authApi.directLogin(userName, userPassword, consumerKey, "password");


                var accessToken = response.access_token;
                long OrganizerKey;

                long.TryParse(response.organizer_key, out OrganizerKey);

                try
                {

                    long longWebinarKey = 0;
                    long.TryParse(WebInarKey, out longWebinarKey);

                    WebinarsApi objWebinarsApi = new WebinarsApi();
                    RegistrantsApi objRegistrantsApi = new RegistrantsApi();
                    List<Webinar> objWebinarList = new List<Webinar>();

                    objWebinarList = objWebinarsApi.getAllWebinars(accessToken, OrganizerKey);

                    if (objWebinarList.Where(s => s.webinarKey == longWebinarKey).Any())
                    {
                        List<Registrant> objRegistrantList = new List<Registrant>();
                        try
                        {
                            objRegistrantList = objRegistrantsApi.getAllRegistrantsForWebinar(accessToken, OrganizerKey, longWebinarKey);
                        }
                        catch (Exception ex) { }

                        string EmailAddress = this.UserInfo.Email;
                        string FirstName = this.UserInfo.FirstName;
                        string LastName = this.UserInfo.LastName;



                        if (!objRegistrantList.Where(s => s.email.ToLower() == EmailAddress.ToLower()).Any())
                        {

                            RegistrantFields objRegistrantFields = new RegistrantFields();

                            objRegistrantFields.zipCode = "";
                            objRegistrantFields.purchasingTimeFrame = "";
                            objRegistrantFields.numberOfEmployees = "";
                            objRegistrantFields.industry = "";
                            objRegistrantFields.questionsAndComments = "";
                            objRegistrantFields.jobTitle = "";
                            objRegistrantFields.organization = "";
                            objRegistrantFields.phone = "";
                            objRegistrantFields.country = "";

                            objRegistrantFields.state = "";
                            objRegistrantFields.city = "";
                            objRegistrantFields.address = "";
                            objRegistrantFields.source = "";
                            objRegistrantFields.email = EmailAddress;
                            objRegistrantFields.lastName = LastName;
                            objRegistrantFields.firstName = FirstName;
                            objRegistrantFields.purchasingRole = "";


                            try
                            {
                                RegistrantCreated regisrtant = new RegistrantCreated();
                                regisrtant = objRegistrantsApi.createRegistrant(accessToken, OrganizerKey, longWebinarKey, "application/vnd.citrix.g2wapi-v1.1+json", true, objRegistrantFields);

                                if (regisrtant != null)
                                {

                                    int intWCCId = 0;
                                    int.TryParse(WCCId, out intWCCId);

                                    int intComponentId = 0;
                                    int.TryParse(ComponentId, out intComponentId);

                                    var dal2 = new Components.DBController();

                                    bool saveStatus = dal2.SaveWebinarUserRegistrationDetails(this.UserInfo.UserID, intWCCId, intComponentId, WebInarKey, regisrtant.registrantKey.ToString(), regisrtant.joinUrl);

                                }
                                status = 1;
                            }
                            catch (Exception ex) { }




                        }
                        else
                        {
                            status = 2;
                            //Already Register
                        }


                    }
                    else {
                        status = 0;
                    }

                }
                catch { }


            }
            catch (System.Exception ex)
            {
                status = 0;
            }

            return Request.CreateResponse(HttpStatusCode.OK, status);
        }



        [HttpGet]
        public HttpResponseMessage UnLikeThePost(string ContentPostID, string ItemType)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                int tmpLikes = js.Deserialize<int>(ContentPostID);
                int CurrentUserid = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;
                if (CurrentUserid > 0)
                {
                    var dal2 = new Components.DBController();
                    string oUserlike = dal2.UnLikePost(tmpLikes, CurrentUserid);
                    string ilist = dal2.GetForPostCount(tmpLikes, ItemType);
                    string iUnLikeCount = dal2.GetUnlikeForPost(tmpLikes, ItemType).Count.ToString();
                    var dto = new LikesDTO();
                    dto.CommentID = tmpLikes;
                    dto.UserID = CurrentUserid;
                    dto.LikesCount = ilist;
                    dto.UserLiked = oUserlike;
                    dto.UnLikeCount = iUnLikeCount;
                    return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(dto));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "0");
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        #region "File Attachment"


        [DnnAuthorize]
        [HttpGet()]
        // [IFrameSupportedValidateAntiForgeryToken]
        public HttpResponseMessage GetSaveEditReplyAttachments(int contentId, string fileName, string content, string fileurl)
        {
            bool flagStatus = true;
            try
            {
                var oController = new Components.DBController();
                oController.SaveEditReplyAttachments(contentId, fileName, content, fileurl);
            }
            catch
            {
                flagStatus = false;
            }

            return Request.CreateResponse(flagStatus);

        }



        [DnnAuthorize]
        [HttpGet()]
        // [IFrameSupportedValidateAntiForgeryToken]
        public HttpResponseMessage DeleteAttachmentsFromURL(string fileurl)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            bool statusFlag = true;
            try
            {

                var adb = new Components.DBController();
                adb.DeleteFromURL(fileurl);
            }
            catch
            {
                statusFlag = false;
            }

            return Request.CreateResponse(js.Serialize(statusFlag));

        }



        [DnnAuthorize]
        [HttpGet]
        public HttpResponseMessage SaveFileStackData(string ContentID, int CommentID, string FileName, string contentType, int size, string statckurl)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var oFileStack = new Components.AttachmentInfo();
                if (string.IsNullOrEmpty(statckurl) | string.IsNullOrEmpty(FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Invalid Input");
                }

                oFileStack.CommentID = Convert.ToInt32(ContentID);
                oFileStack.AttachId = -1;
                oFileStack.WCCId = CommentID;
                oFileStack.FileName = FileName;
                oFileStack.FileUrl = statckurl;
                oFileStack.FileSize = size;
                oFileStack.ContentType = contentType;
                int CurrentUserid = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;
                if (CurrentUserid > 0)
                {
                    oFileStack.DateAdded = System.DateTime.Now;
                    oFileStack.UserID = CurrentUserid;

                    var dal2 = new Components.DBController();
                    dal2.AddAttachment(oFileStack);
                    return Request.CreateResponse(HttpStatusCode.OK, "" + oFileStack.FileName + "~" + oFileStack.FileUrl + "|");
                }

                return Request.CreateResponse(HttpStatusCode.OK, "0");
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [DnnAuthorize]
        [HttpGet]
        public HttpResponseMessage GetAttachmentByCommentID(int PortalId, string ContentPostID)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                int tmpLikes = js.Deserialize<int>(ContentPostID);

                var dal2 = new Components.DBController();
                List<Components.AttachmentInfo> olist = dal2.GetAttachmentByComment(tmpLikes);
                List<AttachmentDTO> lTempDTolist = new List<AttachmentDTO>();
                foreach (Components.AttachmentInfo ofile in olist)
                {
                    AttachmentDTO dto = new AttachmentDTO();
                    dto.ContentId = ofile.CommentID;

                    dto.WCCId = ofile.WCCId;
                    dto.FileName = ofile.FileName;
                    dto.FileUrl = ofile.FileUrl;
                    dto.FileSize = ofile.FileSize;
                    dto.ContentType = ofile.ContentType;
                    dto.UserID = ofile.UserID;
                    dto.DateAdded = Components.Util.HumanFriendlyDate(ofile.DateAdded);
                    lTempDTolist.Add(dto);
                }

                return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(lTempDTolist));


            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion  

    }

    public class AttachmentDTO
    {
        public int AttachId { get; set; }
        public int WCCId { get; set; }
        public int ContentId { get; set; }
        public int UserID { get; set; }
        public string FileName { get; set; }
        public string DateAdded { get; set; }
        public string ContentType { get; set; }
        public int FileSize { get; set; }
        public string FileUrl { get; set; }

    }
}