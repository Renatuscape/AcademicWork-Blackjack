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
            bool isGameOver = false;

            for (int i = 0; i <= playerNumber; i++)
            {
                Players.Add(new((Role)i));
                Render.WriteColouredText(Players[i] + " added.", ConsoleColor.Black, (ConsoleColor)Players[i].PlayerRole + 1);
            }

            BlackJackRoundOne.Initialise(Deck, Players);
            BlackJackRoundTwo.Initialise(Deck, Players, ref isGameOver);
            BlackJackRoundThree.Initialise(Deck, Players, ref isGameOver);

            if (!isGameOver)
            {
                AllPlayersFinished(Players, out var eliminatedPlayers, out var standingPlayers);
                Render.WriteColouredText($"{eliminatedPlayers + standingPlayers} player(s) served.", ConsoleColor.Cyan);
                Render.Write("");
                Render.WriteColouredText($"\tPlayers: {standingPlayers}", ConsoleColor.Cyan);
                Render.WriteColouredText($"\tBust: {eliminatedPlayers}", ConsoleColor.Cyan);
                Render.ContinueAfterInput();
            }
            Console.Clear();
        }

        static bool AllPlayersFinished(List<Player> Players, out int countEliminated, out int countStanding)
        {
            int eliminatedPlayers = 0;
            int standingPlayers = 0;

            foreach (Player player in Players)
            {
                if (player.IsEliminated)
                {
                    eliminatedPlayers++;
                }
                else if (player.IsStanding)
                {
                    standingPlayers++;
                }
            }

            if (eliminatedPlayers + standingPlayers >= Players?.Count - 1)
            {
                countEliminated = eliminatedPlayers;
                countStanding = standingPlayers;
                return true;
            }
            countEliminated = eliminatedPlayers;
            countStanding = standingPlayers;
            return false;
        }
    }
}