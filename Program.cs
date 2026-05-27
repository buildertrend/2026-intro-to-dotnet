// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;
using System.IO;

public class Program
{
    public static void Main()
    {
        string filepath = "history.txt";
        string time = DateTime.Now.ToString();

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        string playerChoice = string.Empty;
        while (!ValidateChoice(playerChoice))
        {
            playerChoice = GetPlayerChoice();
        } 
        string computerChoice = GetComputerChoice();

        Console.WriteLine();

        File.AppendAllText(filepath, time + " ");
        Console.WriteLine($"You played:      {playerChoice}");
        File.AppendAllText(filepath, "You Played: " + playerChoice);
        Console.WriteLine($"Computer played: {computerChoice}");
        File.AppendAllText(filepath, ", Computer Played: " +  computerChoice);
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
        File.AppendAllText(filepath, ", Result: " + result + "\n");
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        return input.Trim().ToLower();
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static string GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors" };
        Random random = new Random();
        int index = random.Next(choices.Length);
        return choices[index];
    }

    private static bool ValidateChoice(string input)
    {
        if (input == "rock" || input == "paper" || input == "scissors")
        {
            return true;
        }
        Console.WriteLine("Please ensure your choice is spelled correctly with no extra symbols.");
        return false;
        
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
            (player == "scissors" && computer == "paper");

        return playerWins ? "You win!" : "Computer wins!";
    }
}
