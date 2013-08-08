using System;
using Resources;

/// <summary>
/// This class is a model for Comment. It has validation on properties that
/// can be manipulated by application user.
/// </summary>

[Serializable]
public class Comment : BusinessObjectBase
{
    #region Fields

    private string _commentContent;

    #endregion

    #region Properties, Constructor

    public Comment()
    {
        this.CommentContent = null;
    }

    public int CommentId { get; set; }
    public string ThreadName { get; set; }
    public int ThreadId { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
    public DateTime Date { get; set; }

    public string CommentContent
    {
        get { return this._commentContent; }
        set
        {
            this.ValidationErrors.Remove("CommentContent");

            if (String.IsNullOrWhiteSpace(value))
            {
                this.ValidationErrors.Add("CommentContent", Strings.Comment_CommentContent_Required);
            }

            else if (value.Length > 500)
            {
                this.ValidationErrors.Add("CommentContent", Strings.Comment_CommentContent_MaxLength);
            }

            this._commentContent = value != null ? value.Trim() : null;
        }
    }

    #endregion
}
    