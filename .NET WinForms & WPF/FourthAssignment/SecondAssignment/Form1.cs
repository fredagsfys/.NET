//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnimalMotel.Insects;
using System.IO;

namespace AnimalMotel
{
    /// <summary
    ///     This class handles all the interaction with the user and validates all income information.
    ///     It also handels errors, understandable for the user.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary
        ///     Creates and instance of AnimalManager, this so 
        ///     we can use the methods declared inside the class
        /// </summary>
        private AnimalManager Manager = new AnimalManager();
        /// <summary
        ///     Variables decleared to be sent later into Manager.AddAnimal(...)
        /// </summary>
        private GenderType gender;
        private CategoryType category;
        private Enum specie;
        private string fileName = null;
        /// <summary
        ///     Constructor with own initilizing, calls IntializeGUI()
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //Egen initialisering
            InitializeGUI();
        }
        /// <summary
        ///     Each time an animal has been added and list is being updated this method
        ///     is called to remove existing items in AnimalRgstrListBox and add the new list.
        ///     It also sets textboxes to empty.
        /// </summary>
        private void UpdateGUI()
        {
            nameTextbox.Text = null;
            ageTextbox.Text = null;
            AddImpInfoTextbox.Text = null;
            foreach (string item in Manager.GetAnimalList())
            {
                AnimalRgstrListView.Items.Add(item);
            }
        }
        /// <summary
        ///     Sets the DataSource from Gender and Catergory enums
        /// </summary>
        private void InitializeGUI()
        {
            CategoryListBox.DataSource = Enum.GetValues(typeof(CategoryType));
            GenderListbox.DataSource = Enum.GetValues(typeof(GenderType));

            AnimalRgstrListView.View = View.Details;
            AnimalRgstrListView.FullRowSelect = true;
            AnimalRgstrListView.GridLines = true;
        }
        /// <summary
        ///     Depending on what category the user chooses, the switch statemnt sets
        ///     AnimalListBox to diffrent Datasources.
        /// </summary>
        private void CategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            category = (CategoryType)CategoryListBox.SelectedIndex;

            switch (category)
            {
                case CategoryType.Insect:
                    AnimalListBox.DataSource = Enum.GetValues(typeof(InsectSpecies));
                    break;
                case CategoryType.Canine:
                    AnimalListBox.DataSource = Enum.GetValues(typeof(CanineSpecies));
                    break;
                case CategoryType.Reptile:
                    AnimalListBox.DataSource = Enum.GetValues(typeof(ReptileSpecies));
                    break;
            }
        }
        /// <summary
        ///     When the user clicks the Add Animal button this event is triggered.
        ///     First it validates the inputs by calling method Validation();
        ///     If they are valid the inputs are sent to Manager.AddAnimal() class 
        ///     where they are being created and saved.
        ///     
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Manager.AddAnimal(nameTextbox.Text, ageTextbox.Text, AddImpInfoTextbox.Text, gender, category, specie);
                UpdateGUI();
            }
        }
        /// <summary
        ///     This method handels the validation.
        ///     If one statment fails to return true it results in an error with correction for the user.
        ///     The if-statments depending on which miss the user is doing renders a text with instruction on whats wrong.
        ///     
        ///     The method also Parses age string to int capsed in a try/catch.
        /// </summary>
        /// <returns>confirm, if all statments are true this methods is passed</returns>
        private bool Validation()
        {
            int index = AnimalListBox.SelectedIndex;
            bool validAge = true;
            int age = 0;
            try
            {
                age = int.Parse(ageTextbox.Text);
            }
            catch
            {
                validAge = false;
            }
            if (string.IsNullOrEmpty(nameTextbox.Text) && string.IsNullOrEmpty(ageTextbox.Text))
            {
                MessageBox.Show("Please enter a name and a age in the fields", "Invalid name and age");
            }
            else if (string.IsNullOrEmpty(nameTextbox.Text))
            {
                MessageBox.Show("Please enter a name in the name field", "Invalid name");
            }
            else if (string.IsNullOrEmpty(ageTextbox.Text))
            {
                MessageBox.Show("Please enter a age in the age field", "Invalid age");
            }
            else if (validAge == false || age < 0)
            {
                MessageBox.Show("The age is incorrect, please enter an valid age", "Invalid age");
            }
            bool confirm = (!string.IsNullOrEmpty(nameTextbox.Text) & (!string.IsNullOrEmpty(ageTextbox.Text)) & validAge & age >= 0 == true);
            return confirm;
        }
        /// <summary
        ///     If the selected index changes it sets the new selected index to the variable
        /// </summary>
        private void GenderListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            gender = (GenderType)GenderListbox.SelectedIndex;
        }
        /// <summary
        ///     If the selected index changes it sets the new selected index to the variable
        /// </summary>
        private void AnimalListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specie = (Enum)AnimalListBox.SelectedItem;
        }
        /// <summary
        ///     If the selected index changes it calls the Manager.GetExtraInfo(...)
        ///     with selectedIndex and gets the information specific for that index choosen.
        ///     
        ///     This will result in an dynamic view of the register.
        /// </summary>
        private void AnimalRgstrListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.AnimalRgstrListView.SelectedIndices;
            foreach (int index in indexes)
            {
                ShowImpInfoTextbox.Text = Manager.GetExtraInfo(index);
            }
        }
        /// <summary
        ///     Calls method that clears current animals out when user chooses New in menu.
        ///     Also checks if user wants to save before resetting etc.
        /// </summary>
        private void mnuNew_Click(object sender, EventArgs e)
        {
           DialogResult dialogResult = MessageBox.Show("Would you like to save before resetting?", "Alert", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                Save();
                DialogResult dialogResult3 = MessageBox.Show("Are you sure you want to reset?", "Alert", MessageBoxButtons.YesNo);
                if (dialogResult3 == DialogResult.Yes)
                {
                    Reset();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                DialogResult dialogResult2 = MessageBox.Show("Are you sure you want to reset?", "Alert", MessageBoxButtons.YesNo);
                if (dialogResult2 == DialogResult.Yes)
                {
                    Reset();
                }
            }
            
        }
        /// <summary
        ///     Opens up a dialog were the user can choose which file to be opened.
        ///     Also adds a filter so the user only can choose .dat format
        /// </summary>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
              saveFileDialog.Filter = ".dat|*.dat";
              if(openFileDialog.ShowDialog() == DialogResult.OK)
              {
                  Manager.BinaryDeserialize(openFileDialog.FileName);
                  fileName = openFileDialog.FileName;
                  UpdateGUI();
              }
        }
        /// <summary
        ///    Opens a dialog where the user can choose where to save their data on the computers harddrive.
        ///    Also added a filter so the user only can choose to save in .dat format.
        /// </summary>
        public void mnuSaveAs_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = ".dat|*.dat";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { 
                Manager.BinarySerialize(fileName);
                fileName = saveFileDialog.FileName;
            }
        }
        /// <summary
        ///    Calls Save() method
        /// </summary>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        /// <summary
        ///    Exit method that has an algoritm, depending on if the file has been saved before 
        ///    exiting and will ask you if you really want to exit the program.
        /// </summary>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (fileName == null)
            {
                DialogResult dialogReslutQuitSave = MessageBox.Show("Do you want to save before exiting application?", "Alert", MessageBoxButtons.YesNoCancel);
                if (dialogReslutQuitSave == DialogResult.Yes)
                {
                    Save();
                    DialogResult dialogReslutQuit = MessageBox.Show("Are you sure you want to quit?", "Alert", MessageBoxButtons.YesNo);
                    if (dialogReslutQuit == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
                else if (dialogReslutQuitSave == DialogResult.No)
                {
                    DialogResult dialogReslutQuit = MessageBox.Show("Are you sure you want to quit?", "Alert", MessageBoxButtons.YesNo);
                    if (dialogReslutQuit == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
            }
            else if (fileName != null)
            {
                DialogResult dialogReslutQuit = MessageBox.Show("You are about to quit, proceed?", "Alert", MessageBoxButtons.YesNo);
                if (dialogReslutQuit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }
        /// <summary
        ///    Save method with a minior algoritm, depending on if the file has been
        ///    saved before or not.
        /// </summary>
        private void Save()
        {
            saveFileDialog.Filter = ".dat|*.dat";
            if (fileName == null)
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    try
                    {
                        Manager.BinarySerialize(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }  
            }
            else if (fileName != null)
            {
                try
                {
                    Manager.BinarySerialize(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                throw new ArgumentNullException("An error occured, contact your programmer");
            }
        }
        /// <summary
        ///    Reset method that restors the application to a start-up view, all existing info
        ///    will be whiped.
        /// </summary>
        private void Reset()
        {
            nameTextbox.Text = null;
            ageTextbox.Text = null;
            AddImpInfoTextbox.Text = null;
            ShowImpInfoTextbox.Text = null;
            AnimalListBox.SelectedItem = InsectSpecies.Bee;
            CategoryListBox.SelectedItem = CategoryType.Insect;
            GenderListbox.SelectedItem = GenderType.Male;
            AnimalRgstrListView.Items.Clear();
            Manager.ClearAnimalList();
        }
        /// <summary
        ///    LoadDataTool event triggers an loadevent which calls DataAccessManager and
        ///    get back in return all data info from the animal list in the database
        /// </summary>
        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.ClearAnimalList();
            AnimalRgstrListView.Items.Clear();
            try
            {
                foreach (string item in Manager.LoadAnimals())
                {
                    AnimalRgstrListView.Items.Add(item);
                    Manager.GetAnimalID();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary
        ///    Save event which triggers an save function, this event calls method in animal manager
        ///    and saves down all info from the application list into the database
        /// </summary>
        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Manager.AddAnimalDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
