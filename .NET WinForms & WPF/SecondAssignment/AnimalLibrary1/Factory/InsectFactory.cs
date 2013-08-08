//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AnimalMotel.Insects
{
    /// <summary
    ///     InsectFactory is being called when an Animal in the category
    ///     Insect is choosen to be created. In this class the switch statment
    ///     solves the animal which is choose by the user and creates it.
    /// </summary
    public class InsectFactory
    {
        #region Methods
        public static Insect CreateInsect(InsectSpecies Species)
        {
            /// <summary
            ///     Static method that creates choosen Animal, determed on the parameter Species.
            ///     Insect animalObj set to null;
            ///     <returns>animalObj with created animal object</returns>
            /// </summary
            Insect animalObj = null;
            switch (Species)
            {
                case InsectSpecies.Bee:
                    animalObj = new Bee();
                    break;
                case InsectSpecies.Butterfly:
                    animalObj = new Butterfly();
                    break;
            }
            animalObj.Category = CategoryType.Insect;
            return animalObj;
        }
            #endregion
    }

}
