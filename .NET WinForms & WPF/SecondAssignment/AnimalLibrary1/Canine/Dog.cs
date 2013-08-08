//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalMotel
{
    /// <summary
    ///     This class inherits from Canine, and through Canine from Animal.
    ///     The class describes an Dog
    /// </summary>
    public class Dog : Canine
    {
        #region Private fields
        /// <summary
        ///     Private field that describes if the dog is playful, true or false
        /// </summary>
        private bool m_playful;
        #endregion
        #region Properties
        /// <summary
        ///     Property Playful returns a true or false value to m_playful
        /// </summary
        public bool Playful
        {
            get { return this.m_playful; }
            set { this.m_playful = value; }
        }
        #endregion
        #region Methods
        /// <summary
        ///     Overrideable method that concats ExtraAnimalInfo with other strings depending on inherited properties
        /// </summary
        /// <returns>string strout with the whole sentence</returns>
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
                strout += String.Format("This dog is hungry!");
            }
            if (m_playful)
            {
                strout += String.Format("This dog is very playful!");
            }
            strout += Environment.NewLine + SetStinkAndInvasiveData() + "The dog doesn't want to be bothered";

            return strout;
        }
        /// <summary
        ///     Overridable method that sets if the dog stink and/or is invasive
        /// </summary
        /// <returns>string strOut</returns>
        public override string SetStinkAndInvasiveData()
        {
            var strOut = "Dog stink and needs a bath asap!";
            return strOut;
        }
        #endregion
    }
}
