using System;
using System.Web.Security;
using Resources;

public partial class Account_Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        try
        {
            var service = new Service();
            var user = new FUser
                {
                    Name = RegisterUser.UserName
                };

            if (user.IsValid)
            {
                service.AddFUser(user);
                if (user.FUserId != 0)
                {
                    Session["StatusMessage"] = null;
                    Session["StatusMessage"] = Strings.FUser_Inserted;
                }
            }
            else
            {
                Page.AddErrorMessage(Strings.FUser_Inserting_Error, "RegisterUserValidationGroup");
            }
            Roles.AddUserToRole(RegisterUser.UserName, "Members");
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);
            var continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
        catch
        {
            Page.AddErrorMessage(Strings.FUser_Inserting_Error, "RegisterUserValidationGroup");
        }
    }
}
