using System;
using System.Web.UI.WebControls;
using Resources;

public partial class Members_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateSubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            var service = new Service();
            var thread = new Thread
                {
                    ThreadName = ThreadNameTextBox.Text,
                    ThreadContent = ThreadContentTextBox.Text,
                    UserName = Page.User.Identity.Name
                };

            if (thread.IsValid)
            {
                service.SaveThread(thread);
                if (thread.ThreadId != 0)
                {
                    Session["StatusMessage"] = null;
                    Session["StatusMessage"] = Strings.Thread_Inserted;

                    Response.Redirect(String.Format("~/Members/Threads.aspx?id={0}", thread.ThreadId), false);
                }
            }
            else
            {
                Page.AddErrorMessage(Strings.Thread_Inserting_Error, "CreateThread");
            }
        }
        catch
        {
            Page.AddErrorMessage(Strings.Thread_Inserting_Error, "CreateThread");
        }
    }
}