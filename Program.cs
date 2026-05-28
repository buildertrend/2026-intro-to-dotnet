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

    public static string filepath = "history.txt";

    public static void Main()
    {
        string time = DateTime.Now.ToString();

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        Console.Write("How many rounds do you wish to play?: ");
        int N = GetNumberOfRounds();
        int win = (N/2) + 1;
        int playerWins = 0;
        int computerWins = 0;

        while((playerWins < win) && (computerWins < win))
       {
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
        Console.WriteLine(result + " This round...");
        File.AppendAllText(filepath, ", Result: " + result + "\n");
        if(result.Equals("You win!"))
        {
            playerWins+=1;
        }
        else if(result.Equals("Computer wins!"))
        {
            computerWins+=1;
        }
        Console.WriteLine($"Your Wins: {playerWins}  Computer Wins: {computerWins}   Out of {N} rounds");
       } 
       string winner = (playerWins == win) ? "Player" : "Computer";
       Console.WriteLine($"{winner} wins the game!");

        
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

    // Picks an integer, N, for the number of rounds they wish to play.
    private static int GetNumberOfRounds()
    {
        bool success = int.TryParse(Console.ReadLine(), out int pick);
        while(!success)
        {
            Console.WriteLine("\n\nPlease enter a valid integer: ");
            success = int.TryParse(Console.ReadLine(), out pick);
        }
        return pick;
    }

    private static bool ValidateChoice(string input)
    {
        if (input == "rock" || input == "paper" || input == "scissors")
        {
            return true;
        }
        Console.WriteLine("\nPlease ensure your choice is spelled correctly with no extra symbols.");
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
