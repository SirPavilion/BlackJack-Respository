using System;
using System.Collections.Generic;

class BlackjackGame
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");  // Move content down
        CenterText("Welcome to Multiplayer Blackjack!");
        Console.ResetColor();
        CenterText("Enter number of players (2-4): ");
        int playerCount;

        while (!TryReadCenteredInput(out playerCount) || playerCount < 2 || playerCount > 4)
        {
            CenterText("Invalid input. Please enter a number between 2 and 4: ");
        }

        PlayBlackjack(playerCount);
    }

    static void PlayBlackjack(int playerCount)
    {
        List<List<string>> playerHands = new List<List<string>>();
        List<int> playerTotals = new List<int>();
        for (int i = 0; i < playerCount; i++)
        {
            playerHands.Add(new List<string> { DrawCard(), DrawCard() });
            playerTotals.Add(0);
        }

        List<string> dealerHand = new List<string> { DrawCard(), DrawCard() };

        Console.ForegroundColor = ConsoleColor.Yellow;
        CenterText($"\nDealer's revealed card: {dealerHand[0]}");
        CenterText($"Dealer's hidden card: ?");
        Console.ResetColor();

        for (int i = 0; i < playerCount; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            CenterText($"\n--- Player {i + 1}'s Turn ---");
            Console.ResetColor();
            while (true)
            {
                CenterText($"Your hand: {string.Join(", ", playerHands[i])} (Total: {CalculateHandValue(playerHands[i])})");
                CenterText("Would you like to 'hit' or 'stand'? ");
                string choice = ReadCenteredInput()?.ToLower();

                if (choice == "hit")
                {
                    playerHands[i].Add(DrawCard());
                    if (CalculateHandValue(playerHands[i]) > 21)
                    {
                        CenterText($"You busted! Your total is {CalculateHandValue(playerHands[i])}.");
                        break;
                    }
                }
                else if (choice == "stand")
                {
                    playerTotals[i] = CalculateHandValue(playerHands[i]);
                    break;
                }
                else
                {
                    CenterText("Invalid choice. Please enter 'hit' or 'stand'.");
                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        CenterText($"\n--- Dealer's Turn ---");
        Console.ResetColor();
        CenterText($"Dealer's full hand: {string.Join(", ", dealerHand)} (Total: {CalculateHandValue(dealerHand)})");

        while (CalculateHandValue(dealerHand) < 17)
        {
            string newCard = DrawCard();
            dealerHand.Add(newCard);
            CenterText($"Dealer hits and draws: {newCard}");
        }

        int dealerTotal = CalculateHandValue(dealerHand);
        CenterText($"Dealer's final hand: {string.Join(", ", dealerHand)} (Total: {dealerTotal})");

        Console.ForegroundColor = ConsoleColor.Blue;
        CenterText("\n--- Results ---");
        Console.ResetColor();
        for (int i = 0; i < playerCount; i++)
        {
            int playerTotal = CalculateHandValue(playerHands[i]);
            CenterText($"Player {i + 1}'s hand: {string.Join(", ", playerHands[i])} (Total: {playerTotal})");

            if (playerTotal > 21)
            {
                CenterText("Result: Busted!");
            }
            else if (dealerTotal > 21 || playerTotal > dealerTotal)
            {
                CenterText("Result: You win!");
            }
            else if (playerTotal == dealerTotal)
            {
                CenterText("Result: It's a tie!");
            }
            else
            {
                CenterText("Result: Dealer wins!");
            }
        }
    }

    static string DrawCard()
    {
        string[] suits = { "♠", "♥", "♦", "♣" };
        string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        string suit = suits[random.Next(suits.Length)];
        string rank = ranks[random.Next(ranks.Length)];
        return $"{rank}{suit}";
    }

    static int CalculateHandValue(List<string> hand)
    {
        int total = 0;
        int aces = 0;

        foreach (string card in hand)
        {
            string rank = card.Substring(0, card.Length - 1);
            if (int.TryParse(rank, out int value))
            {
                total += value;
            }
            else if (rank == "A")
            {
                total += 11;
                aces++;
            }
            else
            {
                total += 10;
            }
        }

        while (total > 21 && aces > 0)
        {
            total -= 10;
            aces--;
        }

        return total;
    }

    static void CenterText(string text)
    {
        int windowWidth = Console.WindowWidth;
        int stringWidth = text.Length;
        int spaces = (windowWidth - stringWidth) / 2;
        Console.WriteLine(new string(' ', spaces) + text);
    }

    static bool TryReadCenteredInput(out int result)
    {
        CenterText(""); // To center the input prompt itself
        Console.Write(new string(' ', Console.WindowWidth / 2 - 10));  // Adjust padding as needed
        string input = Console.ReadLine();
        CenterText(""); // To center the next input prompt
        return int.TryParse(input, out result);
    }

    static string ReadCenteredInput()
    {
        CenterText(""); // To center the input prompt itself
        Console.Write(new string(' ', Console.WindowWidth / 2 - 10));  // Adjust padding as needed
        string input = Console.ReadLine();
        CenterText(""); // To center the next input prompt
        return input;
    }
}