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

            for (int i = 0; i <= playerNumber; i++)
            {
                Players.Add(new((Role)i));
                Console.WriteLine(Players[i] + " added.");
                Console.WriteLine();
            }

            GameSetup();

            void GameSetup()
            {
                foreach (Player player in Players)
                {
                    int cardsValue;
                    Console.WriteLine($"\n\n ***** {player}'s Hand *****\n");
                    player.Cards.Add(Deck.Draw());
                    player.Cards[0].Flip();
                    player.Cards[0].Render();

                    player.Cards.Add(Deck.Draw());
                    if (player.PlayerRole != Role.Dealer)
                    {
                        player.Cards[1].Flip();
                    }
                    player.Cards[1].Render();
                    if (player.PlayerRole != Role.Dealer)
                    {
                        cardsValue = CalculateTotal(player.Cards);
                        if (cardsValue == 21) {
                            Console.WriteLine($"{player} has BLACKJACK!");
                            break;
                        }
                        Console.WriteLine($"Total value of hand: {CalculateTotal(player.Cards)}");
                    }
                }
            }
        }
        public static int CalculateTotal(List<Card> hand)
        {
            int sum = 0;

            if (hand.Count == 2)
            {
                if (hand[0].Value == Value.Ace && hand[1].Value > (Value)9)
                {
                    sum = 21;
                }
            }

            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Value > (Value)10)
                {
                    sum+= 10;
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