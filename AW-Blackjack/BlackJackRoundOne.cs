namespace AW_Blackjack
{
    public class BlackJackRoundOne
    {
        public static void Initialise(DeckOfCards Deck, List<Player> Players)
        {
            Render.ContinueAfterInput();
            foreach (Player player in Players)
            {
                player.Cards.Add(Deck.Draw());
                player.Cards[0].Flip();
            }
        }
    }
}