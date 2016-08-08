using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for clsBusinessLayer
/// </summary>
public class clsBusinessLayer
{
	// Declare variable to hold path
    string dataPath;

    // Declare variable to hold instance of the data layer
    clsDataLayer myDataLayer;
    
    public clsBusinessLayer()
	{}

    public clsBusinessLayer(string serverMappedPath)
    {
        // Instantiate the data path and the data layer
        dataPath = serverMappedPath;
        myDataLayer = new clsDataLayer(dataPath + "ProgramaholicsAnonymous.mdb");
    }

    public dsUserAccount FindUserAccount(string username)
    {
        // Call the find user account method in the data layer
        dsUserAccount dsFoundUser = myDataLayer.FindUserAccount(username);

        // If any of the fields are null set the string to empty
        if (dsFoundUser.tblUserAccount.Rows.Count > 0)
        {
            // Set the user record to the first record in the table
            System.Data.DataRow userRecord = dsFoundUser.tblUserAccount.Rows[0];

            if (userRecord["City"] == DBNull.Value)
                userRecord["City"] = string.Empty;

            if (userRecord["State"] == DBNull.Value)
                userRecord["State"] = string.Empty;

            if (userRecord["FavoriteLanguage"] == DBNull.Value)
                userRecord["FavoriteLanguage"] = string.Empty;

            if (userRecord["LeastFavoriteLanguage"] == DBNull.Value)
                userRecord["LeastFavoriteLanguage"] = string.Empty;
        }

        return dsFoundUser;
    }

    public dsUserApplications FindUserApplications(string username)
    {
        // Call find user applications sending username in the data layer
        dsUserApplications dsFoundApplications = myDataLayer.FindUserApplications(username);
        int rowCount = dsFoundApplications.tblUserApplications.Rows.Count;
        
        // For every record found check for null values
        if (dsFoundApplications.tblUserApplications.Rows.Count > 0)
        {
            for (int i = 0; i < rowCount; i++)
            {
                System.Data.DataRow userApplications = dsFoundApplications.tblUserApplications.Rows[i];

                if (userApplications["ApplicationName"] == DBNull.Value)
                    userApplications["ApplicationName"] = string.Empty;

                if (userApplications["ProgrammingLanguage"] == DBNull.Value)
                    userApplications["ProgrammingLanguage"] = string.Empty;
            }
        }
        else
            dsFoundApplications = null;

        return dsFoundApplications;
    }

    public Tuple<bool, string> DeleteUserAccount(string username)
    {
        // Declare variable for checking if there was an error deleting the user's record
        bool userDeleteError = false;

        string resultMessage = "User Deleted Successfully.";

        // Pass username to delete user in data layer class
        // Try to delete record, send message if error occurs
        try
        {
            myDataLayer.DeleteUserAccount(username);
        }
        catch (Exception error)
        {
            // If an error was caught display it to the user
            userDeleteError = true;
            resultMessage = "Error deleting customer. " + error;
        }

        // Return error message and bool if there was an error
        return new Tuple<bool, string>(userDeleteError, resultMessage);
    }

    public string WriteApplicationsXMLFile()
    {
        // Instantiate xml dataset
        DataSet xmlDataSet = new DataSet();
        string message = "XML Data Successfully Saved to " + (dataPath + "username.xml");
        
        // Call get all applications from the data layer
        dsUserApplications userApplications = myDataLayer.GetAllApplications();

        try
        {
            userApplications.tblUserApplications.WriteXml(dataPath + "username.xml");
        }
        catch (Exception error)
        {
            message = "XML Data Was Not Succussfully Saved. " + error;
        }

        return message;
    }

    public bool CheckUserCredentials(System.Web.SessionState.HttpSessionState currentSession,
                               string username, string password)
    {
        bool isValid = myDataLayer.ValidateUser(username, password);

        // Set the users session not locked
        currentSession["LockedSession"] = false;

        // Track the number of total fail login attemps in that session
        int totalAttempts = Convert.ToInt32(currentSession["AttemptCount"]) + 1;
        currentSession["AttemptCount"] = totalAttempts;

        // Track the number of password attemps in the current session
        int userAttempts = Convert.ToInt32(currentSession[username]) + 1;
        currentSession[username] = userAttempts;

        // If the user attemps to log in more than 3 times then they are locked
        // out of being logged in
        if ((userAttempts >= 3) || (totalAttempts >= 6))
        {
            currentSession["LockedSession"] = true;
            myDataLayer.LockUserAccount(username);
        }

        return isValid;
    }
}