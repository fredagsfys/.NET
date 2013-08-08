<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Members_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="span10 offset1">
       <asp:ValidationSummary ID="ThreadCreateValidationSummary" runat="server"  HeaderText="Oops, there was an error..." ValidationGroup="CreateThread" CssClass="alert alert-error span10" />
    </div>

    <div class="span10 well">
        <div class="span2 pull-right">
            <img id="Img1" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
            <span class="text-error">Role</span><br/>
            <asp:Label runat="server" ID="Label1">Name</asp:Label>
        </div>
        <div class="span10">
	        <h2>
	            <asp:RequiredFieldValidator ID="ThreadNameRequiredFieldValidator" runat="server" 
                    ErrorMessage="Thread must have a title" ControlToValidate="ThreadNameTextBox"
                    Display="Dynamic" CssClass="text-error errorInh2" 
                    ValidationGroup="CreateThread" SetFocusOnError="True">*
                </asp:RequiredFieldValidator>
                <asp:TextBox ID="ThreadNameTextBox" runat="server" Placeholder="Thread title" MaxLength="50"></asp:TextBox>
	        </h2>
	    </div>
        <div class="span10">
            <p>
                <asp:RequiredFieldValidator ID="ThreadContentRequiredFieldValidator" runat="server" 
                    ErrorMessage="Thread must have some content" ControlToValidate="ThreadContentTextBox"
                    Display="Dynamic" CssClass="text-error errorInh2" 
                    ValidationGroup="CreateThread" SetFocusOnError="True">*
                </asp:RequiredFieldValidator>

                <asp:TextBox ID="ThreadContentTextBox" runat="server" CssClass="span11" 
                    Rows="8" TextMode="MultiLine" Placeholder="Thread content..." MaxLength="1000"></asp:TextBox>
            </p>    
        </div>
	    <div class="span11">
	            <hr>
            <asp:LinkButton ID="CreateSubmitButton" runat="server" Text="<i class='icon-ok-sign'></i> Create thread" CssClass="btn btn-small btn-success" ValidationGroup="CreateThread" onclick="CreateSubmitButton_Click" />
            <a id="A1" href="~/Members/Forum.aspx" runat="server" class="btn btn-small btn-danger"><i class='icon-remove-circle'></i> Cancel</a>
    	    | <i class="icon-user"></i> <a href="#"><asp:Label runat="server" ID="Label2">Name</asp:Label></a> 
    	    | <i class="icon-calendar"></i><asp:Label ID="DateLabel" runat="server" Text='Date' />
     	    | <i class="icon-comment"></i> <a href="#"><asp:Label ID="CommentCountLabel" runat="server" Text='' /> Comments</a>

        </div>            
    </div>
</asp:Content>