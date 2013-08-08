using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// This is CRUD class for Thread which handles Read, Insert, Update, Delete.
/// Uses stored procedures from SQL Database.
/// </summary>

public class ThreadDAL : DALBase
{
    #region Metoder

    public List<Thread> GetThreads()
    {
        var thread = new List<Thread>(100);

        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.GetThreads", conn) { CommandType = CommandType.StoredProcedure };

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var threadIdIndex = reader.GetOrdinal("ThreadId");
                    var threadNameIndex = reader.GetOrdinal("ThreadName");
                    var userNameIndex = reader.GetOrdinal("UserName");
                    var commentCountIndex = reader.GetOrdinal("CommentCount");
                    var viewCountIndex = reader.GetOrdinal("ViewCount");

                    while (reader.Read())
                    {
                        thread.Add(new Thread
                            {
                                ThreadId = reader.GetInt32(threadIdIndex),
                                ThreadName = reader.GetString(threadNameIndex),
                                UserName = reader.GetString(userNameIndex),
                                CommentCount = reader.GetInt32(commentCountIndex),
                                ViewCount = reader.GetInt32(viewCountIndex)

                            });
                    }
                }
                thread.TrimExcess();

                return thread;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public Thread GetThread(int threadId)
    {
        var thread = new Thread();

        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.GetThread", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Value = threadId;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var threadIdIndex = reader.GetOrdinal("ThreadId");
                    var threadNameIndex = reader.GetOrdinal("ThreadName");
                    var threadContentIndex = reader.GetOrdinal("ThreadContent");
                    var dateIndex = reader.GetOrdinal("Date");
                    var commentCountIndex = reader.GetOrdinal("CommentCount");
                    var userNameIndex = reader.GetOrdinal("UserName");
                    var userRoleIndex = reader.GetOrdinal("Role");

                    while (reader.Read())
                    {
                        thread.ThreadId = reader.GetInt32(threadIdIndex);
                        thread.ThreadName = reader.GetString(threadNameIndex);
                        thread.ThreadContent = reader.GetString(threadContentIndex);
                        thread.Date = reader.GetDateTime(dateIndex);
                        thread.CommentCount = reader.GetInt32(commentCountIndex);
                        thread.UserName = reader.GetString(userNameIndex);
                        thread.UserRole = reader.GetString(userRoleIndex);
                    }
                }
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }

        return thread;
    }

    public int AddThread(Thread thread)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.InsertThread", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadName", SqlDbType.VarChar, 50).Value = thread.ThreadName;
                cmd.Parameters.Add("@ThreadContent", SqlDbType.VarChar, 1000).Value = thread.ThreadContent;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = thread.UserName;

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                conn.Open();

                cmd.ExecuteNonQuery();

                return thread.ThreadId = (int) cmd.Parameters["@ThreadId"].Value;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public void DeleteThread(int threadId)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.DeleteThread", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Value = threadId;

                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public void UpdateThread(Thread thread)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.UpdateThread", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Value = thread.ThreadId;
                cmd.Parameters.Add("@ThreadName", SqlDbType.VarChar, 50).Value = thread.ThreadName;
                cmd.Parameters.Add("@ThreadContent", SqlDbType.VarChar, 1000).Value = thread.ThreadContent;

                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    #endregion
}