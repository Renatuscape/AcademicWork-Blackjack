namespace AW_Blackjack
{
    public static class Menu
    {
        public static void Initialise()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Render.WriteColouredText("\t ♠  ♦  ♣  ♥ WELCOME TO", ConsoleColor.Black, ConsoleColor.Gray, false);
                    Render.WriteColouredText(" BLACKJACK ♠  ♦  ♣  ♥ ", ConsoleColor.DarkRed, ConsoleColor.Gray, true, false);
                Render.Write("");
                Render.WriteColouredText("\t[0] Start", ConsoleColor.Gray);
                Render.WriteColouredText("\t[X] Exit", ConsoleColor.Red);
                Render.Write("");
                Render.Write("\t", false);
                var choice = Console.ReadKey().KeyChar.ToString();
                if (choice.ToLower() == "x")
                {
                    break;
                }
                if (choice.ToLower() == "0")
                {
                    Console.Clear();
                    BlackJackInterface();
                }
                Console.Clear();
            }
        }
        public static void BlackJackInterface()
        {
            Render.Write("This game is designed for 1 - 5 players.");
            Render.Write("Please enter number of players:");
            Render.Write("", false);
            var choice = Console.ReadKey().KeyChar.ToString();
            Render.Write("");

            if (int.TryParse(choice, out var playerCount))
            {
                if (playerCount < 1 || playerCount > 5)
                {
                    Render.Write("Players must be between 1 and 5.", false);
                    Render.ContinueAfterInput();
                }
                else
                {
                    BlackJackGamePlay game = new();
                    game.NewGame(playerCount);
                }

            }
            else
            {
                Render.Write("No valid player choice detected. Press any key to return to menu.", false);
                Render.ContinueAfterInput();
            }
        }
    }
}