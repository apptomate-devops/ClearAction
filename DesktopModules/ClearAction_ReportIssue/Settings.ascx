<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.ReportIssue.Settings" %>

<table cellpadding="10" cellspacing="10">
    <tr>
        <td>                
            <asp:Label  ID="lblMailReceiver" ResourceKey="MailReceiver" runat="server" Text="Mail Receiver" />    
        </td>
        <td>
            <asp:TextBox ID="txtMailReceiver" runat="server" style="width:350px;" ></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td>                
            <asp:Label  ID="lblSender" ResourceKey="MailReceiver" runat="server" Text="Mail Sender" />    
        </td>
        <td>
            <asp:TextBox ID="txtSender" runat="server" style="width:350px;" ></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:Label ID="lblMailSubject" ResourceKey="MailSubject" runat="server" Text="Mail Subject" />                
        </td>
        <td>
            <asp:TextBox ID="txtMailSubject" style="width:400px;"  Width="250" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMessageSent" ResourceKey="MessageSent" runat="server" Text="Message Sent" />                
        </td>
        <td>
            <asp:TextBox ID="txtMessageSent" style="width:400px;height:200px;" runat="server" Rows="5" Columns="20" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    
    
    <tr>
        <td colspan="2">
            <label>##module## : Module type e.g. Forum / Blog / General </label><br />
            <label>##issuetype## : Issue type e.g. Spam, Malware or virus etc </label><br />
            <label>##data## : All the details entered by the end-user while reporting the issue.</label><br />

        </td>
    </tr>
</table>