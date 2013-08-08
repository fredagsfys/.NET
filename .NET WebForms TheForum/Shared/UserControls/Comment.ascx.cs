using System;
using System.Web.UI.WebControls;
using Resources;

public partial class Shared_UserControls_Comment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CommentNewButton_Click(object sender, EventArgs e)
    {
        CommentNewButton.Visible = false;
        CommentListView.InsertItemPosition = InsertItemPosition.LastItem;
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        CommentNewButton.Visible = true;
        CommentListView.InsertItemPosition = InsertItemPosition.None;
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.Comment_Inserting_Error, "Comment");
            e.ExceptionHandled = true;
        }
        else
        {
            Session["StatusMessage"] = null;
            Session["StatusMessage"] = Strings.Comment_Inserted;
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void ObjectDataSource1_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.Comment_Deleting_Error, "Comment");
            e.ExceptionHandled = true;
        }
        else
        {
            Session["StatusMessage"] = null;
            Session["StatusMessage"] = Strings.Comment_Deleted;
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.Comment_Updating_Error, "Comment");
            e.ExceptionHandled = true;
        }
        else
        {
            Session["StatusMessage"] = null;
            Session["StatusMessage"] = Strings.Comment_Updated;
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        var comment = (Comment)e.InputParameters[0];

        comment.ThreadId = int.Parse(Request.QueryString["id"]);
        comment.UserName = Page.User.Identity.Name;

        if (!comment.IsValid)
        {
            Page.AddErrorMessage(comment);
            e.Cancel = true;
        }
    }


}