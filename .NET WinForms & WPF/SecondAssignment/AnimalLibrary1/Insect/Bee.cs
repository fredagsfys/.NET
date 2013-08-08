//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimalMotel.Insects;

/// <summary
///     This class inherits from Insect, and through Insect from Animal.
///     The class describes an Bee
public class Bee : Insect
{
    #region Private fields
    /// <summary
    ///     Private fields, holds count of flowersVisited and if its an HoneyBee, true or false
    /// </summary>
    private int m_flowersVisited;
    private bool m_isHoneyBee;
    #endregion
    #region Properties
    /// <summary
    ///     Properties that return the value to the private fields 
    /// </summary>
    public int flowersVisted
    {
        get { return this.m_flowersVisited; }
        set { this.m_flowersVisited = value; }
    }
    public bool IsHoneyBee
    {
        get { return m_isHoneyBee; }
        set { m_isHoneyBee = value; }
    }
    #endregion
    #region Methods
    /// <summary
    ///     Overrideable method which concats ExtraAnimalInfo with diffrent 
    ///     statments depending on inherited properties from Insect.cs
    /// </summary>
    public override string GetAnimalSpecificData()
    {
        var strInfo = ExtraAnimalInfo;
        var strout = "";
        if (string.IsNullOrEmpty(strInfo))
        {
            strout = string.Empty;
        }
        if (IsHoneyBee)
        {
            strout += string.Format("Has visited {0} flowers!", m_flowersVisited);
        }
        else if(IsPoisonous)
        {
            strout += String.Format("This bee type is poisonous.");
        }
        strout += Environment.NewLine + SetStinkAndInvasiveData() + "This bee type is not poisonous.";

        return strout;
    }
    /// <summary
    ///     Overrideable method which sets if the Bee is invasive or/and stinks.
    /// </summary>
    /// <returns>String strOut</returns>
    public override string SetStinkAndInvasiveData()
    {
        string strOut = "Not stinky but it can be invasive when bothered!";
        return strOut;
    }
    #endregion

}
