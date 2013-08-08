<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comment.ascx.cs" Inherits="Shared_UserControls_Comment" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetComments" 
    TypeName="Service" DataObjectTypeName="Comment" 
    DeleteMethod="DeleteComment" InsertMethod="SaveComment" 
    UpdateMethod="SaveComment" ondeleted="ObjectDataSource1_Deleted" 
    oninserted="ObjectDataSource1_Inserted" 
    onupdated="ObjectDataSource1_Updated" oninserting="ObjectDataSource1_Inserting">
    <SelectParameters>
        <asp:QueryStringParameter Name="threadId" QueryStringField="id" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="comment" Type="Object"/>
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="comment" Type="Object"/>
    </InsertParameters>
</asp:ObjectDataSource>
<div class="span9">
    <asp:LinkButton ID="CommentNewButton" runat="server"  CausesValidation="false" Text="<i class='icon-pencil'></i> Reply" CssClass="btn btn-success" onclick="CommentNewButton_Click" />
</div>
<div class="span8 offset1">
    <asp:ValidationSummary ID="CommentValidationSummary" runat="server"  HeaderText="Oops, there was an error..." ValidationGroup="Comment" CssClass="alert alert-error span10" />
</div>
<asp:ListView ID="CommentListView" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="CommentId">
    <EditItemTemplate>
        <div class="span9 well">
            <div class="span2 pull-right">
                <img id="Img1" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
                <span class="text-success"> <asp:Label ID="RoleLabel" runat="server" Text='<%# Eval("Role") %>' /></span>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UserName") %>' />
            </div>
            <div class="span10">
	            <h5><span class="icon-share-alt"></span>Re: <asp:Label ID="Label1" runat="server" Text='<%# Eval("ThreadName") %>' /></h5>
	        </div>
            <div class="span10">
                <asp:RequiredFieldValidator ID="CommentContentRequiredFieldValidator" runat="server" 
                    ErrorMessage="Thread must have some content" ControlToValidate="CommentContentTextBox"
                    Display="Dynamic" CssClass="text-error errorInh2" 
                    ValidationGroup="Comment" SetFocusOnError="True">*
                </asp:RequiredFieldValidator>
                <asp:TextBox ID="CommentContentTextBox" runat="server" Text='<%# Bind("CommentContent") %>' TextMode="MultiLine" CssClass="span11" Rows="8" MaxLength="500" />
            </div>
	        <div class="span11">
	                <hr>
                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="<i class='icon-ok-sign'></i> Save changes" ValidationGroup="Comment" CssClass="btn btn-small btn-success" />
                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel"  Text="<i class='icon-remove-circle'></i> Cancel" CssClass="btn btn-small btn-danger" />
    	        | <i class="icon-user"></i> <a href="#"><asp:Label ID="NameLabel" runat="server" Text='<%# Eval("UserName") %>' /></a> 
    	        | <i class="icon-calendar"></i> <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />

            </div>
        </div>
    </EditItemTemplate>
    <EmptyDataTemplate>
        <div class="span9">
            <p class="muted">~ There are no comments yet. :(</p>
        </div>
    </EmptyDataTemplate>
    <InsertItemTemplate>
        <div class="span9 well">
            <div class="span2 pull-right">
                <img id="Img2" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
                <asp:Label class="text-success" runat="server" Text="Role" />
                <br/>
                <asp:Label ID="Label3" runat="server" Text='Name' />
            </div>
            <div class="span10">
	            <h5><span class="icon-share-alt"></span>Re: <asp:Label ID="Label4" runat="server" Text='' /></h5>
	        </div>
            <div class="span10">
                <asp:RequiredFieldValidator ID="CommentContentRequiredFieldValidator" runat="server" 
                    ErrorMessage="Thread must have some content" ControlToValidate="CommentContentTextBox"
                    Display="Dynamic" CssClass="text-error errorInh2" 
                    ValidationGroup="Comment" SetFocusOnError="True">*
                </asp:RequiredFieldValidator>
                <asp:TextBox ID="CommentContentTextBox" runat="server" Text='<%# Bind("CommentContent") %>' TextMode="MultiLine" CssClass="span11" Rows="8" />
            </div>
	        <div class="span11">
	                <hr>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Insert" Text="<i class='icon-ok-sign'></i> Save changes" CssClass="btn btn-small btn-success" ValidationGroup="Comment" />
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="CancelButton_Click" CommandName="Cancel"  Text="<i class='icon-remove-circle'></i> Cancel" CssClass="btn btn-small btn-danger" />
    	        | <i class="icon-user"></i> <a href="#"><asp:Label ID="NameLabel" runat="server" Text='Name' /></a> 
    	        | <i class="icon-calendar"></i> <asp:Label ID="Label5" runat="server" Text='Date' />

            </div>
        </div>
    </InsertItemTemplate>
    <ItemTemplate>
        <div class="span9 well">
            <div class="span2 pull-right">
                <img id="Img1" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
                <asp:Label class="text-success"> <asp:Label ID="RoleLabel" runat="server" Text='<%# Eval("Role") %>' />
                <br/>
                </asp:Label><asp:Label ID="Label2" runat="server" Text='<%# Eval("UserName") %>' />
            </div>
            <div class="span10">
	            <h5><span class="icon-share-alt"></span>Re: <asp:Label ID="Label1" runat="server" Text='<%# Eval("ThreadName") %>' /></h5>
	        </div>
            <div class="span10">
                <asp:Label ID="CommentContentLabel" runat="server" Text='<%# Eval("CommentContent") %>' />
            </div>
	        <div class="span11">
	                <hr>
                <asp:LinkButton ID="CommentEditButton" runat="server" CommandName="Edit" CausesValidation="false" Text="<i class='icon-edit'></i> Edit" CssClass="btn btn-small btn-warning" />
                <asp:LinkButton ID="CommentDeleteButton" runat="server" CommandName="Delete" CausesValidation="false" Text="<i class='icon-trash'></i> Delete" CssClass="btn btn-small btn-danger" OnClientClick='<%# "return confirm(\"Do you want to delete this comment permanent?\")" %>' />
    	        | <i class="icon-user"></i><a href="#"><asp:Label ID="NameL" runat="server" Text='<%# Eval("UserName") %>' /></a> 
    	        | <i class="icon-calendar"></i><asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />

            </div>
        </div>
    </ItemTemplate>
    <LayoutTemplate>
        <div ID="itemPlaceholderContainer" runat="server"></div>
        <div ID="itemPlaceholder" runat="server"></div>
    </LayoutTemplate>
</asp:ListView>
