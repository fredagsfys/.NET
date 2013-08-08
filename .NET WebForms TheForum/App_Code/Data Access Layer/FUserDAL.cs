using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// This is CRUD class for FUser which handles Insert.
/// Uses stored procedures from SQL Database.
/// </summary>

public class FUserDAL : DALBase
{
    #region Methods
    public int AddFUser(FUser user)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.InsertFUser", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.Name;

                cmd.Parameters.Add("@FUserId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                conn.Open();

                cmd.ExecuteNonQuery();

                return user.FUserId = (int)cmd.Parameters["@FUserId"].Value;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }
    #endregion
}