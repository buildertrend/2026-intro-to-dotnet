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
    public static string statsFilePath = "stats.txt";
    public static int playerWins = 0;
    public static int computerWins = 0;
    public static int roundTies = 0;
    public static void Main()
    {
        string time = DateTime.Now.ToString();

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        Console.Write("How many rounds do you wish to play?: ");
        int N = GetNumberOfRounds();
        int win = (N/2) + 1;
        

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
        Console.ResetColor();
        File.AppendAllText(filepath, ", Result: " + result + "\n");
        if(result.Equals("You win!"))
        {
            playerWins+=1;
            EditStats(statsFilePath, "playerWin");
        }
        else if(result.Equals("Computer wins!"))
        {
            computerWins+=1;
            EditStats(statsFilePath, "playerLoss");
        }
        else{
            roundTies+=1;
            EditStats(statsFilePath, "tie");
        }
        Console.WriteLine($"Your Wins: {playerWins}  Computer Wins: {computerWins}  Ties: {roundTies}  Out of {N} rounds");
       } 
       string winner = (playerWins == win) ? "Player" : "Computer";
       if(winner.Equals("Player"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
       Console.WriteLine($"{winner} wins the game!");
       Console.ResetColor();
        
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
        else if (input == "stats")
        {
            var (wins, losses, ties) = ReadStats(statsFilePath);
            Console.WriteLine($"Your Wins: {wins}  Computer Wins: {losses}  Ties: {ties}\n\n");
        } 
        else
        {
            Console.WriteLine("\nPlease ensure your choice is spelled correctly with no extra symbols.");
        }
        
        return false;
        
    }

    //Initializes a stats.txt if it does not yet exist.
    private static void InitializeStats(string statsFilePath)
    {
        if(!File.Exists(statsFilePath))
        {
            File.WriteAllText(statsFilePath, "0,0,0");
        }
    }

    //Reads the values of the stats file. Wins,Losses,Ties.
    private static (int wins, int losses, int ties) ReadStats(string statsFilePath)
    {
        InitializeStats(statsFilePath);
        string line = File.ReadAllText(statsFilePath).Trim();
        string[] parts = line.Split(',');

        int wins = int.Parse(parts[0]);
        int losses = int.Parse(parts[1]);
        int ties = int.Parse(parts[2]);

        return (wins, losses, ties);
    }

    //Edits the value of the stats.txt file corresponding to the result of the round.
    private static void EditStats(string statsFilePath, string result)
    {
        InitializeStats(statsFilePath);
        var (wins, losses, ties) = ReadStats(statsFilePath);
        switch (result)
        {
            case "playerWin":
                wins+=1;
                break;
            case "playerLoss":
                losses+=1;
                break;
            case "tie":
                ties+=1;
                break;
        }

        File.WriteAllText(statsFilePath, $"{wins},{losses},{ties}");
    }


    // Returns a string describing who won this round.
    private static string DetermineWinner(string player, string computer)
    {
        if (player == computer)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return "It's a tie!";
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");

        if(playerWins)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        return playerWins ? "You win!" : "Computer wins!";
    }
}
