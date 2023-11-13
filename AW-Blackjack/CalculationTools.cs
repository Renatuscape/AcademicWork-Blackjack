namespace AW_Blackjack
{
    public static class CalculationTools
    {
        public static int CalculateTotal(List<Card> hand)
        {
            int sum = 0;

            if (hand.Count == 2)
            {
                if ((hand[0].Value == Value.Ace && hand[1].Value > (Value)9) || (hand[1].Value == Value.Ace && hand[0].Value > (Value)9))
                {
                    return 21;
                }
            }

            for (int i = 0; i < hand.Count; i++)
            {
                sum = hand[i] + sum;
            }

            return sum;
        }
    }
}