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

            //There is no application logic. 
            //This seems to be an infnite loop
            //also because of the error in the constructors
            //above this code is never reached
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

            //Innovative way to create all the cards
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
            //Not neccessary to compare a boolean 
            //if(IsEmpty()) works too
            if (IsEmpty() == false)
            {
                return cards.Pop();

            }
            //Is this an exception or is this a normal
            //state of application operation? How is this exception 
            //handled?
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
        //What happens if the player implementation changes the 
        //rank?
        public int Rank { get; set; }


        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Rank == c2.Rank;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            return c1.Rank != c2.Rank;
        }
        ///Are these operators used in the application?
        public static bool operator <(Card c1, Card c2)
        { return c1.Rank < c2.Rank; }
        ///Are these operators used in the application?
        public static bool operator >(Card c1, Card c2)
        { return c1.Rank > c2.Rank; }


    }


    class Player
    {
        ///This constructor trhows an exception
        public Player(Deck dk)
        {
            //Do players grab the top 26 cards or are the 
            //cards dealt round robin? How do we know 
            //how many cards are in the Deck
            for (int i = 0; i < 26; i++)
            {
                var temp = dk.GetTopCard();

                playerCards.Push(temp);

            }
        }

        public Card DrawACard()
        {
            //Is is necessary to shuffle the cards
            //for each draw?
            playerCards.Shuffle();
            return playerCards.Pop();
        }
        ///Playercards is never initialized
        ///Usually Properties in .NET would be PascalCase
       public Stack<Card> playerCards { get; set; }
        ///What is points_accumulated? This is a privater
        ///member that is not referecend
       private int points_accumulated;
    
    }


    public static class WarGameExtensions
    {
        ///Does shuffle belong as a method in Deck or as en extension method?
        public static void Shuffle<Card>(this Stack<Card> cards)
        {
            //Can the shuffle be done inplace or is a clone
            //of the array necessary?
            var cardValues = cards.ToArray();
            cards.Clear();
            //What does the documentation for Random read
            //in regards to the default constructor?
            var rnd = new Random(DateTime.Now.Millisecond);

            foreach (var cv in cardValues.OrderBy(x => rnd.Next(cards.Count)))
                cards.Push(cv);


        }

    }
}
