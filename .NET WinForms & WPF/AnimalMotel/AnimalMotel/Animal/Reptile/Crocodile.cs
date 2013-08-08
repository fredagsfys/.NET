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
    ///     This class inherits from Reptile, and through Reptile from Animal.
    ///     The class describes an Crocodile
    /// </summary>
    public class Crocodile : Reptile
    {
        #region Private fields
        /// <summary
        ///     Private field that hold if Crocdile has sharpTeeth, true or false
        /// </summary>
        private bool m_hasSharpTeeth;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets the value for HasSharpTeeth into m_hasSharpTeeth, true or false 
        /// </summary>
        public bool HasSharpTeeth
        {
            get { return this.m_hasSharpTeeth; }
            set { this.m_hasSharpTeeth = value; }
        }
        #endregion
        #region Method
        /// <summary
        ///     Overrideable method which concats ExtraAnimalInfo with diffrent 
        ///     statments depending on inherited properties from Crocdile.cs
        /// </summary>
        public override string GetAnimalSpecificData()
        {
            var strInfo = ExtraAnimalInfo;
            var strout = "";

            if (string.IsNullOrEmpty(strInfo))
            {
                strout = string.Empty;
            }
            if (m_hasSharpTeeth)
            {
                strout += String.Format("Teeth are sharp, many too!");
            }
            else if (IsDeadly)
            {
                strout += String.Format("This animal only looks for a kill");
            }
            strout += Environment.NewLine + SetStinkAndInvasiveData() + "Teeth are tiny, this animal isn't deadly";
            return strout;
        }
        /// <summary
        ///     Overrideable method which sets if the crocodile is invasive or/and stinks.
        /// </summary>
        /// <returns>String strOut</returns>
        public override string SetStinkAndInvasiveData()
        {
            var strOut = "Stink and is invasive!";
            return strOut;
        }
        #endregion
    }
}