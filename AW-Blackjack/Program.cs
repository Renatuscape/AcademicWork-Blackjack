namespace AW_Blackjack
{
    public class Program
    {
        static void Main()
        {
            DeckOfCards deck = new();

            foreach (Card c in deck.Draw(5))
            {
                Console.WriteLine($"{c.Value} of {c.Suit}");
            }
        }
    }
}