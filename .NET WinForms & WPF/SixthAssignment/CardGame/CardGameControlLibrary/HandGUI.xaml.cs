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
    /// Interaction logic for HandGUI.xaml
    /// </summary>
    public partial class HandGUI : UserControl
    {
        /// <summary
        ///     Constructor for HandGUI
        /// </summary>
        public HandGUI()
        {
            InitializeComponent();
        }

        /// <summary
        ///     Counts the card and sends it to the label to show
        /// </summary>
        public void Count()
        {
            label1.Content = canvas1.Children.Count.ToString();
        }

        /// <summary
        ///     Creates a new cardGUI for the next card and places it depending on the coordinate.
        /// </summary>
        /// <returns>card</returns>
        public CardGUI newCardGUI(double xCoordinate)
        {
            CardGUI newCard = new CardGUI();

            try
            {
                canvas1.Children.Add(newCard);
                newCard.SetValue(Canvas.LeftProperty, xCoordinate);
            }
            catch (Exception)
            {
                MessageBox.Show("error!");
            }

            return newCard;
        }

        /// <summary
        ///     Clears the Canvas
        /// </summary>
        public void deleteList()
        {
            canvas1.Children.Clear();
        }
    }
}
