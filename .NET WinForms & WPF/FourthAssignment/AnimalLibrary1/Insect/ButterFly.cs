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
///     The class describes an Butterfly
/// </summary>
    [Serializable]
    public class Butterfly : Insect
    {
        #region Private fields
        /// <summary
        ///     Private fields, holds if its in LarvalPhase, true or false
        /// </summary>
        private bool m_larvalPhase;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets value to private field m_larvalPhase
        /// </summary>
        public bool LarvalPhase
        {
            get { return this.m_larvalPhase; }
            set { this.m_larvalPhase = value; }
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
            if (IsPoisonous)
            {
                strout += String.Format("This butterfly is in the larval phase.");
            }
            strout += Environment.NewLine + SetStinkAndInvasiveData() + "This bee is not in the larval phase";

            return strout;
        }
        /// <summary
        ///     Overrideable method which sets if the Butterfly is invasive or/and stinks.
        /// </summary>
        /// <returns>String strOut</returns>
        public override string SetStinkAndInvasiveData()
        {
            string strOut = "Butterflies are beautiful.  They do not stink or get aggressive!";
            return strOut;
        }
        #endregion
    }

