using System.Numerics;

namespace AW_Blackjack
{
    public class GamePlay
    {
        public int Round { get; set; }
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

                        cardsValue = CalculateTotal(player.Cards);
                        RenderHand(player, true, 1800);
                        if (cardsValue == 21)
                        {
                            Console.WriteLine($"\n\t{player} wins with Blackjack!\n");
                            isOver = true;
                            break;
                        }
                    }
                    else
                    {
                        RenderHand(player);
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
                        RenderHand(Players[0]);
                        RenderHand(player, true);

                        Console.WriteLine($"\tWhat would you like to do?" +
                            $"\n\t[H]it");
                        var playerChoice = Console.ReadKey().KeyChar.ToString();

                        if (!string.IsNullOrWhiteSpace(playerChoice))
                        {
                            if (playerChoice.ToLower() == "h")
                            {
                                player.Cards.Add(Deck.Draw());
                                player.Cards[player.Cards.Count - 1].Flip();
                                RenderHand(Players[0]);
                                RenderHand(player, true);
                                if (CalculateTotal(player.Cards) == 21)
                                {
                                    Console.WriteLine($"\t{player} has won the game!");
                                    isOver = true;
                                    break;
                                }
                                else if (CalculateTotal(player.Cards) > 21)
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

        public static void RenderHand(Player player, bool calculateSum = false, int sleepTimer = 0)
        {
            Console.WriteLine($"\n\t ***** {player}'s Hand *****\n");

            List<string> cardsByLine = new();

            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < 8; i++)
            {
                var faceLine = string.Empty;
                foreach (Card card in player.Cards)
                {
                    var faceStringList = card.Render();
                    faceLine += faceStringList[i];
                }
                cardsByLine.Add(faceLine);
            }

            foreach (string faceLines in cardsByLine)
            {
                Console.WriteLine(faceLines);
            }
            Console.ForegroundColor = ConsoleColor.White;

            if (calculateSum)
            {
                Console.WriteLine($"\n\tTotal value of hand: {CalculateTotal(player.Cards)}");
            }
            Thread.Sleep(sleepTimer);
        }
        public static int CalculateTotal(List<Card> hand)
        {
            int sum = 0;

            if (hand.Count == 2)
            {
                if ((hand[0].Value == Value.Ace && hand[1].Value > (Value)9) || (hand[1].Value == Value.Ace && hand[0].Value > (Value)9))
                {
                    return 21;
                }
            }

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Value > (Value)10)
                {
                    sum += 10;
                }
                else
                {
                    sum += (int)hand[i].Value;
                }
            }

            return sum;
        }
    }
}