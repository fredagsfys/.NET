//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
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

namespace CardGameControlLibrary
{
    /// <summary>
    /// Interaction logic for CardGUI.xaml
    /// </summary>
    public partial class CardGUI : UserControl
    {
        /// <summary
        ///     Constructor for CardGUI
        /// </summary>
        public CardGUI()
        {
            InitializeComponent();
        }

        /// <summary
        ///     Method with sets the url for the images
        /// </summary>
        public void CardImage(string url)
        {
            string longUrl = String.Format("/CardGameControlLibrary;component/Images/{0}", url);
            this.cardImage.Source = new BitmapImage(new Uri(longUrl, UriKind.Relative));
        }
    }
}
