using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW_Blackjack
{
    public class BlackJackFinalRound
    {
        public static void Initialise(DeckOfCards Deck, List<Player> Players)
        {
            DealersPlay(Deck, Players);
        }

        public static void DealersPlay(DeckOfCards Deck, List<Player> Players)
        {
            var dealer = Players[0];
            foreach (Card card in dealer.Cards)
            {
                card.IsFaceUp = true;
            }
            Render.RenderHand(dealer);
            Render.Write($"{dealer}'s hidden card was {dealer.Cards[dealer.Cards.Count - 1]}");
            var dealerTotal = CalculationTools.CalculateTotal(dealer.Cards);
            Render.ContinueAfterInput();

            while (dealerTotal < 17)
            {
                Render.RenderHand(dealer);
                Render.Write($"{dealer}'s hand is less than 17. {dealer} must draw.");
                Render.ContinueAfterInput();
                var newCard = Deck.Draw();
                newCard.Flip();
                dealer.Cards.Add(newCard);
                Render.RenderHand(dealer);
                Render.Write($"{dealer} drew {newCard}.");
                Render.ContinueAfterInput();

                dealerTotal = CalculationTools.CalculateTotal(dealer.Cards);
            }

            if (dealerTotal > 17 && dealerTotal < 21)
            {
                Render.RenderHand(dealer);
                Render.Write($"{dealer}'s hand is 17 or more. {dealer} must stand with a total value of {dealerTotal}.");
                Render.ContinueAfterInput();
                FinalTally(Players);
            }
            else if (dealerTotal > 21)
            {
                Render.WriteColouredText($" ♠  ♦  ♣  ♥ {dealerTotal}! {dealer.ToString().ToUpper()} IS BUST! ♠  ♦  ♣  ♥ ", ConsoleColor.White, ConsoleColor.DarkGreen);
                Render.Write("");
                Render.Write("\n\tWinner(s):");
                foreach (Player player in Players)
                {
                    if (player.IsStanding)
                    {
                        Render.Write(player.ToString());
                    }
                }
                Render.ContinueAfterInput();
            }
            else if (dealerTotal == 21)
            {
                Render.WriteColouredText(" *** House wins with 21! *** ", ConsoleColor.Red);
                Render.ContinueAfterInput();
            }
        }

        public static void FinalTally(List<Player> Players)
        {
            Render.WriteColouredText(" ♠  ♦  ♣  ♥ Final Tally ♠  ♦  ♣  ♥ \n", ConsoleColor.Red);
            List<Player> scoreList = new();
            foreach (Player player in Players)
            {
                if (!player.IsEliminated)
                {
                    scoreList.Add(player);
                }
            }

            var sortedList = scoreList.OrderBy(Player => CalculationTools.CalculateTotal(Player.Cards)).ToList();

            foreach (Player player in sortedList)
            {
                var totalValue = CalculationTools.CalculateTotal(player.Cards);
                Render.WriteColouredText($"{player}: {totalValue} with {player.Cards.Count} cards", ConsoleColor.Gray);
            }
            Render.ContinueAfterInput();
        }
    }
}
