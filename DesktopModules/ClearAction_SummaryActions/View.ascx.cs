/*
' Copyright (c) 2018  ClearAction
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
using System.Web.UI.WebControls;
using ClearAction.Modules.SummaryActions.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
//using HiQPdf;
using System.Web;
using System.IO;
//using HiQPdf;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace ClearAction.Modules.SummaryActions
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_SummaryActionsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ActionsModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               // var tc = new ItemController();
                //rptItemList.DataSource = tc.GetItems(ModuleId);
                //rptItemList.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void rptItemListOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //    var lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
            //    var lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;

            //    var pnlAdminControls = e.Item.FindControl("pnlAdmin") as Panel;

            //    var t = (Item)e.Item.DataItem;

            //    if (IsEditable && lnkDelete != null && lnkEdit != null && pnlAdminControls != null)
            //    {
            //        pnlAdminControls.Visible = true;
            //        lnkDelete.CommandArgument = t.ItemId.ToString();
            //        lnkDelete.Enabled = lnkDelete.Visible = lnkEdit.Enabled = lnkEdit.Visible = true;

            //        lnkEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Edit", "tid=" + t.ItemId);

            //        ClientAPI.AddButtonConfirm(lnkDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
            //    }
            //    else
            //    {
            //        pnlAdminControls.Visible = false;
            //    }
            //}
        }


        public void rptItemListOnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "Edit")
            //{
            //    Response.Redirect(EditUrl(string.Empty, string.Empty, "Edit", "tid=" + e.CommandArgument));
            //}

            //if (e.CommandName == "Delete")
            //{
            //    var tc = new ItemController();
            //    tc.DeleteItem(Convert.ToInt32(e.CommandArgument), ModuleId);
            //}
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        public void lnkDownloadPDF_Click(object sender, EventArgs e)
        {
            //return;
            if (hdHTMLString.Value.Trim() != "")
            {
                #region iTextSharp Implementation [W O R K I N G   C O D E ] 
                /*SAchin : PDF is generating now, I just have to look for tables on UI should be formatted in PDF too, 
                 *         right now , the table borders are not rendering, otherwise the content is coming up in the PDF
                 */
                //  hdHTMLString.Value = "<div><strong>I am bold and brave</strong></div>";
                StringReader sr = new StringReader(hdHTMLString.Value.Trim());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    htmlparser.Parse(new StringReader(hdHTMLString.Value.Trim()));
                    pdfDoc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    // Clears all content output from the buffer stream                 
                    Response.Clear();
                    // Gets or sets the HTTP MIME type of the output stream.
                    Response.ContentType = "application/pdf";
                    // Adds an HTTP header to the output stream
                    Response.AddHeader("Content-Disposition", "attachment; filename=SolveSpace_Summary.pdf");

                    //Gets or sets a value indicating whether to buffer output and send it after
                    // the complete response is finished processing.
                    Response.Buffer = true;
                    // Sets the Cache-Control header to one of the values of System.Web.HttpCacheability.
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    // Writes a string of binary characters to the HTTP output stream. it write the generated bytes .
                    Response.BinaryWrite(bytes);
                    // Sends all currently buffered output to the client, stops execution of the
                    // page, and raises the System.Web.HttpApplication.EndRequest event.

                    Response.End();
                    // Closes the socket connection to a client. it is a necessary step as you must close the response after doing work.its best approach.
                    Response.Close();
                }
                #endregion
            }
        }
    }
}