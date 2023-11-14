using AW_Blackjack;
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
            int eliminatedPlayers = 0;
            int standingPlayers = 0;
            bool isGameOver = false;

            for (int i = 0; i <= playerNumber; i++)
            {
                Players.Add(new((Role)i));
                Render.WriteColouredText(Players[i] + " added.", ConsoleColor.Black, (ConsoleColor)Players[i].PlayerRole + 1);
            }

            BlackJackRoundOne.Initialise(Deck, Players);
            BlackJackRoundTwo.Initialise(Deck, Players, ref isGameOver);
            BlackJackRoundThree.Initialise(Deck, Players, ref isGameOver);

            GetPlayerStatus(Players, out eliminatedPlayers, out standingPlayers);

            if (eliminatedPlayers == Players.Count - 1)
            {
                Render.WriteColouredText(" ♠  ♦  ♣  ♥ ALL PLAYERS BUST - HOUSE WINS! ♠  ♦  ♣  ♥ ", ConsoleColor.DarkRed, ConsoleColor.DarkYellow);
                Render.ContinueAfterInput();
            }

            else
            {
                if (!isGameOver)
                {
                    Render.WriteColouredText($"{eliminatedPlayers + standingPlayers} player(s) served.", ConsoleColor.Gray);
                    Render.Write("");
                    Render.WriteColouredText($"\tPlayers: {standingPlayers}", ConsoleColor.Gray);
                    Render.WriteColouredText($"\tBust: {eliminatedPlayers}", ConsoleColor.Red);
                    Render.ContinueAfterInput();
                    BlackJackFinalRound.Initialise(Deck, Players);
                }
            }

            Console.Clear();
        }

        static void GetPlayerStatus(List<Player> Players, out int countEliminated, out int countStanding)
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
            countEliminated = eliminatedPlayers;
            countStanding = standingPlayers;
        }
    }
}