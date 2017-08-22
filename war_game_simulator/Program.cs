using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace war_game_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var gowDeck = new Deck();

            var p1 = new Player(gowDeck);
            var p2 = new Player(gowDeck);

            while(!gowDeck.IsEmpty())
            {


            }
            
            
        }
    }

    enum suits { diamond, club, heart, spade };

    class Deck
    {
        public Deck()
        {

            cards = new Stack<Card>();

            
            foreach (var cv in Enum.GetValues(typeof(suits)).Cast<suits>().Zip(Enumerable.Range(1, 13), (a, b) => new { sv = a, rv = b }))
            {
                cards.Push(new Card(cv.sv, cv.rv));
            }


            cards.Shuffle();
          

        }

        public bool IsEmpty()
        {
            return cards.Count() <= 0;
        }


        public Card GetTopCard()
        {

            if (IsEmpty() == false)
            {
                return cards.Pop();

            }

            throw new Exception("Empty!");

        }


        

        public Stack<Card> cards { get; set; }


    }


    class Card
    {
        public Card(suits suitValue, int rankValue)
        {
            this.Rank = rankValue;
            this.Suite = suitValue;
        }

        public suits Suite { get; set; }

        public int Rank { get; set; }


        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Rank == c2.Rank;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            return c1.Rank != c2.Rank;
        }

        public static bool operator <(Card c1, Card c2)
        { return c1.Rank < c2.Rank; }


        public static bool operator >(Card c1, Card c2)
        { return c1.Rank > c2.Rank; }


    }


    class Player
    {
        public Player(Deck dk)
        {
            for (int i = 0; i < 26; i++)
            {
                var temp = dk.GetTopCard();

                playerCards.Push(temp);

            }
        }

        public Card DrawACard()
        {
            playerCards.Shuffle();
            return playerCards.Pop();
        }

       public Stack<Card> playerCards { get; set; }

       private int points_accumulated;
    
    }


    public static class WarGameExtensions
    {
        public static void Shuffle<Card>(this Stack<Card> cards)
        {
            var cardValues = cards.ToArray();
            cards.Clear();

            var rnd = new Random(DateTime.Now.Millisecond);

            foreach (var cv in cardValues.OrderBy(x => rnd.Next(cards.Count)))
                cards.Push(cv);


        }

    }
}
