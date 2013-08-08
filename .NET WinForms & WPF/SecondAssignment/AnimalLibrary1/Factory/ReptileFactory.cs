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
    ///     ReptileFactory is being called when an Animal in the category
    ///     Reptile is choosen to be created. In this class the switch statment
    ///     solves the animal which is choose by the user and creates it.
    /// </summary
    public class ReptileFactory
    {
        #region Methods
        public static Reptile CreateReptile(ReptileSpecies Species)
        {
            /// <summary
            ///     Static method that creates choosen Animal, determed on the parameter Species.
            ///     Reptile animalObj set to null;
            ///     <returns>animalObj with created animal object</returns>
            /// </summary
            Reptile animalObj = null;
            switch (Species)
            {
                case ReptileSpecies.Anaconda:
                    animalObj = new Anaconda();
                    break;
                case ReptileSpecies.Crocodile:
                    animalObj = new Crocodile();
                    break;
            }
            animalObj.Category = CategoryType.Reptile;
            return animalObj;
        }
            #endregion
    }
}
