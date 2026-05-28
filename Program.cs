// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Runtime.Versioning;

namespace RpsWorkshop;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        Console.WriteLine("How many rounds do you want to play?: ");
        string rounds = Console.ReadLine() ?? "1";

        if (int.TryParse(rounds, out int result))
        {
            Console.WriteLine($"Got it! Starting round 1 of {result}");
        } else {
            Console.WriteLine("That's not a number, silly...");
        }

        for (int i = 0; i < result; i++)
        {
            string playerChoice = GetPlayerChoice();
            string computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string winner = DetermineWinner(playerChoice, computerChoice);

            if (winner.Equals("You win!")) {
                Console.ForegroundColor = ConsoleColor.Green;
            } else if (winner.Equals("It's a tie!")) {
                Console.ForegroundColor = ConsoleColor.Yellow;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(winner);
            Console.ResetColor();

        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors, lizard, spock): ");
        string input = Console.ReadLine() ?? "";

        bool valid = false;
        input = input.Trim().ToLower(); 

        while (!valid) {

            if (String.Equals(input, "paper") || String.Equals(input, "scissors") || String.Equals(input, "rock") || String.Equals(input, "lizard") || String.Equals(input, "spock"))
            {
                break;
            }

            Console.Write("That's not an option, silly! Try again. ");
            Console.WriteLine();
            Console.Write("Enter your choice (rock, paper, scissors, lizard, spock): ");
            input = Console.ReadLine() ?? "";
            input = input.Trim().ToLower();
        }

        return input;
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static string GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors", "lizard", "spock" };
        Random random = new Random();
        int index = random.Next(choices.Length);
        return choices[index];
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(string player, string computer)
    {
        if (player == computer)
        {
            return "It's a tie!";
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper") ||
            (player == "rock" && computer == "lizard") ||
            (player == "lizard" && computer == "spock") ||
            (player == "spock" && computer == "scissors") ||
            (player == "scissors" && computer == "lizard") ||
            (player == "lizard" && computer == "paper") ||
            (player == "paper" && computer == "spock") ||
            (player == "spock" && computer == "rock");

        return playerWins ? "You win!" : "Computer wins!";
    }
}
