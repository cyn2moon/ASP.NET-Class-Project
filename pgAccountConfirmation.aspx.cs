using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class pgAccountConfirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.PageTitle.Text = "Account Confirmation";

        try
        {
            if (!IsPostBack)
            {
                // Fill in data entered on Account Details screen
                lblUserName.Text = PreviousPage.Username.Text;
                lblCity.Text = PreviousPage.City.Text;
                lblState.Text = PreviousPage.State.Text;
                lblFavorite.Text = PreviousPage.Favorite.Text;
                lblLeastFavorite.Text = PreviousPage.LeastFavorite.Text;
                lblLastCompletionDate.Text = PreviousPage.LastCompletionDate.Text;
            }
                
            // Get user applications to display on form
            dsUserApplications userApplications = GetUserApplications(lblUserName.Text);
                
            // Insert a data table using the information from the dataset
            DataTable userApplicationTable = userApplications.Tables[0];

            // Create the table and populate data
            for (int i=0; i < userApplicationTable.Rows.Count; i++)
            {
                DataRow currentDataRow = userApplicationTable.Rows[i];
                    
                TableRow newRow = new TableRow();

                for (int r = 2; r < currentDataRow.Table.Columns.Count; r++)
                {
                    // Create a Cell
                    TableCell newCell = new TableCell();

                    // Add the text from the DataRow
                    newCell.Text = currentDataRow[r].ToString();

                    if (r == 3)
                    {
                        if (newCell.Text != "")
                        {
                            string dateComplete = newCell.Text;
                            DateTime dt;
                            dt = Convert.ToDateTime(dateComplete);
                            newCell.Text = (dt.Month + "/" + dt.Day + "/" + dt.Year);
                        }
                    }

                    if (newCell.Text == "")
                    {
                        newCell.Text = " ";
                    }

                    // Add the cell to the row
                    newRow.Cells.Add(newCell);
                    }
                    
                    // Add the row to the table:
                    this.userApplicationTable.Rows.Add(newRow);
                }
                
                // Create a header for the table
                TableHeaderRow headerRow = new TableHeaderRow();

                headerRow.BackColor = Color.Black;
                headerRow.ForeColor = Color.White;

                // Create cells that contain 
                // the application name, date, and programming language header.
                TableHeaderCell headerApplication = new TableHeaderCell();
                TableHeaderCell headerDate = new TableHeaderCell();
                TableHeaderCell headerLanguage = new TableHeaderCell();

                headerApplication.Text = "Application Name";
                headerDate.Text = "Completion Date";
                headerLanguage.Text = "Programming Language Used";
               

                // Add the rows to the table
                headerRow.Cells.Add(headerApplication);
                headerRow.Cells.Add(headerDate);
                headerRow.Cells.Add(headerLanguage);

                // Add rows at the top
                this.userApplicationTable.Rows.AddAt(0, headerRow);
        }
        catch (Exception error)
        {
            Master.ErrorMessage.Text = error.Message;
        }
    }

    private dsUserApplications GetUserApplications(string username)
    {
        // Pass the location of the database to the data layer
        string tempPath = Server.MapPath("~/App_Data/ProgramaholicsAnonymous.mdb");
        clsDataLayer myDataLayer = new clsDataLayer(tempPath);

        // Call FindUserApplications in the data layer to get users applications
        dsUserApplications userApplications = myDataLayer.FindUserApplications(username);
        Cache.Insert("ApplicationsDataSet", userApplications);
        return userApplications;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        // Instantiate the dataset
        dsUserAccount dsFindUserAccount;

        // Pass the location of the database to the data layer
        string tempPath = Server.MapPath("~/App_Data/ProgramaholicsAnonymous.mdb");
        clsDataLayer dataLayerObj = new clsDataLayer(tempPath);

        try
        {
            // Call FindCustomer in the data layer class to search for username
            dsFindUserAccount = dataLayerObj.FindUserAccount(lblUserName.Text);

            // Populate data into the form
            if (dsFindUserAccount.tblUserAccount.Rows.Count > 0)
            {
                // Declare variable for checking if there was an error updating the user's record
                bool userUpdateError = false;

                // Pass the location of the database to the data layer
                clsDataLayer myDataLayer = new clsDataLayer(tempPath);

                // Pass values to update user in data layer class
                // Try to update record, send message if error occurs
                try
                {
                    myDataLayer.UpdateUser(lblUserName.Text, lblCity.Text, lblState.Text, lblFavorite.Text,
                        lblLeastFavorite.Text);
                }
                catch (Exception error)
                {
                    // If an error was caught display it to the user
                    userUpdateError = true;
                    string message = "Error updating customer. ";
                    Master.ErrorMessage.Text = message + error.Message;
                }

                if (!userUpdateError)
                {
                    Master.ErrorMessage.Text = "User Updated Successfully.";
                }
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

        btnGoBack.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["username"] = lblUserName.Text;

        // If credentials are valid direct user to account details page
        Response.Redirect("~/pgAccountDetails.aspx");
    }


    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/pgAccountDetails.aspx");
    }
}