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
    }
}