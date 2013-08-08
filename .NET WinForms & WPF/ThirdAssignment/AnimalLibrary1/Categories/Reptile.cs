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
    ///     Class Reptile describes the Category Reptile and inherits class Animal
    ///     The class properties and methods inherits by underlying classes Anaconda and Crocodile
    /// </summary
    [Serializable]
    public abstract class Reptile : Animal
    {
        #region Private fields
        /// <summary
        ///     Private field which contains if the animal is Deadly, true or false
        /// </summary
        private bool m_isDeadly;
        #endregion
        #region Properties
        /// <summary
        ///     Property that sets the value to m_isDeadly, true or false
        /// </summary
        public bool IsDeadly
        {
		    get { return m_isDeadly; }
		    set { m_isDeadly = value; }
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
