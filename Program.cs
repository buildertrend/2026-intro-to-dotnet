// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Data.SqlTypes;

namespace RpsWorkshop;

enum Choice
{
    Rock,
    Paper,
    Scissors
}

public class Program
{
    private static int userScore = 0;
    private static int computerScore = 0;
    private static int ties = 0;

    public static void Main()
    {

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();
        Console.WriteLine("How many rounds do you want to play?");
        string roundInput = Console.ReadLine() ?? "";
        int rounds;
        while(!(int.TryParse(roundInput, out rounds))){
            Console.WriteLine("Not a valid integer. How many rounds do you want to play?");
            roundInput = Console.ReadLine() ?? "";
        }
        Console.WriteLine();

        for(int i = 0; i < rounds; i++){
        Choice playerChoice = GetPlayerChoice();
        Choice computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        UpdateScore(result);
        Console.WriteLine(result);
        Console.WriteLine("Scores:");
        Console.WriteLine("You: " + userScore);
        Console.WriteLine("Computer: " + computerScore);
        Console.WriteLine("Ties: " + ties);
        }
    }

    public static void UpdateScore(string result)
    {
        if (result.Contains("Computer")){
            computerScore++;
        } else if (result.Contains("You")){
            userScore++;
        } else {
            ties++;
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Choice GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        string trimmed = input.Trim().ToLower();
        if (Enum.TryParse<Choice>(trimmed, ignoreCase: true, out Choice choice))
        {
            return choice;
        }
        Console.WriteLine("Invalid choice. Defaulting to rock.");
        return Choice.Rock; // Default to rock if input is invalid. 
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Choice GetComputerChoice()
    {
        Random random = new Random();
        int index = random.Next(Enum.GetValues(typeof(Choice)).Length);
        return (Choice)index;
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(Choice player, Choice computer)
    {
        if (player == computer)
        {
            return "It's a tie!";
        }

        bool playerWins =
            (player == Choice.Rock && computer == Choice.Scissors) ||
            (player == Choice.Paper && computer == Choice.Rock) ||
            (player == Choice.Scissors && computer == Choice.Paper);

        return playerWins ? "You win!" : "Computer wins!";
    }
}
