//Programming using .NET, Advanced Course
//The Control Tower
//Haris Kljajic August 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ControlTower
{
    /// <summary>
    /// Interaction logic for ControlTowerWindow.xaml
    /// Subscriber class
    /// </summary>
    public partial class ControlTowerWindow : Window
    {
        private FlightInfo newFlight;
        /// <summary
        ///     Constructor with flightInfo object as parameter
        /// </summary>
        public ControlTowerWindow(FlightInfo flightInfo)
        {
            InitializeComponent();
            newFlight = flightInfo;
        }
        /// <summary
        ///     Empty constructor
        /// </summary>
        public ControlTowerWindow()
        {
        }
        /// <summary
        ///     Method which creates a new flight and subscribes to events
        ///     If statment checks if there is an flight code and if its correct wroten, else the user will get
        ///     an error message.
        /// </summary>
        /// <returns>bool, true or false</returns>
        private bool CreateNewFlight()
        {
            string flightCode = ReadFlightCode();

            //Creates the flight
            if (!string.IsNullOrEmpty(flightCode) && Regex.IsMatch(flightCode, @"^[A-Za-z]{2}.*\d{4}$"))
            {
                Image imgHolder = new Image();
                //Creates an instance of FlightWindow and sends parameters with image and flightcode
                FlightWindow flightWindw = new FlightWindow(flightCode, imgHolder);
                flightWindw.Show();

                //Suscribe to publisher
                flightWindw.FlightStarted += OnFlightStarted;
                flightWindw.FlightLanded += OnFlightLanded;
                flightWindw.ChangedRoute += OnChangedRoute;
                return false;
            }

            else
            {
                return true;
            }
        }
        /// <summary
        ///     If the event FlightStarted is being triggered it sends info to the ListView and show the information there
        /// </summary>
        private void OnFlightStarted(object sender, FlightInfo e)
        {
            dynamic strOut = string.Format("\t{0,-40} {1,-40} {2,20}", e.SetFlight, e.SetStatus, e.SetTime);
            ListView.Items.Add(strOut);
        }
        /// <summary
        ///     If the event ChangedRoute is being triggered it sends info to the ListView and show the information there
        /// </summary>
        private void OnChangedRoute(object sender, FlightInfo e)
        {
            dynamic strOut = string.Format("\t{0,-40} {1,-40} {2,20}", e.SetFlight, e.SetStatus, e.SetTime);
            ListView.Items.Add(strOut);
        }
        /// <summary
        ///     If the event FlightLanded is being triggered it sends info to the ListView and show the information there
        /// </summary>
        private void OnFlightLanded(object sender, FlightInfo e)
        {
            dynamic strOut = string.Format("\t{0,-40} {1,-40} {2,20}", e.SetFlight, e.SetStatus, e.SetTime);
           ListView.Items.Add(strOut);
        }
        /// <summary
        ///     Method which reads the entered flightcode
        /// </summary>
        /// <returns>the entered code if its correct</returns>
        private string ReadFlightCode()
        {
            //take away the starting and trailing spaces
            string flightTextBox = nextFlightBox.Text.Trim();

            //is the flightcode empty?
            if (string.IsNullOrEmpty(flightTextBox) || !Regex.IsMatch(flightTextBox, @"^[A-Za-z]{2}.*\d{4}$"))
            {
                //show error message and make text red
                MessageBox.Show("The typed flightcode is incorrect! (Ex. XX9999 / YY7777 / ZZ4444)");
                return flightTextBox;
            }
            return flightTextBox;
        }
        /// <summary
        ///     Method updates status when its sent to runway and adds the 
        ///     string concated into the listview
        /// </summary>
        private void UpdateStatus()
        {
            string sentRunway = "Sent to runway";
            string timeRunway = DateTime.Now.ToLongTimeString() + ", " + DateTime.Now.ToShortDateString();
            dynamic strOut = string.Format("\t{0,-40} {1,-40} {2,20}", nextFlightBox.Text, sentRunway, timeRunway);
            ListView.Items.Add(strOut);
        }
        /// <summary
        ///     Click-function, triggered when pushing Send next flight to Runway
        /// </summary>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            //Updates the status if the flight was created
            if (CreateNewFlight() == false)
            {
                UpdateStatus();
                nextFlightBox.Text = null;
            }
        }
    }
}
