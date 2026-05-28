// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public class Program
{
    public static void Main()
    {
        const string WRITE_PATH = "./history.txt";

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        string playerChoice = GetPlayerChoice();
        string computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);

        try
        {
            AppendToFile(WRITE_PATH, playerChoice, computerChoice, result);
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"File append could not be performed: {ex.Message}");
        }
        Console.WriteLine(result);
    }

    // Writes a single line to file given filepath, the player and computer's choice, and the result of the match
    private static void AppendToFile(string filepath, string playerChoice, string computerChoice, string result)
    {
        using (StreamWriter sw = new StreamWriter(filepath, append: true))
        {
            sw.WriteLine($"Timestamp: {System.DateTime.Now}, Player Choice: {playerChoice}, Computer Choice: {computerChoice}, Result: {result}");
        }
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
