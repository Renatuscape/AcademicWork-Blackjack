namespace AW_Blackjack
{
    public enum Role
    {
        Dealer,
        Player1,
        Player2,
        Player3,
        Player4
    }

    public class Player
    {
        public Role PlayerRole { get; set; }
        public List<Card> Cards { get; set; } = new();
        public bool IsEliminated { get; set; } = false;

        public Player(Role role)
        {
            PlayerRole = role;
        }
        public override string ToString()
        {
            if (PlayerRole > 0)
            {
                var roleName = PlayerRole.ToString();
                string roleNumber = roleName[roleName.Length - 1].ToString();

                return $"{PlayerRole.ToString().Replace(roleNumber, " " + roleNumber)}";
            }
            else
            {
            return $"{PlayerRole}";
            }

        }
    }
}