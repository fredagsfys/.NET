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
using System.Windows.Shapes;

namespace ControlTower
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// publisher class
    /// </summary>
    public partial class FlightWindow : Window
    {
        /// <summary
        ///     Events for the three diffrent interactions, start, change route, land.
        /// </summary>
        public event EventHandler<FlightInfo> FlightStarted;
        public event EventHandler<FlightInfo> FlightLanded;
        public event EventHandler<FlightInfo> ChangedRoute;
        /// <summary
        ///     Fields
        /// </summary>
        private string flightCode = string.Empty;
        private Image imgHolder = null;
        /// <summary
        ///     Receives the flight objects properties
        /// </summary>
        public FlightWindow(string flightCode, Image imgHolder)
        {
            InitializeComponent();
            this.flightCode = flightCode;
            this.imgHolder = imgHolder;
            InitializeGUI();
        }
        /// <summary
        ///     Method which gets the flightcode and picks a picture for the flight
        /// </summary>
        public void InitializeGUI()
        {
            //Sets the entered flightcode as window titel in FlightWindow
            this.Title = flightCode;
            // Gets the flightcodes first letters and that will determ which
            //picture will show
            string imgUrl = null;
            //Gets the codes first letters and makes them capital
            string flightCodeHigh = flightCode.ToUpper();
            string flightCodeSub = flightCodeHigh.Substring(0, 2);
            //show different images depending on first letters in flightcode
            switch (flightCodeSub)
            {
                case "YY":
                    imgUrl = "/ControlTower;component/Images/flight.jpg";
                    break;
                case "ZZ":
                    imgUrl = "/ControlTower;component/Images/flight1.jpg";
                    break;
                case "XX":
                    imgUrl = "/ControlTower;component/Images/flight2.jpg";
                    break;
                default:
                       imgUrl = "/ControlTower;component/Images/noimage.png";
                    break;
            }
            //Sets the image
            this.flightImage.Source = new BitmapImage(new Uri(imgUrl, UriKind.Relative)); ;
        }
        /// <summary
        ///     Click event, enables and disables buttons and sends information about the flight started to listview later
        /// </summary>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;
            landButton.IsEnabled = true;
            changeRouteBox.IsEnabled = true;
            string time = DateTime.Now.ToLongTimeString() + ", " + DateTime.Now.ToShortDateString();
            //Sets the flightinfo
            FlightInfo flightInfo = new FlightInfo(this.Title, time, "Started");
            OnFlightStart(flightInfo);
        }
        /// <summary
        ///     Same as before, but this time while clicking "Land"
        /// </summary>
        private void landButton_Click(object sender, RoutedEventArgs e)
        {
            landButton.IsEnabled = false;
            changeRouteBox.IsEnabled = false;
            string time = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
            FlightInfo flightInfo = new FlightInfo(this.Title, time, "Landed");
            OnFlightLand(flightInfo);
            this.Close();
        }
        /// <summary
        ///     Sends the degree chosen in the ComboBox besides same as before
        /// </summary>
        private void changeRouteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string time = DateTime.Now.ToLongTimeString() + ", " + DateTime.Now.ToShortDateString();
            string deg = ((String)((ComboBoxItem)changeRouteBox.SelectedItem).Content);
            FlightInfo flightInfo = new FlightInfo(this.Title, time, "Now heading: " + deg);
            OnChangedRoute(flightInfo);
        }
        /// <summary
        ///     Checks if flightStarted isn't null, if not it sends event
        /// </summary>
        public void OnFlightStart(FlightInfo e)
        {
            if (FlightStarted != null)
            {
                FlightStarted(this, e);
            }
        }
        /// <summary
        ///     Checks if flightLanded isn't null, if not it sends event
        /// </summary>
        private void OnFlightLand(FlightInfo e)
        {
            if (FlightLanded != null)
            {
                FlightLanded(this, e);
            }
        }
        /// <summary
        ///     Checks if ChangedRoute isn't null, if not it sends event
        /// </summary>
        private void OnChangedRoute(FlightInfo e)
        {
            if (ChangedRoute != null)
            {
                ChangedRoute(this, e);
            }
        }
    }
}
