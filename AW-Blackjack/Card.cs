namespace AW_Blackjack
{
    public class Card
    {
        public Suit Suit { get; }
        public Value Value { get; }
        public bool IsFaceUp { get; private set; }
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }
        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }

        public List<string> Render()
        {
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

            if (IsFaceUp)
            {
                cardRenders[1] = cardRenders[1].Remove(4, Value.ToString().Count()).Insert(4, Value.ToString());
                cardRenders[6] = cardRenders[6].Remove(3, Suit.ToString().Count()).Insert(3, Suit.ToString());
            }

            return cardRenders;
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