/*
' Copyright (c) 2018 ClearAction
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using ClearAction.Modules.Dashboard.Components;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System.Web.Configuration;

namespace ClearAction.Modules.SummaryActions.Components
{
    
   public class SA_UserRelationshipInfo: UserRelationshipInfo
    {
        public string Title
        {
            get
            {
                try
                {
                    return GetResponseText(this.RelatedUserID, "Title");
                }

                catch (Exception ex) { return ""; }
            }
        }
        public string Roles
        {
            get
            {

                return GetcurrentUserRoleName(this.RelatedUserID);
            }
        }
        private string ExeculdeRoles
        {
            get
            {
                string strConfigVaulue = System.Configuration.ConfigurationManager.AppSettings["Exculde_Role"];
                if (string.IsNullOrEmpty(strConfigVaulue))
                    strConfigVaulue = "Administrators, Registered Users, Subscribers, Translator (en-US),Unverified Users";
                return strConfigVaulue;
            }
        }
        private string GetResponseText(int iUserID, string Key)
        {

            string QuestionID = PortalController.GetPortalSetting(Key, 0, string.Empty);
            if (string.IsNullOrEmpty(QuestionID))
                return "";
            DTOResponse t;
            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteSingleOrDefault<DTOResponse>(System.Data.CommandType.StoredProcedure, "CA_GetQuestionResponseByUserID", iUserID, QuestionID);
            }
            if (t == null)
            {
                return "";
            }
            else
            {
                return t.ResponseText;
            }
           
        }
        private string GetcurrentUserRoleName(int iUserID)
        {
            string[] strRole = UserController.GetUserById(0, iUserID).Roles;
            string strRoleToExecludes = ExeculdeRoles;
            string struserrole = string.Empty;
            foreach (string strrolename in strRole)
            {
                if (!strRoleToExecludes.Contains(strrolename))
                {
                    struserrole = strrolename;
                }


            }
            return struserrole;
        }
    }

    public class CA_UserInActiveInfo
    {
        public  int UserSessionInMin
        {
            get
            {
                int rValue = 20;
                if (WebConfigurationManager.AppSettings["CA_UserSessionInMin"] == null)
                {
                    rValue = 20;
                }
                else
                {
                    rValue = int.Parse(WebConfigurationManager.AppSettings["CA_UserSessionInMin"].ToString());
                }
                return rValue;
            }
        }
        public int PromptBeforeInMin
        {
            get
            {
                int rValue = 20;
                if (WebConfigurationManager.AppSettings["CA_PromptBeforeInMin"] == null)
                {
                    rValue = 20;
                }
                else
                {
                    rValue = int.Parse(WebConfigurationManager.AppSettings["CA_PromptBeforeInMin"].ToString());
                }
                return rValue;

            }
        }
    }
}
