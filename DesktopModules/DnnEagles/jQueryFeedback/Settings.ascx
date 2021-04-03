<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DnnEagles.jQueryFeedback.DesktopModules.DnnEagles.jQueryFeedback.Settings" %>
<%@ Register src="../../../controls/labelcontrol.ascx" tagname="labelcontrol" tagprefix="uc1" %>
<table>
    <tr>
        <td>                
            <uc1:labelcontrol ID="lblMailReceiver" ResourceKey="MailReceiver" runat="server" Text="Mail Receiver" />    
        </td>
        <td>
        
            <asp:TextBox ID="txtMailReceiver" runat="server"></asp:TextBox>
        
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblMailSubject" ResourceKey="MailSubject" runat="server" Text="Mail Subject" />                
        </td>
        <td>
        
            <asp:TextBox ID="txtMailSubject" runat="server" TextMode="MultiLine"></asp:TextBox>
        
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblMessageSent" ResourceKey="MessageSent" runat="server" Text="Message Sent" />                
        </td>
        <td>
        
            <asp:TextBox ID="txtMessageSent" runat="server" TextMode="MultiLine"></asp:TextBox>
        
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblMessageNotSent" ResourceKey="MessageNotSent" runat="server" Text="Message Not Sent" />                
        </td>
        <td>
        
            <asp:TextBox ID="txtMessageNotSent" runat="server" TextMode="MultiLine"></asp:TextBox>
        
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblDisclaimer" ResourceKey="Disclaimer" runat="server" Text="Disclaimer" />
        </td>
        <td>
        
            <asp:TextBox ID="txtDisclaimer" runat="server" TextMode="MultiLine"></asp:TextBox>
        
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblHideOnSubmit" ResourceKey="HideOnSubmit" runat="server" Text="Hide on submit" />     
        </td>
        <td>
            <asp:DropDownList ID="ddlHide" runat="server">
                <asp:ListItem Text="false" Value="false" Selected="True"></asp:ListItem>
                <asp:ListItem Text="true" Value="true"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblNotifySender" ResourceKey="NotifySender" runat="server" Text="Notify Sender" />     
        </td>
        <td>
            <asp:CheckBox ID="chkNotify" runat="server" Checked="false"/>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblSubject" ResourceKey="Subject" runat="server" Text="Subject" />     
        </td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:labelcontrol ID="lblMailToSender" ResourceKey="MailToSender" runat="server" Text="Mail To Sender" />     
        </td>
        <td>
            <asp:TextBox ID="txtMailToSender" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
</table>