//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    sealed class DataAccess : DataAccessManager
    {
        /// <summary
        ///    private fields with information such as connection path to database, 
        ///    dataset which is able to copy database data.
        ///    Adapter to use commands to speak with database.
        /// </summary>
        private SqlConnection mConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AnimalMotel.Properties.Settings.AnimalMotelDBConnectionString"].ConnectionString);
        private SqlDataAdapter mDataAdapterAnimals = new SqlDataAdapter();
        private DataSet mDataSet = new DataSet();
        /// <summary
        ///    Property that gets the total rows in the database
        /// </summary>
        public int AnimalsDBCount 
        {
            get
            {
                return mDataSet.Tables["AnimalList"].Rows.Count;    
            }
        }
        /// <summary
        ///    Method which calls database and fills the DataSet with all its info
        /// </summary>
        public DataSet LoadAnimalData()
        {
            try
            {
                mConnection.Open();

                mDataAdapterAnimals.SelectCommand = new SqlCommand("SELECT * FROM AnimalList", mConnection);

                mDataAdapterAnimals.Fill(mDataSet, "AnimalList");
            }
            catch
            {
                throw;
            }
            finally
            {
                mConnection.Close();
            }
            return mDataSet;
        }
        /// <summary
        ///    Inline written database query which saves animal list from application into the database
        /// </summary>
        public void SaveAnimal(int id, string name, int age, string gender, string category, string info)
        {
            try
            {   
                mDataAdapterAnimals = new SqlDataAdapter();

                mDataAdapterAnimals.InsertCommand = new SqlCommand("INSERT INTO AnimalList VALUES(@ID , @Name, @Age, @Info, @Gender,  @Category)", mConnection);
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = name;
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@Age", SqlDbType.Int).Value = age;
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@Info", SqlDbType.VarChar, 500).Value = info;
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@Gender", SqlDbType.VarChar, 500).Value = gender;
                mDataAdapterAnimals.InsertCommand.Parameters.Add("@Category", SqlDbType.VarChar, 500).Value = category;

                mConnection.Open();

                mDataAdapterAnimals.InsertCommand.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                mConnection.Close();
            }
        }
     }
}
