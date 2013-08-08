<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Threads.aspx.cs" Inherits="Members_Threads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="span10 offset1">
       <asp:ValidationSummary ID="ThreadValidationSummary" runat="server"  HeaderText="Oops, there was an error..." ValidationGroup="Thread" CssClass="alert alert-error span10" />
    </div>

    <asp:ObjectDataSource ID="ThreadDetailsObjectDataSource" runat="server" 
        SelectMethod="GetThread" TypeName="Service" DataObjectTypeName="Thread"
        DeleteMethod="DeleteThread" UpdateMethod="SaveThread" 
    ondeleted="ThreadDetailsObjectDataSource_Deleted" 
    onupdated="ThreadDetailsObjectDataSource_Updated" InsertMethod="SaveThread">
        <SelectParameters>
            <asp:QueryStringParameter Name="threadId" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ListView ID="ThreadListView" runat="server" DataSourceID="ThreadDetailsObjectDataSource" DataKeyNames="ThreadId">
        <EditItemTemplate>
             <div class="span12 well">
                <div class="span2 pull-right">
                    <img id="Img1" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
                    <span class="text-error"><%# Eval("UserRole") %></span> ✰✰<br/>
                    <asp:Label runat="server" ID="Label1"><%# Eval("UserName") %></asp:Label>
                </div>
                <div class="span10">
	                <h2>
	                    <asp:RequiredFieldValidator ID="ThreadNameRequiredFieldValidator" runat="server" 
                            ErrorMessage="Thread must have a title" ControlToValidate="ThreadNameTextBox"
                            Display="Dynamic" CssClass="text-error errorInh2" 
                            ValidationGroup="Thread" SetFocusOnError="True">*
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="ThreadNameTextBox" runat="server" Text='<%# Bind("ThreadName") %>' MaxLength="50" />
	                </h2>
	            </div>
                <div class="span10">
                    <p>
                        <asp:RequiredFieldValidator ID="ThreadContentRequiredFieldValidator" runat="server" 
                            ErrorMessage="Thread must have some content" ControlToValidate="ThreadContentTextBox"
                            Display="Dynamic" CssClass="text-error errorInh2" 
                            ValidationGroup="Thread" SetFocusOnError="True">*
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="ThreadContentTextBox" runat="server" Text='<%# Bind("ThreadContent") %>' TextMode="MultiLine" CssClass="span11" Rows="8" MaxLength="1000" />
                    </p>    
                </div>
	            <div class="span11">
	                    <hr>
                   <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="<i class='icon-ok-sign'></i> Save changes" ValidationGroup="Thread" CssClass="btn btn-small btn-success" />
                   <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel"  Text="<i class='icon-remove-circle'></i> Cancel" CssClass="btn btn-small btn-danger" />
    	            | <i class="icon-user"></i> <a href="#"><asp:Label runat="server" ID="Label2"><%# Eval("UserName") %></asp:Label></a> 
    	            | <i class="icon-calendar"></i><asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
     	            | <i class="icon-comment"></i> <a href="#"><asp:Label ID="CommentCountLabel" runat="server" Text='<%# Eval("CommentCount") %>' /> Comments</a>

                </div>            
            </div>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <div class="span9">
                <p class="muted">~ This thread doesn't exist, someone might recently removed it. :(</p>
            </div>              
        </EmptyDataTemplate>
        <ItemTemplate>
        <div class="span12 well">
            <div class="span2 pull-right">
                <img id="Img1" class="img-polaroid" runat="server" src="~/App_Themes/Main/img/user.png"/>
                <span class="text-error"><%# Eval("UserRole") %></span><br/>
                <asp:Label runat="server" ID="UserNameLabel"><%# Eval("UserName") %></asp:Label>
            </div>
        <div class="span10">
	        <h2><asp:Label runat="server" ID="ThreadName"><%# Eval("ThreadName") %></asp:Label></h2>
	    </div>
        <div class="span10">
            <p>
                <asp:Label runat="server" ID="ThreadContent"><%# Eval("ThreadContent") %></asp:Label>
            </p>
        </div>
	    <div class="span11">
	            <hr>
            <asp:LinkButton ID="ThreadEditButton" runat="server" CommandName="Edit" CausesValidation="false" Text="<i class='icon-edit'></i> Edit" CssClass="btn btn-small btn-warning" />
            <asp:LinkButton ID="ThreadDeleteButton" runat="server" CommandName="Delete" CausesValidation="false" Text="<i class='icon-trash'></i> Delete" CssClass="btn btn-small btn-danger" OnClientClick='<%# String.Format("return confirm(\"Do you want to delete &rsquo;{0}&rsquo; permanent?\" )", Eval("ThreadName")) %>' />

    	    | <i class="icon-user"></i><a href="#"><asp:Label runat="server" ID="Label2"><%# Eval("UserName") %></asp:Label></a>
    	    | <i class="icon-calendar"></i><asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
     	    | <i class="icon-comment"></i> <a href="#"><asp:Label ID="CommentCountLabel" runat="server" Text='<%# Eval("CommentCount") %>' /> Comments</a>

        </div>            
    </div>
        </ItemTemplate>
        <LayoutTemplate>
            <div ID="itemPlaceholderContainer" runat="server"></div>
            <div ID="itemPlaceholder" runat="server"></div>
        </LayoutTemplate>
    </asp:ListView>
    
    <com:Comment runat="server" ID="Comment" />
</asp:Content>