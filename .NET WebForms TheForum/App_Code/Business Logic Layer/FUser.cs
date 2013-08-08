using System;

/// <summary>
/// This class is a model for FUser.
/// </summary>

[Serializable]
public class FUser : BusinessObjectBase
{
	public FUser()
	{
	    this.Name = null;
	}

    public int FUserId { get; set; }
    public string Name { get; set; }
          
}