namespace AW_Blackjack
{
    public static class RenderTools
    {
        public static void RenderHand(Player player)
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
        }
    }
}