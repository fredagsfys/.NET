using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// This is CRUD class for Comment which handles Read, Insert, Update, Delete.
/// Uses stored procedures from SQL Database.
/// </summary>

public class CommentDAL : DALBase
{
    #region Methods

    public List<Comment> GetComments(int threadId)
    {
        var comments = new List<Comment>(100);

        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.GetComments", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Value = threadId;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var commentIdIndex = reader.GetOrdinal("CommentId");
                    var threadIdIndex = reader.GetOrdinal("ThreadId");
                    var dateIndex = reader.GetOrdinal("Date");
                    var commentContentIndex = reader.GetOrdinal("CommentContent");
                    var threadNameIndex = reader.GetOrdinal("ThreadName");
                    var userNameIndex = reader.GetOrdinal("UserName");
                    var roleIndex = reader.GetOrdinal("Role");

                    while (reader.Read())
                    {
                        comments.Add(new Comment
                            {
                                CommentId = reader.GetInt32(commentIdIndex),
                                ThreadId = reader.GetInt32(threadIdIndex),
                                Date = reader.GetDateTime(dateIndex),
                                CommentContent = reader.GetString(commentContentIndex),
                                ThreadName = reader.GetString(threadNameIndex),
                                UserName = reader.GetString(userNameIndex),
                                Role = reader.GetString(roleIndex)
                            });
                    }
                }

                comments.TrimExcess();

                return comments;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public int AddComment(Comment comment)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.InsertComment", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@ThreadId", SqlDbType.Int, 4).Value = comment.ThreadId;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = comment.UserName;
                cmd.Parameters.Add("@CommentContent", SqlDbType.VarChar, 350).Value = comment.CommentContent;

                cmd.Parameters.Add("@CommentId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                conn.Open();

                cmd.ExecuteNonQuery();

                comment.CommentId = (int)cmd.Parameters["@CommentId"].Value;

                return comment.CommentId;
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public void DeleteComment(int commentId)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.DeleteComment", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@CommentId", SqlDbType.Int, 4).Value = commentId;

                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new ApplicationException(GenericErrorMessage);
            }
        }
    }

    public void UpdateComment(Comment comment)
    {
        using (var conn = CreateConnection())
        {
            try
            {
                var cmd = new SqlCommand("appSchema.UpdateComment", conn) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add("@CommentId", SqlDbType.Int, 4).Value = comment.CommentId;
                cmd.Parameters.Add("@CommentContent", SqlDbType.VarChar, 350).Value = comment.CommentContent;

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