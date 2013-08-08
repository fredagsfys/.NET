//Programming using .NET advanced course
//Code Example : Animal Motel
//Haris Kljajic June 2012
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameLibrary
{
    public class DrawCardEventArgscs : EventArgs
    {
        private int a_cardsLeft;
        private Card a_card;

        public DrawCardEventArgscs(Card card)
        {
            a_card = card;
        }
        public Card aCard
        {
            get { return a_card; }
            set { a_card = value; }
        }
        public int CardsLeft
        {
            get { return a_cardsLeft; }
            set { a_cardsLeft = value;}
        }
    }
}
