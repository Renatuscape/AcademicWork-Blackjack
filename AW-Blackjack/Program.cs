namespace AW_Blackjack
{
    public class Program
    {
        static void Main()
        {
            DeckOfCards deck = new();
        }
    }

    public enum Suit
    {
        Spades,
        Diamonds,
        Hearts,
        Clubs
    }
    public enum Value
    {
        None,
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}