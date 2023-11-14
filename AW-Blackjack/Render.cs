namespace AW_Blackjack
{
    public static class Render
    {

        public static void RenderHand(Player player)
        {
            var playerColour = (ConsoleColor)player.PlayerRole + 1;
            WriteColouredText($"***** {player}'s Hand *****\n", ConsoleColor.Black, playerColour);

            List<string> cardsByLine = new();
            if (playerColour == ConsoleColor.Gray || playerColour == ConsoleColor.White)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else
            {
            Console.BackgroundColor = ConsoleColor.Gray;
            }

            Console.ForegroundColor = (ConsoleColor)player.PlayerRole+1;
            for (int i = 0; i < 8; i++)
            {
                var faceLine = string.Empty;
                foreach (Card card in player.Cards)
                {
                    var faceStringList = RenderCard(card);
                    faceLine += faceStringList[i];
                }
                cardsByLine.Add(faceLine);
            }

            foreach (string faceLines in cardsByLine)
            {
                Console.WriteLine(faceLines);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public static List<string> RenderCard(Card card)
        {
            var suit = FindSuit(card.Suit);
            var value = FindValue(card.Value);

            List<string> cardRenders = new List<string>()
            {
            new("\t --------- "),
            new("\t|  _____  |"),
            new("\t| |     | |"),
            new("\t| |     | |"),
            new("\t| |     | |"),
            new("\t| |     | |"),
            new("\t|  -----  |"),
            new("\t --------- ")
            };

            if (card.IsFaceUp)
            {
                cardRenders[1] = cardRenders[1].Remove(2, value.ToString().Count()).Insert(2, value.ToString());
                cardRenders[6] = cardRenders[6].Remove(2, suit.ToString().Count()).Insert(2, suit.ToString());

                cardRenders[6] = cardRenders[6].Remove(8, value.ToString().Count()).Insert(8, value.ToString());
                cardRenders[1] = cardRenders[1].Remove(8, suit.ToString().Count()).Insert(8, suit.ToString());
            }

            return cardRenders;

            static string FindSuit(Suit suit)
            {
                if (suit == Suit.Spades)
                {
                    return " ♠ ";
                }
                else if (suit == Suit.Diamonds)
                {
                    return " ♦ ";
                }
                else if (suit == Suit.Clubs)
                {
                    return " ♣ ";
                }
                else
                {
                    return " ♥ ";
                }
            }

            static string FindValue(Value value)
            {
                if ((int)value == 1)
                {
                    return " A ";
                }
                else if ((int)value < 11)
                {
                    return $" {(int)value} ";
                }
                else if ((int)value == 11)
                {
                    return " J ";
                }
                else if ((int)value == 12)
                {
                    return " Q ";
                }
                else
                return " K ";
            }
        }

        public static void Write(string toPrint, bool newLine = true, bool isTabbed = true)
        {
            string tabbed = "\t";
            if (!isTabbed)
            {
                tabbed = "";
            }
            if (newLine == true)
            {
            Console.WriteLine(tabbed + toPrint);
            }
            else
            {
                Console.Write(tabbed + toPrint);
            }

        }

        public static void WriteColouredText(string toPrint, ConsoleColor colourForeground, ConsoleColor colourBackground = ConsoleColor.Black, bool newLine = true, bool isTabbed = true)
        {
            string tabbed = "\t";
            if (!isTabbed)
            {
                tabbed = "";
            }
            Console.ForegroundColor = colourForeground;
            Console.BackgroundColor = colourBackground;
            if (newLine == true)
            {
                Console.WriteLine(tabbed + toPrint);
            }
            else
            {
                Console.Write(tabbed + toPrint);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void ContinueAfterInput()
        {
            Write("");
            WriteColouredText(">> Press any key to continue", ConsoleColor.DarkGray, ConsoleColor.Black, false);
            Console.ReadKey();
            Console.Clear();
        }
    }
}