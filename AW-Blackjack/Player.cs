namespace AW_Blackjack
{
    public class Player
    {
        public Role PlayerRole { get; set; }
        public List<Card> Cards { get; set; } = new();

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