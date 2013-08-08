//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AnimalMotel
{
    /// <summary
    ///     CanineFactory is being called when an Animal in the category
    ///     Canine is choosen to be created. In this class the switch statment
    ///     solves the animal which is choose by the user and creates it.
    /// </summary
    public class CanineFactory
    {
        #region Methods
        public static Canine CreateCanine(CanineSpecies Species)
        {
            /// <summary
            ///     Static method that creates choosen Animal, determed on the parameter Species.
            ///     Canine animalObj set to null;
            ///     <returns>animalObj with created animal object</returns>
            /// </summary
            Canine animalObj = null;
            switch (Species)
            {
                case CanineSpecies.Dog:
                    animalObj = new Dog();
                    break;
                case CanineSpecies.Cat:
                    animalObj = new Cat();
                    break;
            }
            return animalObj;
        }
        #endregion

    }

}
