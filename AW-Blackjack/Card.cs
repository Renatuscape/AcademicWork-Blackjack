namespace AW_Blackjack
{
    public enum Suit
    {
        Spades,
        Clubs,
        Diamonds,
        Hearts
    }
    public enum Value
    {
        None,
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class Card
    {
        public Suit Suit { get; }
        public Value Value { get; }
        public bool IsFaceUp { get; set; }
        public bool blackJackRules = false;
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }
        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }

        public static bool operator ==(Card a, Card b)
        {
            if (a.Value == b.Value)
                return true;
            else return false;
        }

        public static bool operator !=(Card a, Card b)
        {
            if (a.Value == b.Value)
                return true;
            else return false;
        }

        public static int operator +(Card a, Card b)
        {
            int valueA = (int)a.Value;
            int valueB = (int)b.Value;

            if (a.blackJackRules)
            {
                valueA = BlackJacker(a);
            }
            if (b.blackJackRules)
            {
                valueB = BlackJacker(b);
            }

            int BlackJacker(Card card)
            {
                if ((int)card.Value > 10)
                {
                    return 10;
                }
                return (int)card.Value;
            }

            return valueA + valueB;
        }

        public static int operator +(Card a, int b)
        {
            int valueA = (int)a.Value;
            int valueB = b;

            if (a.blackJackRules)
            {
                if (valueA > 10)
                {
                    valueA = 10;
                }
            }

            return valueA + valueB;
        }

        #region Overrides
        public override string ToString()
        {
            if (IsFaceUp)
                return $"{Value} of {Suit}";
            else
                return "Facedown Card";
        }

        public bool Equals(Card other)
        {
            return Suit == other.Suit && Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Card other)
            {
                return Equals(other);
            }

            else
            {
                return false;
            }

        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}