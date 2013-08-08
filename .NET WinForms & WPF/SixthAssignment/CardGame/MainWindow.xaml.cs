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
using CardGameLibrary;
using System.Threading;

namespace CardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary
        ///     Instance of Deck, field
        /// </summary>
        private Deck newDeck;
        private double xCoordinate = 0.0;

        /// <summary
        ///     Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary
        ///     Click event which on click removes current game and shuffles/creates a new one
        ///     Sets the coordinate to 0, enables some buttons which are disabled at games end or before shuffleing.
        ///     
        ///     Also has an if-statment for the show cards up in the pile or down
        /// </summary>
        private void newDeckShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            handGUI1.deleteList();
            xCoordinate = 0.0;
            faceUpCheckBox.IsEnabled = IsEnabled;
            cardGUI1.Visibility = Visibility.Visible;
            drawCardsButton.IsEnabled = IsEnabled;
            acesListBox.Items.Clear();
            newDeck = new Deck();
            newDeck.Shuffle();

            //Gets the topcard and put in first cardcontent
            Card nextCard = newDeck.GetTopCard();
            if (faceUpCheckBox.IsChecked == true)
            {
                cardGUI1.CardImage(nextCard.ImageIndex);
            }

            else
            {
                cardGUI1.CardImage("b1fv.png");
            }
        }

        /// <summary
        ///     On ace drawn, adds the ace into the listbox
        /// </summary>
        private void OnAceDraw(object sender, DrawCardEventArgscs e)
        {
            acesListBox.Items.Add(e.aCard.Suit);
        }

        /// <summary
        ///     When the button drawCard is clicked this fires and gets the next card from the list inside the deck.
        ///     Also checks if the card is an ace, if it true the event is fired and calls also GetAceSuit in the Deck.
        ///     drawCardsButton also checks if its the last card and shows a end card
        /// </summary>
        private void drawCardsButton_Click(object sender, RoutedEventArgs e)
        {
            Card theCard = newDeck.GetNextCard();
            newDeck.GetCurrentCard();
            string imgIndex = theCard.ImageIndex;
            if (imgIndex == "h1.png" || imgIndex == "s1.png" || imgIndex == "d1.png" || imgIndex == "c1.png")
            {
                Deck deck = new Deck();
                deck.AceDrawn += OnAceDraw;
                deck.GetAceSuit(theCard);
            }

            if (showAllCardsButton.IsChecked == true)
            {
                List<Card> allCards = newDeck.GetAllCards();
                int currentPosition = newDeck.GetCurrentPosition();

                for (int i = currentPosition; i < allCards.Count(); i++)
                { 
                    handGUI1.newCardGUI(xCoordinate += 12).CardImage(allCards[i].ImageIndex);
                }

                cardGUI1.CardImage("end.png");
                xCoordinate = 0.0;
                drawCardsButton.IsEnabled = false;
                faceUpCheckBox.IsEnabled = false;
            }

            else
            {
                try
                {
                    handGUI1.newCardGUI(xCoordinate += 12).CardImage(newDeck.GetTopCard().ImageIndex);
                }

                catch
                {
                    MessageBox.Show("Couldnt get new card");
                }

                if (faceUpCheckBox.IsChecked == true)
                {
                    try
                    {
                        cardGUI1.CardImage(newDeck.GetNextCard().ImageIndex);
                    }
                    catch
                    {
                        cardGUI1.CardImage("end.png");
                        xCoordinate = 0.0;
                        drawCardsButton.IsEnabled = false;
                        faceUpCheckBox.IsEnabled = false;
                    }
                }

                else
                {
                    cardGUI1.CardImage("b1fv.png");

                    try
                    {
                        newDeck.GetNextCard();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("last card");
                        cardGUI1.CardImage("end.png");
                        xCoordinate = 0.0;
                        drawCardsButton.IsEnabled = false;
                        faceUpCheckBox.IsEnabled = false;
                    }

                }
            }

            handGUI1.Count();
        }

        /// <summary
        ///     If the checkbox is checked it shows the cards up in the pile
        /// </summary>
        private void faceUpCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cardGUI1.CardImage(newDeck.GetTopCard().ImageIndex);
        }

        /// <summary
        ///     If the checkbox is unchecked it hides the cards in the pile, face down
        /// </summary>
        private void faceUpCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            cardGUI1.CardImage("b1fv.png");
        }

        private void acesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
