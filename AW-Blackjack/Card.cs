namespace AW_Blackjack
{
    public class Card
    {
        public Suit Suit { get; }
        public Value Value { get;}
        public bool IsFaceUp { get; set; }
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }
    }
}