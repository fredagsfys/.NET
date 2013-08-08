using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class Members_Threads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            Response.Redirect("~/Error.aspx", false);
        }
    }

    protected void ThreadDetailsObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.Thread_Deleting_Error, "Thread");
            e.ExceptionHandled = true;
        }
        else
        {
            Session["StatusMessage"] = null;
            Session["StatusMessage"] = Strings.Thread_Deleted;
            Response.Redirect("~/Members/Forum.aspx");
        }
    }

    protected void ThreadDetailsObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            Page.AddErrorMessage(Strings.Thread_Updating_Error, "Thread" );
            e.ExceptionHandled = true;
        }
        else
        {
            Session["StatusMessage"] = null;
            Session["StatusMessage"] = Strings.Thread_Updated;
            Response.Redirect(Request.RawUrl);
        }
    }
}