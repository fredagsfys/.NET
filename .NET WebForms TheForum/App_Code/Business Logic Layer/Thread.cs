using System;
using System.ComponentModel;
using Resources;

/// <summary>
/// This class is a model for Thread. It has validation on properties that
/// can be manipulated by application user.
/// </summary>

[Serializable]
public class Thread : BusinessObjectBase
{
    #region Field

    private string _threadName;
    private string _threadContent;

    #endregion

    #region Properties, Constructor

    public Thread()
    {
        this.ThreadName = null;
        this.ThreadContent = null;
    }

    public int ThreadId { get; set; }
    public int CommentCount { get; set; }
    public int ViewCount { get; set; }
    public DateTime Date { get; set; }
    public string UserRole { get; set; }
    public string UserName { get; set; }

    public string ThreadName
    {
        get { return this._threadName; }
        set
        {
            this.ValidationErrors.Remove("ThreadName");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("ThreadName", Strings.Thread_ThreadName_Required);
            }
            else if (value.Length > 50)
            {
                this.ValidationErrors.Add("ThreadName", Strings.Thread_ThreadName_MaxLength);
            }

            this._threadName = value != null ? value.Trim() : null;
        }
    }

    public string ThreadContent
    {
        get { return this._threadContent; }
        set
        {
            this.ValidationErrors.Remove("ThreadContent");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("ThreadContent", Strings.Thread_ThreadContent_Required);
            }
            else if (value.Length > 1000)
            {
                this.ValidationErrors.Add("ThreadContent", Strings.Thread_ThreadContent_MaxLength);
            }

            this._threadContent = value != null ? value.Trim() : null;
        }
    }

    #endregion
}