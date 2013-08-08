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
    ///     The class describes an Anaconda
    /// </summary>
    public class Anaconda : Reptile
    {
        #region Private fields
        /// <summary
        ///     Private field that holds if the teeth are tiny, true or false
        /// </summary>
        private bool m_hasTinyTeeth;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets value to private field m_hasTinyTeeth
        /// </summary>
        public bool HasTinyTeeth
        {
            get { return this.m_hasTinyTeeth; }
            set { this.m_hasTinyTeeth = value; }
        }
        #endregion
        #region Methods
        /// <summary
        ///     Overrideable method which concats ExtraAnimalInfo with diffrent 
        ///     statments depending on inherited properties from Reptile.cs
        /// </summary>
        public override string GetAnimalSpecificData()
        {
            var strInfo = ExtraAnimalInfo;
            var strout = "";

            if (string.IsNullOrEmpty(strInfo))
            {
                strout = string.Empty;
            }
            if (m_hasTinyTeeth)
            {
                strout += String.Format("These theeth are sharp as a blade but very tiny!");
            }
            else if(IsDeadly)
            {
                strout += String.Format("The animal is very deadly, keep out!");
            } 
            strout += Environment.NewLine + SetStinkAndInvasiveData() + "Teeth are tiny, this animal isn't deadly";
            return strout;
        }
        /// <summary
        ///     Overrideable method which sets if the Anaconda is invasive or/and stinks.
        /// </summary>
        /// <returns>String strOut</returns>
        public override string SetStinkAndInvasiveData()
        {
            var strOut = "Doesn't stink but is invasive!";
            return strOut;
        }
        #endregion
    }
}