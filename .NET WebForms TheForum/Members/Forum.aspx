<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forum.aspx.cs" Inherits="Members_Forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <div class="row-fluid">
        <div class="span12 well">
            <h2>Our philosophy is to...</h2>
            <img src="../App_Themes/Main/img/eatsleepcode.png" class="forumimg offset3" />
            <h2 class="offset9">...Enjoy!</h2>
        </div>
    </div>

    <div class="row-fluid">
		<div class="span12">
			<div class="alert alert-info">
				Welcome to the forums. Don't forget to post something!
				<a href="#" data-dismiss="alert" class="close">×</a>
			</div>
         <a id="A1" href="~/Members/Create.aspx" runat="server" class="btn btn-success"><i class="icon-pencil"></i> New thread</a>
         <p></p>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetThreads" 
                TypeName="Service">
        </asp:ObjectDataSource>
		        <div class="row-fluid">
                    <table class="table table-bordered table-striped table-hover">
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">                    
                        <LayoutTemplate>
							<thead>
								<tr>
									<th>Threads</th>
                                    <th>Views</th>
                                    <th>Replies</th>
								</tr>
                                <tbody>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                </tbody>
							</thead>
                        </LayoutTemplate>
                        <ItemTemplate>
							<tr>
								<td><asp:HyperLink ID="A1" NavigateUrl='<%# Eval("ThreadId", "~/Members/Threads.aspx?id={0}") %>' runat="server"><asp:Label ID="ThreadNameLabel" runat="server" Text='<%# Eval("ThreadName") %>' /></asp:HyperLink></td><td><%# Eval("ViewCount") %></td>
                                <td><%# Eval("CommentCount") %></td>
							</tr>
                          </ItemTemplate>
                        </asp:ListView>
                        </table>
                      </div>							
				   </div>
			    </div>    							
</asp:Content>
