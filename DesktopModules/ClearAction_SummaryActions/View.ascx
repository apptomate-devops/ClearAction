<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.SummaryActions.View" %>
<div>
    <div class="CA_SummaryActionContainer">
        <div class="CA_ActionRow">
            <div>
                <span class="actions_title">Next Steps:</span>
            </div>
            <div>
                &nbsp;
            </div>
        </div>
        <div class="CA_ActionRow">
            <div class="icon">
                <img src="/Portals/0/Skins/DemoSkin/images/solvespaces/icons/icon_share.png" alt="" />
            </div>
            <div class="action">
              <span class="CA_ActionName">Share this Solve-Space<sup>TM</sup> ACTION PLAN</span>
                <br /><br /><br />
                <div id="CA_ShareSummaryAsPDF">
                    <input type="button" id="CA_btnShareSummary" value="Share WITH my answer" onclick="CA_SetButton(1);" class="btn btn-info btn-lg" data-toggle="modal" data-target="#modalShareSummary" />
                    <input type="button" id="CA_btnShareLink" value="Share WITHOUT my answer" onclick="CA_SetButton(2);" class="btn btn-info btn-lg" data-toggle="modal" data-target="#modalShareSummary" />
                </div>
            </div>
      </div>
        <div class="CA_ActionRow">
          &nbsp;
        </div>
        <div class="CA_ActionRow">
            <div class="action">
                <asp:LinkButton runat="server" ID="lnkDownloadPDF" CssClass="CA_ActionLink" Style="color: #FFF !important" OnClientClick="return CA_DownloadPDFNew(this);" OnClick="lnkDownloadPDF_Click">
                  <img src="/Portals/0/Skins/DemoSkin/images/solvespaces/icons/icon_pdf.png" alt="" />
                  <span class="CA_ActionName" style="color: #FFF !important;">PDF</span>
                  &#8212; Download this ACTION PLAN</asp:LinkButton>
            </div>
        </div>
        <div class="CA_ActionRow">
            <div class="action">
                <asp:LinkButton runat="server" ID="lnkPrint" CssClass="CA_ActionLink" Style="color: #FFF !important" OnClientClick="return CA_PrintSSP();">
               <img src="/Portals/0/Skins/DemoSkin/images/solvespaces/icons/icon_print.png" alt="" />                
                  <span style="color: #FFF !important;">Print</span></asp:LinkButton>
            </div>
        </div>
        <div class="CA_ActionRow">
            <div class="action">
<a href="https://exchange.clearaction.com/Forum"><span class="CA_ActionLink" id="CA_lnkJoinTheDiscussion" onclick="https://exchange.clearaction.com/Forum">
                <img src="/Portals/0/Skins/DemoSkin/images/solvespaces/icons/icon_solvespace.png" alt="" />
               <span class="CA_ActionName" style="color: #FFF !important;">Comments</span>
  &#8212; Join the discussion</span></a>
            </div>
        </div>
        <div class="CA_ActionRow">
            <div class="action">
             <p style="color: #ffffff;">Click the "Done" button to mark <strong><span class="CA_SSPName">&nbsp;</span></strong> as "Done" (with green dot) in your Solve-Spaces<sup>TM</sup> --> <a href="/SolveSpaces.aspx" style="color: #FFF !important;">My Vault.</a><br /> You can always access it there to review or re-work.</p>
            </div>
        </div>
        <div class="CA_ActionRow last">
            <div class="icon">
            </div>
            <div class="action">
            </div>
        </div>
        <div style="clear: both"></div>
    </div>
</div>
<div id="editor"></div>
<div id="modalShareSummary" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div>
                    <h4 id="CA_H4Header" class="modal-title">Share this Solve-Space WITH my answers:</h4>
                    <div>
                        <div class="my_connections">My Connections: </div>
                        <div>
                            <ul id="CA_ulUsers"></ul>
                        </div>
                    </div>
                    <div style="display: none;">
                        <div>Email Subject: </div>
                        <div>
                            <input type="text" id="CA_txtSubject" />
                        </div>
                    </div>
                    <div style="display: none;">
                        <div>Email Content: </div>
                        <div>
                            <textarea id="CA_txtContent" rows="8"></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div>
                    <button type="button" class="btn btn-info" id="CA_btnSharePDF" onclick="return CA_ValidateNSend(this);">Send via email</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdHTMLString" runat="server" />
<div id="finalPDFHTMLContent" style="display:none;"></div>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.debug.js"></script>
<script src="/DesktopModules/ClearAction_SummaryActions/js/html2canvas.min"></script>
<script src="/Desktopmodules/ClearAction_SummaryActions/js/Module.js"></script>
<script>
    var CA_UserID =<%=this.UserId %>;
    var oKey = '<%=GetAPIKey%>';
    //Forum/CategoryId/-1/SortBy/-1/TopicId/59/PI/1/ContentID/59
</script>
