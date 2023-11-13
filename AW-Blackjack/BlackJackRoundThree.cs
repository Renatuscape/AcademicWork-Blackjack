namespace AW_Blackjack
{
    public class BlackJackRoundThree
    {
        public static void Initialise(DeckOfCards Deck, List<Player> Players, ref bool isGameOver)
        {

            foreach (Player player in Players)
            {
                Console.Clear();
                if (player.PlayerRole != Role.Dealer && !player.IsEliminated && !isGameOver)
                {
                    bool stand = false;
                    while (!stand && !player.IsEliminated)
                    {
                        Render.WriteColouredText($"[ {player}'s turn ]", ConsoleColor.Black, (ConsoleColor)player.PlayerRole + 1, true, true);
                        Render.RenderHand(Players[0]);
                        Render.RenderHand(player);
                        Render.Write("Total value is " + CalculationTools.CalculateTotal(player.Cards));

                        Render.Write($"What would you like to do?");
                        Render.WriteColouredText("[H]it", ConsoleColor.Magenta);
                        Render.WriteColouredText("[S]tand", ConsoleColor.Magenta);
                        var playerChoice = Console.ReadKey().KeyChar.ToString();
                        Console.Clear();

                        if (!string.IsNullOrWhiteSpace(playerChoice))
                        {
                            if (playerChoice.ToLower() == "h")
                            {
                                player.Cards.Add(Deck.Draw());
                                player.Cards[player.Cards.Count - 1].Flip();
                                Render.WriteColouredText($"[ {player}'s turn ]", ConsoleColor.Black, (ConsoleColor)player.PlayerRole + 1, true, true);
                                Render.RenderHand(Players[0]);
                                Render.RenderHand(player);
                                var cardsValue = CalculationTools.CalculateTotal(player.Cards);
                                Render.Write("Total value is " + cardsValue);
                                if (cardsValue == 21)
                                {
                                    var winText = $"\n\t - {player} has won the game! - \n";
                                    Render.WriteColouredText(winText.ToUpper(), ConsoleColor.Black, ConsoleColor.DarkGreen);
                                    isGameOver = true;
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }
                                else if (cardsValue > 21)
                                {
                                    Render.Write($"{player} has been eliminated!");
                                    player.IsEliminated = true;
                                    Render.ContinueAfterInput();
                                }
                                Console.Clear();
                            }
                            if (playerChoice.ToLower() == "s")
                            {
                                stand = true;
                                player.IsStanding = true;
                                Render.Write($"{player} has chosen to stand with a total value of " + CalculationTools.CalculateTotal(player.Cards));
                                Render.ContinueAfterInput();
                            }
                        }
                    }
                }
            }
        }
    }
}