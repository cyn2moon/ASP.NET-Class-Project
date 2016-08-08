using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.OleDb;
using System.Net;

/// <summary>
/// Summary description for clsDataLayer
/// </summary>
public class clsDataLayer
{
    OleDbConnection dbConnection;
    
    public clsDataLayer()
	{}

    public clsDataLayer(string Path)
    {
        dbConnection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path);
    }

    public dsUserAccount FindUserAccount(string username)
    {
        // Create the sql statement to select the record that has the same username
        // Instantiate the data adapter
        string sqlStmt = "SELECT * FROM tblUserAccount where Username like '" + username + "'";
        OleDbDataAdapter sqlDataAdapter = new OleDbDataAdapter(sqlStmt, dbConnection);

        // Instantiate the data set
        // Fill the data set with the results from the query
        dsUserAccount myStoreDataSet = new dsUserAccount();
        sqlDataAdapter.Fill(myStoreDataSet.tblUserAccount);

        // Return the data set
        return myStoreDataSet;
    }

    public dsUserApplications FindUserApplications(string username)
    {
        dbConnection.Open();
        
        // Create the sql statement to select the record that has the same username
        // Instantiate the data adapter
        string sqlStmt = "SELECT * FROM tblUserApplications WHERE Username = '" +
                    username + "'" +
                    " ORDER BY DateCompleted DESC"; 
        OleDbDataAdapter sqlDataAdapter = new OleDbDataAdapter(sqlStmt, dbConnection);
        
        // Instantiate the data set
        // Fill the data set with the results from the query
        dsUserApplications myStoreDataSet = new dsUserApplications();
        sqlDataAdapter.Fill(myStoreDataSet.tblUserApplications);

        // Return the data set
        return myStoreDataSet;
    }

    public void UpdateUser(string username, string city, string state, string favoriteLanguage,
                        string leastFavoriteLanguage)
    {
        // Open connection to accounts database
        dbConnection.Open();

        // Create SQL statement to update all fields in the customer table using command parameters
        string sqlStmt = "UPDATE tblUserAccount SET City = @city, " +
                    "State = @state, " +
                    "FavoriteLanguage = @favoriteLanguage, " +
                    "LeastFavoriteLanguage = @leastFavoriteLanguage " +
                    "WHERE (tblUserAccount.Username = @username)";

        // Instantiate the database command
        OleDbCommand dbCommand = new OleDbCommand(sqlStmt, dbConnection);

        // Create database parameter
        // Add city to the database command
        OleDbParameter param = new OleDbParameter("@city", city);
        dbCommand.Parameters.Add(param);

        // Add the rest of the values to the database command
        dbCommand.Parameters.Add(new OleDbParameter("@state", state));
        dbCommand.Parameters.Add(new OleDbParameter("@favoriteLanguage", favoriteLanguage));
        dbCommand.Parameters.Add(new OleDbParameter("@leastFavoriteLanguage", leastFavoriteLanguage));
        dbCommand.Parameters.Add(new OleDbParameter("@username", username));

        // Execute the update
        dbCommand.ExecuteNonQuery();

        // Close the connection to the database
        dbConnection.Close();
    }

    public void DeleteUserAccount(string username)
    {
        // Open connection to accounts database
        dbConnection.Open();

        // Create SQL statement to delete all fields in the customer table using command parameters
        string sqlAccountStmt = "DELETE * FROM tblUserAccount " +
                    "WHERE (tblUserAccount.Username = @username)";

        string sqlApplicationStmt = "DELETE * FROM tblUserApplications " +
                    "WHERE (tblUserApplications.Username = @username)";

        // Instantiate the database command
        OleDbCommand dbAccountCommand = new OleDbCommand(sqlAccountStmt, dbConnection);
        OleDbCommand dbApplicationCommand = new OleDbCommand(sqlApplicationStmt, dbConnection);

        // Create database parameter
        // Add city to the database command
        dbAccountCommand.Parameters.Add(new OleDbParameter("@username", username));
        dbApplicationCommand.Parameters.Add(new OleDbParameter("@username", username));

        // Execute the update
        dbAccountCommand.ExecuteNonQuery();
        dbApplicationCommand.ExecuteNonQuery();

        // Close the connection to the database
        dbConnection.Close();
    }

    public dsUserApplications GetAllApplications()
    {
        //  Set the sql statment getting all applications
        OleDbDataAdapter sqlDataAdapter = new OleDbDataAdapter("SELECT ApplicationName,ProgrammingLanguage FROM tblUserApplications;", dbConnection);

        // Fill data set
        dsUserApplications myAppDataSet = new dsUserApplications();
        sqlDataAdapter.Fill(myAppDataSet.tblUserApplications);

        // Return the data set
        return myAppDataSet;
    }

    public bool ValidateUser(string username, string password)
    {
        // Open the connection to the database
        dbConnection.Open();

        // Create SQL statement to select user record where username and password match
        string sqlStmt = "SELECT * FROM tblUserLogin WHERE Username = @username AND Password = @password AND Locked = FALSE";

        // Instantiate the database command
        OleDbCommand dbCommand = new OleDbCommand(sqlStmt, dbConnection);

        // Create database parameter
        // Add username and password to the database command
        dbCommand.Parameters.Add(new OleDbParameter("@username", username));
        dbCommand.Parameters.Add(new OleDbParameter("@password", password));

        // Read record to check for matching credentials 
        OleDbDataReader dr = dbCommand.ExecuteReader();

        // Set isValidAccount true or false depending on credentials check
        Boolean isValidAccount = dr.Read();

        // Close the connection to the database
        dbConnection.Close();

        return isValidAccount;
    }

    public void LockUserAccount(string username)
    {
        // Open the connection to the database
        dbConnection.Open();

        // Create SQL statement to update user record where username matches and set locked field
        string sqlStmt = "UPDATE tblUserLogin SET Locked = True WHERE Username = @username";

        // Instantiate the database command
        OleDbCommand dbCommand = new OleDbCommand(sqlStmt, dbConnection);

        // Create database parameter
        // Add username to the database command
        dbCommand.Parameters.Add(new OleDbParameter("@username", username));

        // Execute the update
        dbCommand.ExecuteNonQuery();

        // Close the connection to the database
        dbConnection.Close();
    }
}