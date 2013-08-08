using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// Service class which takes care of the interactions between DAL and BLL.
/// </summary>

[DataObject(true)]
public class Service
{
    #region Fields

    private ThreadDAL _threadDal;
    private CommentDAL _commentDAL;
    private FUserDAL _fUserDAL;

    #endregion

    #region Properties

    public ThreadDAL ThreadDAL
    {
        get { return this._threadDal ?? (this._threadDal = new ThreadDAL()); }
    }

    public CommentDAL CommentDAL
    {
        get { return this._commentDAL ?? (this._commentDAL = new CommentDAL()); }
    }

    public FUserDAL FUserDAL
    {
        get { return this._fUserDAL ?? (this._fUserDAL = new FUserDAL()); }
    }

    #endregion

    #region Methods

    #region Thread
    public List<Thread> GetThreads()
    {
        return ThreadDAL.GetThreads();
    }

    public Thread GetThread(int threadId)
    {
        return ThreadDAL.GetThread(threadId);
    }

    public void DeleteThread(Thread thread)
    {
        ThreadDAL.DeleteThread(thread.ThreadId);
    }

    public void SaveThread(Thread thread)
    {
        if (thread.IsValid)
        {
            if (thread.ThreadId == 0)
            {
                ThreadDAL.AddThread(thread);
            }
            else
            {
                ThreadDAL.UpdateThread(thread);
            }
        }
        else
        {
            var ex = new ApplicationException(thread.Error);
            ex.Data.Add("Thread", thread);
            throw ex;
        }
    }
    #endregion
    #region Comment
    public List<Comment> GetComments(int threadId)
    {
        return CommentDAL.GetComments(threadId);
    }

    public void SaveComment(Comment comment)
    {
        if (comment.IsValid)
        {
            if (comment.CommentId == 0)
            {
                CommentDAL.AddComment(comment);
            }
            else
            {
                CommentDAL.UpdateComment(comment);
            }
        }
        else
        {
            var ex = new ApplicationException(comment.Error);
            ex.Data.Add("Comment", comment);
            throw ex;
        }
    }

    public int AddComment(Comment comment)
    {
        if (comment.IsValid)
        {
            return CommentDAL.AddComment(comment);
        }
        throw new ApplicationException("Error in service");
    }

    public void DeleteComment(Comment comment)
    {
        CommentDAL.DeleteComment(comment.CommentId);
    }

    public void UpdateComment(Comment comment)
    {
        if (comment.IsValid)
        {
            CommentDAL.UpdateComment(comment);
        }
        else
        {
            throw new ApplicationException("Error in service");
        }
    }
    #endregion
    public int AddFUser(FUser user)
    {
        return FUserDAL.AddFUser(user);
    }

    #endregion
}