//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimalMotel;
using AnimalMotel.Insects;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using DataAccess;
using System.Data;

namespace AnimalMotel
{
    /// <summary
    ///     Manager class holds all calculation and validated information 
    ///     which is being created and saved from here
    /// </summary>
    [Serializable]
    public class AnimalManager
    {
        #region Fields, List
        /// <summary
        ///     Private int, holds the ID number, starts at 999
        /// </summary>
        private int number = 999;
        /// <summary
        ///     Creates an instance of the manager
        /// </summary>
        private DataAccessManager da = new DataAccessManager();
        /// <summary
        ///     Creates a animalList where the objects can be saved into
        /// </summary>
        private List<Animal> animalList = new List<Animal>();
        #endregion
        #region Methods
        /// <summary
        ///     Loops out the objects information and saves them into
        ///     an string array which is called to show a list of all
        ///     saved animals
        /// </summary>
        /// <returns>strOut with the objects information</returns>
        public string[] GetAnimalList()
        {
            string[] strOut = new string[this.animalList.Count];
            int i = 0;

            foreach (Animal animalObj in this.animalList)
            {
                strOut[i] += String.Format("{0, 0}\t{1, 20}\t{2, 28}\t{3, 28}", animalObj.ID, animalObj.Name, animalObj.Age, animalObj.Gender);
                i += 1;
            }
            return strOut;
        }
        /// <summary
        ///     Method that get called when the user chooses New in
        ///     the file menu, it clears the current animals listed.
        ///     Sets the number ID to default.
        /// </summary>
        public void ClearAnimalList()
        {
            animalList.Clear();
            number = 999;
        }
        /// <summary
        ///     AddAnimal takes 7 parameters which comes from the user, validated already
        ///     and ready to be created with an animal object and last to be saved.
        ///     
        ///     Depending on choosen animal and object the switch statment solves it
        ///     and creates an object in the InsectFactory.
        /// </summary>
        public void AddAnimal(string name, string age, string extraAnimalInfo, GenderType genderType, CategoryType categoryType, Enum specie)
        {
            GetAnimalID();

            CategoryType animalType = (CategoryType)categoryType;
            Animal animalObj = null;

            switch (animalType)
            {
                case CategoryType.Insect:
                    InsectSpecies insectSpecie = (InsectSpecies)specie;
                    animalObj = InsectFactory.CreateInsect(insectSpecie);
                    break;

                case CategoryType.Canine:
                    CanineSpecies canineSpecie = (CanineSpecies)specie;
                    animalObj = CanineFactory.CreateCanine(canineSpecie);
                    break;

                case CategoryType.Reptile:
                    ReptileSpecies reptileSpecie = (ReptileSpecies)specie;
                    animalObj = ReptileFactory.CreateReptile(reptileSpecie);
                    break;
            }
            /// <summary
            ///     Validates the animalObj, if its not null it saves the information to the List, animalList
            /// </summary>
            if (animalObj != null)
            {
                animalObj.ID = number;
                animalObj.Name = name;
                animalObj.Age = Convert.ToInt32(age);
                animalObj.Gender = (GenderType)genderType;
                animalObj.ExtraAnimalInfo = string.Format("{0} \r\n {1}", extraAnimalInfo, animalObj.GetAnimalSpecificData());

                animalList.Add(animalObj);
            }
        }
        /// <summary
        ///     Depending on what object information in the registeranimalListBox the user
        ///     chooses, this method gets the ExtraAnimalInfo on shows it in the textbox.
        ///     
        ///     The user will have an dynamic experience with the registerlist
        /// </summary>
        /// <returns>specificText - selectedAnimal is the index, found in animalList</returns>
        public string GetExtraInfo(int selectedAnimal)
        {
            string specificText = null;
            if (animalList.Count != 0)
            {
                return specificText = animalList[selectedAnimal].ExtraAnimalInfo;
            }
            else
            {
                var daLoad = da.PassingLoadAnimals();
                foreach (DataRow myDataRow in daLoad.Tables["AnimalList"].Rows)
                {
                    if(selectedAnimal == daLoad.Tables["AnimalList"].Rows.IndexOf(myDataRow))
                    specificText = myDataRow["Info"].ToString();
                }
            }
            return specificText;
        }
        /// <summary
        ///     This method gives each animal saved an ID, each time added by +1, this 
        ///     to not collide with other ID's
        /// </summary>
        /// <returns>Returns the ID number to number</returns>
        public int GetAnimalID()
        {
            return number += 1;
        }
        /// <summary
        ///     This method takes one argument, the filename, given by user and saves all objects
        ///     contained in the List<T> animalList into a .DAT file. 
        /// </summary>
        public void BinarySerialize(string filename)
        {
            try
            {
                using (Stream fileStream = File.Open(filename, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(fileStream, animalList);
                }
            }
            catch (IOException error)
            {
                throw new IOException(error.Message);
            }
        }
        /// <summary
        ///     This method takes one argument, the filename, chosen by user and opens up 
        ///     selected file. The objects saved in the file loads as it was saved before
        ///     in the ListView.
        /// </summary>
        public void  BinaryDeserialize(string filename)
        {
            try
            {
                using (Stream fileStream = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    animalList = (List<Animal>)bin.Deserialize(fileStream);
                }
            }
            catch (IOException error)
            {
                throw new IOException(error.Message);
            }
        }
        public string[] LoadAnimals()
        {
            try
            {
                var daLoad = da.PassingLoadAnimals();

                string[] strOut = new string[daLoad.Tables["AnimalList"].Rows.Count];
                int i = 0;

                foreach (DataRow myDataRow in daLoad.Tables["AnimalList"].Rows)
                {
                    strOut[i] += String.Format("{0, 0}\t{1, 20}\t{2, 28}\t{3, 28}", myDataRow["ID"].ToString(), myDataRow["Name"].ToString(), myDataRow["Age"].ToString(), myDataRow["Gender"].ToString());
                    i += 1;
                }
                return strOut;
            }
            catch
            {
                throw;
            }
        }
        public void AddAnimalDB()
        {
            try
            {
                foreach (Animal animalObj in this.animalList)
                {
                    da.SavingDB(animalObj.ID, animalObj.Name, animalObj.Age, animalObj.Gender.ToString(), animalObj.Category.ToString(), animalObj.ExtraAnimalInfo);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
