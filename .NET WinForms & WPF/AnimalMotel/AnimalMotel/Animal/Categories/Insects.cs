//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1CS.Insects
{
    /// <summary
    ///     Class Insect describes the Category Canine and inherits class Animal
    ///     The class properties and methods inherits by underlying classes Bee and Butterfly.
    /// </summary
   public abstract class Insect : Animal
   {
        #region Private fields
        /// <summary
        ///     Private field which contains if the animal is poisonous, true or false
        /// </summary
	    private bool m_ispoisonous;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets the value to m_ispoisonous, true or false
        /// </summary
	    public bool IsPoisonous 
        {
		    get { return m_ispoisonous; }
		    set { m_ispoisonous = value; }
	    }
        #endregion
        #region Methods
        /// <summary
        ///     Abstract method, inherited by underlying classes
        /// </summary
        public abstract string SetStinkAndInvasiveData();
        #endregion
   }
}
