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
    ///     Class Canine describes the Category Canine and inherits class Animal
    ///     The class properties and methods inherits by underlying classes Dog and Cat.
    /// </summary
    public abstract class Canine : Animal
    {
        #region Private fields
        /// <summary
        ///     Private field that describes if the animal is hungry, true or false
        /// </summary
	    private bool m_isHungry;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets the value for m_isHungry, either true or false
        /// </summary
	    public bool IsHungry
        {
		    get { return m_isHungry; }
		    set { m_isHungry = value; }
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
