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
    ///     Animal is the top class, inherits from the interface IAnimal
    ///     Animal has the most general and common properties, methods, fields.
    ///     All underlying classes inherits from Animal
    /// </summary>
    public abstract class Animal : IAnimal
    {
#region Private fields
        /// <summary
        ///     Private fields that hold the animals general information
        ///     such as Name, Age, Category, Gender, ID and ExtraAnimalInfo
        /// </summary>
        private string _name;
        private double _age;
        private CategoryType _category;
        private GenderType _gendertype;
        private int _id;
        private string _extraAnimalInfo;
#endregion 
#region Properties
        /// <summary
        ///     All these properties sets the value to the private fields above
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public double Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public CategoryType Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public GenderType Gender
        {
            get { return _gendertype; }
            set { _gendertype = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ExtraAnimalInfo
        {
            get { return _extraAnimalInfo; }
            set { _extraAnimalInfo = value; }
        }
#endregion  
#region Methods
        /// <summary
        ///     Declaes abstract method which is inherited and overwritten by animal classes to
        ///     their own specific data
        /// </summary>
        public abstract string GetAnimalSpecificData();
#endregion
    }
}
