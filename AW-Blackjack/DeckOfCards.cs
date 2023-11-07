namespace AW_Blackjack
{
    public class DeckOfCards
    {
        public int MaxCards { get; } = 52;
        public List<Card> Cards { get; set; } = new();
        Random cardRandomiser = new Random();
        public DeckOfCards()
        {
            CreateCards();
            Shuffle();
        }
        void CreateCards()
        {
            for (int cardSuit = 0; cardSuit <= 3; cardSuit++) //run for each suit
            {
                for (int cardValue = 1; cardValue <= 13; cardValue++)
                {
                    Cards.Add(new((Suit)cardSuit, (Value)cardValue));
                }
            }
        }
        public void Shuffle()
        {
            List<Card> shuffleDeck = Cards;
            Cards = new();

            for (int i = 0; i <= shuffleDeck.Count; i = 1)
            {
                int randomIndex = cardRandomiser.Next(shuffleDeck.Count);
                Cards.Add(shuffleDeck[randomIndex]);
                shuffleDeck.RemoveAt(randomIndex);
            }
        }

        public Card[] Draw(int drawNumber)
        {
            Card[] drawnCards = new Card[drawNumber];
            for (int i = 0; i < drawNumber; i++)
            {
                var drawnCard = Cards[i];
                Cards.Remove(Cards[i]);
                drawnCards[i] = drawnCard;
            }
            return drawnCards;
        }
    }
}