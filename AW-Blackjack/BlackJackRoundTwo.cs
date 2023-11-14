namespace AW_Blackjack
{
    public class BlackJackRoundTwo
    {
        public static void Initialise(DeckOfCards Deck, List<Player> Players, ref bool isGameOver)
        {
            foreach (Player player in Players)
            {
                int cardsValue;

                player.Cards.Add(Deck.Draw());

                if (player.PlayerRole != Role.Dealer)
                {
                    player.Cards[1].Flip();

                    Render.RenderHand(player);
                    cardsValue = CalculationTools.CalculateTotal(player.Cards);
                    if (cardsValue == 21)
                    {
                        var winText = $"\n\t ♠  ♦  ♣  ♥ {player.ToString().ToUpper()} WINS WITH BLACKJACK! ♠  ♦  ♣  ♥ \n";
                        Render.WriteColouredText(winText.ToUpper(), ConsoleColor.DarkMagenta, ConsoleColor.Green);
                        isGameOver = true;
                        Render.ContinueAfterInput();
                        break;
                    }
                    Render.ContinueAfterInput();
                }
                else
                {
                    Render.RenderHand(player);
                    Render.ContinueAfterInput();
                }
            }
        }

    }
}