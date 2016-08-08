using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pgLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.ErrorMessage.Text = "Please enter username and password.";

        // Set focus to be on username textbox
        txtUsername.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // Set the path to the business layer
        clsBusinessLayer myBusinessLayer = new clsBusinessLayer(Server.MapPath("~/App_Data/"));

        // Pass user credentials to business layer for verification
        bool isValidUser = myBusinessLayer.CheckUserCredentials(Session, txtUsername.Text, txtPassword.Text);

        bool lockedSession = Convert.ToBoolean(Session["LockedSession"]);
        int attemptCount = Convert.ToInt32(Session["AttemptCount"]);

        // Check if credentials are valid
        if (isValidUser)
        {
            Session["username"] = txtUsername.Text;
            
            // If credentials are valid direct user to account details page
            Response.Redirect("~/pgAccountDetails.aspx");
        }
        else if (Convert.ToBoolean(Session["LockedSession"]))
        {
            Master.ErrorMessage.Text = "Account is disabled. Contact System Administrator";

            // Hide login button
            btnLogin.Visible = false;
        }
        else
        {
            // Set error message on master page
            Master.ErrorMessage.Text = "The User ID and/or Password supplied is incorrect. Please try again!";
        }
    }

    public TextBox LoginUsername
    { get { return txtUsername; } }
}