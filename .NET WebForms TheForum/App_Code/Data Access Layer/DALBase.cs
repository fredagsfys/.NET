using System.Data.SqlClient;
using System.Web.Configuration;
using System.ComponentModel;
using Resources;

/// <summary>
/// This class handles the connection to the database, holds a property for a connection to the database and
/// another for a Generic Error Message which is used in the CRUD classes when throwing exception.
/// </summary>

[DataObject(false)]
public abstract class DALBase
{
    #region Fields

    private static string _connectionString;

    protected static readonly string GenericErrorMessage = Strings.Generic_DAL_Error_Message;

    #endregion

    #region Methods

    protected static SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    static DALBase()
    {
        _connectionString = WebConfigurationManager.ConnectionStrings["ForumConnectionString"].ConnectionString;
    }

    #endregion
}