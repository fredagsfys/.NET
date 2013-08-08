//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameLibrary
{
    /// <summary>
    /// This class is the core of the cardgame, it creates the cards and containes em' in a list.
    /// Also creates events and gets the desired card.
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// Fields and list, obj
        /// </summary>
        private List<Card> cards = new List<Card>();
        private int currentPosition;
        private Card card;

        /// <summary>
        /// Method that has object card as parameter, 
        /// also sends card to the method GetAceSuit
        /// </summary>
        public Deck(Card card)
        {
            this.card = card;
            GetAceSuit(card);
        }

        /// <summary>
        /// Constructor which creates the list of cards within the loop and adds them 
        /// to the list of cards.
        /// </summary>
        public Deck()
        {
            for (int i = 1; i < 14; i++)
            {
                Card heartCard = new Card(i, CardSuit.Heart, String.Format("h{0}.png", i));
                Card spadeCard = new Card(i, CardSuit.Spade, String.Format("s{0}.png", i));
                Card diamondCard = new Card(i, CardSuit.Diamond, String.Format("d{0}.png", i));
                Card clubCard = new Card(i, CardSuit.Club, String.Format("c{0}.png", i));
                cards.Add(heartCard);
                cards.Add(spadeCard);
                cards.Add(diamondCard);
                cards.Add(clubCard);
            }
        }

        /// <summary>
        /// Creates instance of DrawCardEventArgscs and send the card with it, also sets it.
        /// </summary>
        public void GetAceSuit(Card card) 
        {
            DrawCardEventArgscs drawCEA = new DrawCardEventArgscs(card);
            OnAceDrawn(drawCEA);
        }

        /// <summary
        ///     Checks if OnAceDrawn is null, if not sends it
        /// </summary>
        private void OnAceDrawn(DrawCardEventArgscs e)
        {
            if (AceDrawn != null)
            {
                AceDrawn(this, e);
            }
        }

        /// <summary
        ///     Gets the next card in the list and returns it.
        /// </summary>
        /// <returns>string card</returns>
        public Card GetNextCard()
        {
            currentPosition += 1;
            return cards[currentPosition];     
        }

        /// <summary
        ///     Gets the current card in the list and returns it.
        /// </summary>
        /// <returns>string card</returns>
        public Card GetCurrentCard()
        {
            currentPosition -= 1;
            return cards[currentPosition];
        }

        /// <summary
        ///     Gets the top card and returns it
        /// </summary>
        /// <returns>string card</returns>
        public Card GetTopCard()
        {
            return cards[currentPosition];
        }

        /// <summary
        ///     Gets all cards and returns them
        /// </summary>
        /// <returns>list card</returns>
        public List<Card> GetAllCards()
        {
            return cards;
        }

        /// <summary
        ///     Gets current position
        /// </summary>
        /// <returns>int position</returns>
        public int GetCurrentPosition()
        {
            return currentPosition;
        }

        /// <summary
        ///     Shuffles the card before the game starts and sets current position to zero
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();
            for (int first = 0; first < cards.Count(); first++)
            {
                int second = rand.Next(cards.Count());
                Card temp = cards[first];
                cards[first] = cards[second];
                cards[second] = temp;
            }
            currentPosition = 0;
        }

        /// <summary
        ///     Events for the two interactions, on ace drawn and end of deck.
        /// </summary>
        public event EventHandler<DrawCardEventArgscs> AceDrawn;
        public event EventHandler<DrawCardEventArgscs> EndOfDeckHandler;
    }
}
