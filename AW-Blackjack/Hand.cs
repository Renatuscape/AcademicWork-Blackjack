namespace AW_Blackjack
{
    public class Hand
    {
        public Role Role { get; set; }
        public List<Card> Cards { get; set; } = new();

        public Hand(Role role)
        {
            Role = role;
        }

        public void RenderCards()
        {
            foreach (Card card in Cards)
            {
                Console.WriteLine(" --------- ");
                Console.WriteLine("|  _____  |");
                Console.WriteLine("| |     | |");
                if (!card.IsFaceUp)
                {
                    Console.WriteLine("| |     | |");
                }
                else
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine("| |     | |");
                Console.WriteLine("| |     | |");
                Console.WriteLine("|  -----  |");
                Console.WriteLine(" --------- ");
            }
        }
    }
}