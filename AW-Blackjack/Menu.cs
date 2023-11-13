namespace AW_Blackjack
{
    public static class Menu
    {
        public static void Initialise()
        {
            while (true)
            {
                Render.Write("Choose game: (only BlackJack for now)");
                Render.Write("");
                Render.WriteColouredText("\t[0] BlackJack", ConsoleColor.Cyan);
                Render.WriteColouredText("\t[1] Hi-Lo", ConsoleColor.Cyan);
                Render.WriteColouredText("\t[X] Exit", ConsoleColor.Magenta);
                Render.Write("");
                Render.Write("\t", false);
                var choice = Console.ReadKey().KeyChar.ToString();
                if (choice.ToLower() == "x")
                {
                    break;
                }
                Console.Clear();
                BlackJackInterface();
            }
        }
        public static void BlackJackInterface()
        {
            Render.Write("Welcome to BlackJack! (1 - 4 players)");
            Render.Write("Please enter number of players:");
            Render.Write("", false);
            var choice = Console.ReadKey().KeyChar.ToString();
            Render.Write("");

            if (int.TryParse(choice, out var playerCount))
            {
                BlackJackGamePlay game = new();
                game.NewGame(playerCount);
            }
            else
            {
                Render.Write("No valid player choice detected. Press any key to return to menu.", false);
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}