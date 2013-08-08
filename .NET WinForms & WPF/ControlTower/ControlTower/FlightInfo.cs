//Programming using .NET, Advanced Course
//The Control Tower
//Haris Kljajic August 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlTower
{
    /// <summary>
    /// This class contains info about take off event.
    /// When an event is fired this class is also sent
    /// with the event notifier (delegate)
    /// </summary>
    public class FlightInfo : EventArgs
    {
        /// <summary
        ///    Fields
        /// </summary>
        private string _flightCode; //Airplanes flight code
        private string _status; //Airplane status
        private string _time; //Airplane time
        /// <summary
        ///     Constructor which has three parameters and sends them to separate properties..
        /// </summary>
        public FlightInfo(string flightCode, string status, string time)
        {
            SetFlight = flightCode;
            SetStatus = time;
            SetTime = status;
        }
        /// <summary
        ///     Set Flightcode property
        /// </summary>
        public string SetFlight
        {
            get { return _flightCode; }
            set { _flightCode = value; }
        }
        /// <summary
        ///     Set status property
        /// </summary>
        public string SetStatus
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary
        ///     Set time property
        /// </summary>
        public string SetTime
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
