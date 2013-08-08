using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var messageLabel = (Label)FindControl("MessageLabel");
        if (!string.IsNullOrEmpty((string)Session["StatusMessage"]))
        {
            var message = (string)Session["StatusMessage"];
            // Clear the session variable
            Session["StatusMessage"] = null;
            // Enable some control to display the message (control is likely on the master page)            
            messageLabel.Visible = true;
            messageLabel.Text = message;
        }
        else
        {
            messageLabel = (Label)FindControl("MessageLabel");
            messageLabel.Visible = false;
        }

    }
}
