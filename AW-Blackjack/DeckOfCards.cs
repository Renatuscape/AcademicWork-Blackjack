﻿namespace AW_Blackjack
{
    public class DeckOfCards
    {
        public List<Card> Cards { get; set; } = new();
        Random cardRandomiser = new Random();
        public bool blackJackRules = false;
        public DeckOfCards(int deckMultiplier = 1, bool useBlackJackRules = false)
        {
            if (useBlackJackRules)
            {
                blackJackRules = true;
            }
            CreateCards();

            if (deckMultiplier > 0)
            {
                for (int i = 0; i <= deckMultiplier; i++)
                {
                    Shuffle();
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

        public List<Card> Draw(int drawNumber)
        {
            List<Card> drawnCards = new();

            for (int i = 0; i < drawNumber; i++)
            {
                drawnCards.Add(Draw());
            }
            return drawnCards;
        }

        public Card Draw()
        {
            var drawnCard = Cards[0];
            Cards.Remove(Cards[0]);
            return drawnCard;
        }

        void CreateCards()
        {
            for (int cardSuit = 0; cardSuit <= 3; cardSuit++) //run for each suit
            {
                for (int cardValue = 1; cardValue <= 13; cardValue++)
                {
                    Card newCard = new((Suit)cardSuit, (Value)cardValue);
                    if (blackJackRules)
                    {
                        newCard.blackJackRules = true;
                    }
                    Cards.Add(newCard);
                }
            }
        }
    }
}