namespace AW_Blackjack
{
    public static class Menu
    {
        public static void Initialise()
        {
            BlackJackInterface();
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
                GamePlay game = new();
                game.NewGame(playerCount);
            }
        }
    }
}