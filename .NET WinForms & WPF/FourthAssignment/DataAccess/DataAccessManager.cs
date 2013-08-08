//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
    public class DataAccessManager
    {
        /// <summary
        ///    Method which calls another method and gets a DataSet in return with all
        ///    database data.
        /// </summary>
        public DataSet PassingLoadAnimals()
        {
            try
            {
                DataAccess da = new DataAccess();
                var daLoad = da.LoadAnimalData();
                return daLoad;
            }
            catch
            {
                throw;
            }
        }
        /// <summary
        ///    Save method which send output parameters into a save method which saves
        ///    all data into the database
        /// </summary>
        public void SavingDB(int ID, string Name, int Age, string Gender, string Category, string Info)
        {
            try
            {
                DataAccess daSave = new DataAccess();
                daSave.SaveAnimal(ID, Name, Age, Gender, Category, Info);
            }
            catch
            {
                throw;
            }
        }
    }

}