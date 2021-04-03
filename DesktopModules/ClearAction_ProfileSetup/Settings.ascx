<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.ProfileClearAction_ProfileSetup.Settings" %>
<div class="row">
    <fieldset>
        <legend><h4>Linkedin Configuration</h4></legend>
        <div class="form_body col-sm-10">
        <div class="form-group">
            Linkedin Client ID:
        <asp:TextBox ID="txtLinkedinAPIKey" runat="server" Width="300" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            Linkedin Client Secret :
        <asp:TextBox ID="txtLinkedinToken" runat="server" Width="300" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            Linkedin SCOPE :
        <asp:TextBox ID="txtScope" runat="server" Width="300" Text="r_basicprofile r_emailaddress" CssClass="form-control"></asp:TextBox>
            <br />
            <small>Seperated by Space: r_basicprofile r_emailaddress (default)</small>
            <br />
            Put this Link in Linkedin APP settings:
            <asp:Label ID="lblReturnUrl" Font-Bold="true" runat="server"></asp:Label>
        </div>
      

    </div>
    </fieldset>

</div>
<div class="row">
    <fieldset>
        <legend>
          <h4>  Dyanmic Question Setup</h4>
        </legend>
        <div class="form_body col-sm-10">
              <div class="form-group">
            First Name :
        
        <asp:DropDownList ID="ddFirstName" runat="server" DataTextField="QuestionText" DataValueField="QuestionID" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            Last Name  :
      
          <asp:DropDownList ID="ddLastName" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Linkedin :
        
        <asp:DropDownList ID="ddquestions1" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Facebook  :
      
          <asp:DropDownList ID="ddquestions2" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Twitter  :
     
            <asp:DropDownList ID="ddquestions3" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Rank#1 :
    
         <asp:DropDownList ID="ddquestions4" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Rank#2 :
          <asp:DropDownList ID="ddquestions5" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Phone :
      
          <asp:DropDownList ID="ddquestions6" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Location :
      
          <asp:DropDownList ID="ddquestions7" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Education :
      
          <asp:DropDownList ID="ddquestions8" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Honors & Reward :
      
          <asp:DropDownList ID="ddquestions9" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Bio/ Overview :
      
          <asp:DropDownList ID="ddquestions10" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Professional & Associates :
      
          <asp:DropDownList ID="ddquestions11" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Work Title :
      
          <asp:DropDownList ID="ddquestions12" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
        <div class="form-group">
            Work History :
      
          <asp:DropDownList ID="ddquestions13" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>

        <div class="form-group">
            Company Name:
      
          <asp:DropDownList ID="ddquestions14" runat="server" DataTextField="QuestionText" DataValueField="QuestionID"></asp:DropDownList>
        </div>
    </div>
    </fieldset>

</div>
