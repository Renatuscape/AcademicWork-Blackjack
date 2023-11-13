using System.Numerics;

namespace AW_Blackjack
{
    public class BlackJackGamePlay
    {
        public List<Player>? Players { get; private set; }
        public DeckOfCards? Deck { get; private set; }

        public void NewGame(int playerNumber)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Players = new();
            Deck = new(6, true);
            bool isOver = false;

            for (int i = 0; i <= playerNumber; i++)
            {
                Players.Add(new((Role)i));
                Render.WriteColouredText(Players[i] + " added.", ConsoleColor.Black, (ConsoleColor)Players[i].PlayerRole + 1);
            }

            RoundOne();
            RoundTwo();
            RoundThree();

            void RoundOne()
            {
                Render.Write(">> Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                foreach (Player player in Players)
                {
                    player.Cards.Add(Deck.Draw());
                    player.Cards[0].Flip();
                }
            }

            void RoundTwo()
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
                            var winText = $"\n\t !=!=! {player} wins with Blackjack! !=!=! \n";
                            Render.WriteColouredText(winText.ToUpper(), ConsoleColor.DarkMagenta, ConsoleColor.Green);
                            isOver = true;
                            Render.ContinueAfterInput();
                            break;
                        }
                        Render.Write(">> Press any key to continue", false);
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Render.RenderHand(player);
                        Render.ContinueAfterInput();
                    }
                }
            }

            void RoundThree()
            {
                int eliminatedPlayers = 0;
                int standingPlayers = 0;

                bool AllPlayersEliminated()
                {
                    if (eliminatedPlayers >= Players?.Count-1)
                    {
                        return true;
                    }
                    return false;
                }
                bool AllPlayersFinished()
                {
                    if (eliminatedPlayers + standingPlayers >= Players?.Count-1)
                    {
                        return true;
                    }
                    return false;
                }

                foreach (Player player in Players)
                {
                    Console.Clear();
                    if (player.PlayerRole != Role.Dealer && !player.IsEliminated && !isOver)
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
                                        isOver = true;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                                    else if (cardsValue > 21)
                                    {
                                        Render.Write($"{player} has been eliminated!");
                                        eliminatedPlayers++;
                                        player.IsEliminated = true;
                                        Render.ContinueAfterInput();
                                    }
                                    //Render.Write(">> Press any key to continue", false);
                                    //Console.ReadKey();
                                    Console.Clear();
                                }
                                if (playerChoice.ToLower() == "s")
                                {
                                    stand = true;
                                    standingPlayers++;
                                    Render.Write($"{player} has chosen to stand with a total value of " + CalculationTools.CalculateTotal(player.Cards));
                                    Render.ContinueAfterInput();
                                }
                            }
                        }
                    }
                }

                if (!isOver)
                {
                    if (AllPlayersEliminated())
                    {
                        Render.WriteColouredText("\tALL PLAYERS HAVE BEEN ELIMINATED. HOUSE WINS.", ConsoleColor.Yellow, ConsoleColor.DarkRed);
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (AllPlayersFinished())
                    {
                        Render.WriteColouredText($"All players have been served. Players: {standingPlayers}. Bust: {eliminatedPlayers}", ConsoleColor.Cyan);
                        Render.Write("");
                        Render.WriteColouredText($"\tPlayers: {standingPlayers}", ConsoleColor.Cyan);
                        Render.WriteColouredText($"\tBust: {eliminatedPlayers}", ConsoleColor.Cyan);
                        Render.ContinueAfterInput();
                    }
                }
            }
            Console.Clear();
        }
    }
}