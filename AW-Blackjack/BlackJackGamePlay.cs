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

            while (!isOver)
            {
                var eliminatedPlayers = 0;
                foreach (Player player in Players)
                {
                    if (player.IsEliminated)
                        eliminatedPlayers++;
                }
                if (eliminatedPlayers >= playerNumber){
                    isOver = true;
                    Render.WriteColouredText("\tALL PLAYERS HAVE BEEN ELIMINATED. HOUSE WINS.", ConsoleColor.Yellow, ConsoleColor.DarkRed);
                    var x = Console.ReadKey().KeyChar;
                    break;
                }
                RoundNext();
            }


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
                            break;
                        }
                        Render.Write(">> Press any key to continue", false);
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Render.RenderHand(player);
                        Render.Write(">> Press any key to continue", false);
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }

            void RoundNext()
            {
                foreach (Player player in Players)
                {
                    Console.Clear();
                    if (player.PlayerRole != Role.Dealer && player.IsEliminated == false)
                    {
                        Render.WriteColouredText($"[ {player}'s turn ]", ConsoleColor.Black, (ConsoleColor)player.PlayerRole + 1, true, true);
                        Render.RenderHand(Players[0]);
                        Render.RenderHand(player);
                        Render.Write("Total value is " + CalculationTools.CalculateTotal(player.Cards));

                        Render.Write($"What would you like to do?");
                            Render.WriteColouredText("[H]it", ConsoleColor.Magenta);
                        var playerChoice = Console.ReadKey().KeyChar.ToString();
                        Console.Clear();

                        if (!string.IsNullOrWhiteSpace(playerChoice))
                        {
                            if (playerChoice.ToLower() == "h")
                            {
                                player.Cards.Add(Deck.Draw());
                                player.Cards[player.Cards.Count - 1].Flip();
                                Render.WriteColouredText($"[ {player}'s turn ]", ConsoleColor.Black, (ConsoleColor)player.PlayerRole+1, true, true);
                                Render.RenderHand(Players[0]);
                                Render.RenderHand(player);
                                var cardsValue = CalculationTools.CalculateTotal(player.Cards);
                                Render.Write("Total value is "+ cardsValue);
                                if (cardsValue == 21)
                                {
                                    var winText = $"\n\t - {player} has won the game! - \n";
                                    Render.WriteColouredText(winText.ToUpper(), ConsoleColor.Black, ConsoleColor.DarkGreen);
                                    isOver = true;
                                    break;
                                }
                                else if (cardsValue > 21)
                                {
                                    Render.Write($"{player} has been eliminated!");
                                    player.IsEliminated = true;
                                }
                                Render.Write(">> Press any key to continue", false);
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                }
            }
            Console.Clear();
        }
    }
}