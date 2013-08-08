//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameLibrary
{
    /// <summary
    ///     Class Card which sets the cards values such as 
    ///     which card, suit it belongs to and its imageIndex.
    /// </summary>
    public class Card
    {
        /// <summary
        ///     Fields
        /// </summary>
        private int cardValue;
        private CardSuit suit;
        private string imageIndex;

        /// <summary
        ///     Property that sets the cardValue
        /// </summary>
        public int CardValue
        {
            get { return cardValue; }
            set { cardValue = value; }
        }

        /// <summary
        ///     Property that sets the imageIndex
        /// </summary>
        public string ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        /// <summary
        ///     Property which sets the suit
        /// </summary>
        public CardSuit Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        /// <summary
        ///     Constructor with three parameters
        /// </summary>
        public Card(int _cardValue, CardSuit _suit, string _imageIndex)
        {
            cardValue = _cardValue;
            suit = _suit;
            imageIndex = _imageIndex;
        }
    }
}
