//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1CS
{
    /// <summary
    ///     This class inherits from Canine, and through Canine from Animal.
    ///     The class describes an Cat
    /// </summary>
    public class Cat : Canine
    {
#region Private fields
        /// <summary
        ///     private field that holds the Agile properties value, true or false.
        /// </summary>
        private bool m_agile;
#endregion
#region Properties
        /// <summary
        ///     Property that sets the value for Agile into m_agile, true or false 
        /// </summary>
        public bool Agile
        {
            get { return this.m_agile; }
            set { this.m_agile = value; }
        }
        #endregion
#region Methods
        /// <summary
        ///     Overrideable method which concats ExtraAnimalInfo with diffrent 
        ///     statments depending on inherited properties from Canine.cs
        /// </summary>
        public override string GetAnimalSpecificData()
        {
            var strInfo = ExtraAnimalInfo;
            var strout = "";

            if (string.IsNullOrEmpty(strInfo))
            {
                strout = string.Empty;
            }
            if (IsHungry)
            {
                strout += String.Format("This cat is hungry!");
            }
            else if (m_agile)
            {
                strout += String.Format("This cat is agile and though!");
            }
            strout += Environment.NewLine + SetStinkAndInvasiveData() + "This cat is clumsy and lazy";

            return strout;
        }
        /// <summary
        ///     Overrideable method which sets if the cat is invasive or/and stinks.
        /// </summary>
        /// <returns>String strOut</returns>
        public override string SetStinkAndInvasiveData()
        {
            var strOut = "Cats are really invasive!";
            return strOut;
        }
        #endregion
    }
}
