using System.Numerics;

namespace AW_Blackjack
{
    public class GamePlay
    {
        public List<Player>? Players { get; set; }
        public DeckOfCards? Deck { get; set; }

        public void NewGame(int playerNumber)
        {
            Players = new();
            Deck = new(6);
            bool isOver = false;

            for (int i = 0; i <= playerNumber; i++)
            {
                Players.Add(new((Role)i));
                Console.WriteLine(Players[i] + " added.");
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
                    Console.WriteLine("\n\tALL PLAYERS HAVE BEEN ELIMINATED. HOUSE WINS.");
                    var x = Console.ReadKey().KeyChar;
                    break;
                }
                RoundNext();
            }


            void RoundOne()
            {
                Console.WriteLine("\n\tPress any key to deal!");
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

                        RenderTools.RenderHand(player);
                        cardsValue = CalculationTools.CalculateTotal(player.Cards);
                        if (cardsValue == 21)
                        {
                            Console.WriteLine($"\n\t{player} wins with Blackjack!\n");
                            isOver = true;
                            break;
                        }
                        Console.Clear();
                    }
                    else
                    {
                        RenderTools.RenderHand(player);
                        Console.Clear();
                    }
                }
                Console.WriteLine("\n\t>> Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }

            void RoundNext()
            {
                foreach (Player player in Players)
                {
                    Console.Clear();
                    if (player.PlayerRole != Role.Dealer && player.IsEliminated == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"\n\t[ {player}'s turn ]");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        RenderTools.RenderHand(Players[0]);
                        RenderTools.RenderHand(player);
                        Console.WriteLine("\tTotal value is "+CalculationTools.CalculateTotal(player.Cards));

                        Console.WriteLine($"\tWhat would you like to do?" +
                            $"\n\t[H]it");
                        var playerChoice = Console.ReadKey().KeyChar.ToString();
                        Console.Clear();

                        if (!string.IsNullOrWhiteSpace(playerChoice))
                        {
                            if (playerChoice.ToLower() == "h")
                            {
                                player.Cards.Add(Deck.Draw());
                                player.Cards[player.Cards.Count - 1].Flip();

                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"\n\t[ {player}'s turn ]");
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                RenderTools.RenderHand(Players[0]);
                                RenderTools.RenderHand(player);
                                var cardsValue = CalculationTools.CalculateTotal(player.Cards);
                                Console.WriteLine("\tTotal value is "+ cardsValue);
                                if (cardsValue == 21)
                                {
                                    Console.WriteLine($"\t{player} has won the game!");
                                    isOver = true;
                                    break;
                                }
                                else if (cardsValue > 21)
                                {
                                    Console.WriteLine($"\t{player} has been eliminated!");
                                    player.IsEliminated = true;
                                }
                                Console.WriteLine("\n\t>> Press any key to continue");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                }
            }
        }
    }
}