using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pgAccountDetails : System.Web.UI.Page
{
    // Business layer data field
    clsBusinessLayer myBusinessLayer;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize business layer
        myBusinessLayer = new clsBusinessLayer(Server.MapPath("~/App_Data/"));

        Master.PageTitle.Text = "Account Details";

        // Set the curser to be on the city textbox
        txtCity.Focus();

        try
        {
            if (!IsPostBack)
            {
                txtUserName.Text = (string)Session["username"];
                FindUser(txtUserName.Text);
            }
        }
        catch (Exception error)
        {
            Master.ErrorMessage.Text = error.Message;
        }
    }

    protected void FindUser(string username)
    {
        try
        {
            // Call FindUser in the business layer class to search by username
            dsUserAccount dsFindUserAccount = myBusinessLayer.FindUserAccount(username);
            
            // Populate data into the form
            if (dsFindUserAccount.tblUserAccount.Rows.Count > 0)
            {
                // Using data from the dataset populate data into the form
                txtCity.Text = dsFindUserAccount.tblUserAccount[0].City;
                ddlState.SelectedValue = dsFindUserAccount.tblUserAccount[0].State;
                txtFavorite.Text = dsFindUserAccount.tblUserAccount[0].FavoriteLanguage;
                txtLeastFavorite.Text = dsFindUserAccount.tblUserAccount[0].LeastFavoriteLanguage;

                // Show users applications in grid view
                BindUserApplicationsGridView();

                Master.ErrorMessage.Text = "Record Found";
            }
            else
            {
                // Display a message if no records were found
                Master.ErrorMessage.Text = "No records were found!";
            }

        }
        catch (Exception error)
        {
            // Display the error message if there was an error
            string message = "There was an error = ";
            Master.ErrorMessage.Text = message + error.Message;
        }
    }

    private dsUserApplications BindUserApplicationsGridView()
    {
        // If gridview has records in it clear it
        if (gvApplicationsCompleted.Rows.Count > 0)
        {
            gvApplicationsCompleted.Columns.Clear();
            gvApplicationsCompleted.DataSource = null;
            gvApplicationsCompleted.DataBind();
        }

        if (gvApplicationsCompleted.Rows.Count == 0)
        {
            // Call FindUserApplications in the data layer to get users applications
            dsUserApplications userApplications = myBusinessLayer.FindUserApplications(txtUserName.Text);
            
            // Set datasource for user applications gridview
            gvApplicationsCompleted.DataSource = userApplications.tblUserApplications;

            // Set columns
            BoundField appName = new BoundField();
            appName.DataField = "ApplicationName";
            appName.HeaderText = "Application Name";

            BoundField dateComplete = new BoundField();
            dateComplete.DataField = "DateCompleted";
            dateComplete.HeaderText = "Date Completed";
            dateComplete.DataFormatString = "{0:d}";

            BoundField progLang = new BoundField();
            progLang.DataField = "ProgrammingLanguage";
            progLang.HeaderText = " Programming Language Used";

            gvApplicationsCompleted.Columns.Add(appName);
            gvApplicationsCompleted.Columns.Add(dateComplete);
            gvApplicationsCompleted.Columns.Add(progLang);

            // Bind the data to the grid and insert records
            gvApplicationsCompleted.DataBind();

            txtLastCompletionDate.Text = gvApplicationsCompleted.Rows[0].Cells[1].Text;

            return userApplications;
        }
        else
            return null;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Declare variable for checking if there was an error deleting the user's record
        Tuple<bool, string> message = myBusinessLayer.DeleteUserAccount(txtUserName.Text);

        // Set the user message and bool value if there was an error
        bool userDeleteError = message.Item1;
        Master.ErrorMessage.Text = message.Item2;

        if (userDeleteError == false)
        {
            // Clear fields in form
            ClearInputs(Page.Controls);
        }
    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();
            else if (ctrl is GridView)
            {
                gvApplicationsCompleted.Columns.Clear();
                gvApplicationsCompleted.DataSource = null;
                gvApplicationsCompleted.DataBind();
            }
            else
                ClearInputs(ctrl.Controls);
        }
    }

    protected void btnExportStats_Click(object sender, EventArgs e)
    {
        // Get message and call write xml file in business layer
        string message = myBusinessLayer.WriteApplicationsXMLFile();
        Master.ErrorMessage.Text = message;
    }

    public TextBox Username
    { get { return txtUserName; } }

    public TextBox City
    { get { return txtCity; } }

    public DropDownList State
    { get { return ddlState; } }

    public TextBox Favorite
    { get { return txtFavorite; } }

    public TextBox LeastFavorite
    { get { return txtLeastFavorite; } }

    public TextBox LastCompletionDate
    { get { return txtLastCompletionDate; } }
}