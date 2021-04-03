using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using ClearAction_Chat.Models;
using ClearAction_Chat.Services;
namespace ClearAction_Chat
{
    public abstract partial class View : DotNetNuke.Entities.Modules.PortalModuleBase
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            ClearActionChatServices objClearActionChatServices = new ClearActionChatServices();
            List<DNNUserList> objDNNUserList = new List<DNNUserList>();
            try
            {


                //DotNetNuke.Framework.AJAX.RegisterScriptManager();
                //DotNetNuke.Framework.AJAX.RegisterPostBackControl(btnRunReport);

                DotNetNuke.Framework.AJAX.RegisterScriptManager();


                HiddenUserPOrtalId.Value = this.PortalId.ToString();
                int UserId = this.UserInfo.UserID;
                HiddenUserId.Value = UserId.ToString();

                string loginUserFullName = (string.IsNullOrEmpty(this.UserInfo.DisplayName)) ? this.UserInfo.Username : this.UserInfo.DisplayName;
                HiddenUserFullName.Value = loginUserFullName;
                HiddenFieldImageURL.Value = this.UserInfo.Profile.PhotoURL;

                HiddenSelectedUserId.Value = "5";

                objDNNUserList = objClearActionChatServices.GetAllDNNUserList(this.PortalId, UserId);


                RepterUserList.DataSource = objDNNUserList;
                RepterUserList.DataBind();
            }
            catch
            {
                objDNNUserList = new List<DNNUserList>();
            }
        }



        protected void UcmdSave_Click(object sender, EventArgs e)
        {
            if (FileUploadAttachment.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }

            if (FileUpload1.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }
        }

        protected void FileUploadAttachment_DataBinding(object sender, EventArgs e)
        {
            if (FileUploadAttachment.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }

            if (FileUpload1.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }
        }

        protected void FileUpload1_DataBinding(object sender, EventArgs e)
        {
            if (FileUploadAttachment.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }

            if (FileUpload1.HasFile)
            {
                //FileUploadAttachment.SaveAs(Server.MapPath("~/Uploads/" + Path.GetFileName(FileUploadAttachment.FileName)));
            }
        }


        


    }
}