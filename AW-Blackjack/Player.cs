namespace AW_Blackjack
{
    public class Player
    {
        public Role Role { get; set; }
        public List<Card> Cards { get; set; } = new();

        public Player(Role role)
        {
            Role = role;
        }
    }
}