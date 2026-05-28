// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Diagnostics;

namespace RpsWorkshop;

public class Program
{
    public static void Main()
    {   
        bool playagain = true;
        List<int> wintielost = new List<int> { 0, 0, 0 };
        SetupFileSystem();
        while(playagain){
            Console.WriteLine("=== Rock Paper Scissors ===");
            Console.WriteLine();
            string playerChoice = GetPlayerChoice(wintielost);
            string computerChoice = GetComputerChoice();
            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();
            string result = DetermineWinner(playerChoice, computerChoice);
            UpdateWinTieLoss(wintielost, result);
            Console.WriteLine(result + "     " + GetStats(wintielost));
            Console.WriteLine("Play again? (y/n)");
            string playagaininput = Console.ReadLine() ?? "";
            if (playagaininput.Trim().ToLower() != "y"){
                UpdateFileSystem(wintielost);
                playagain = false;
            }
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static void UpdateFileSystem(List<int> wintielost){
        string path = "rps_stats.txt";
        string existingStats = File.ReadAllText(path);
         string[] parts = existingStats.Split(' ');
        int existingWins = int.Parse(parts[1]);
        int existingTies = int.Parse(parts[3]);
        int existingLosses = int.Parse(parts[5]);
        wintielost[0] += existingWins;
        wintielost[1] += existingTies;
        wintielost[2] += existingLosses;
        string stats = GetStats(wintielost);

        File.WriteAllText(path, stats);
    }
    private static void SetupFileSystem()
    {
        string path = "rps_stats.txt";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "Wins: 0 Ties: 0 Losses: 0");
            }
    }
    private static List<int> UpdateWinTieLoss(List<int> wintielost, string result){
        if (result == "You win!"){
                wintielost[0]++;
            } else if (result == "It's a tie!"){
                wintielost[1]++;
            } else {
                wintielost[2]++;
            }
        //UpdateFileSystem(wintielost);
        return wintielost;
    }
    private static string GetStats(List<int> wintielost)
    {
        return $"Wins: {wintielost[0]} Ties: {wintielost[1]} Losses: {wintielost[2]}";
    }
    private static string GetPlayerChoice(List<int> wintielost)
    {
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        if (input.Trim().ToLower() != "rock" && input.Trim().ToLower() != "paper" && input.Trim().ToLower() != "scissors" && input.Trim().ToLower() != "stats" && input.Trim().ToLower() != "alltimestats"){
            Console.WriteLine("Invalid input, defaulting to rock.");
            return "rock";
        }
        if (input.Trim().ToLower() == "stats"){
            Console.WriteLine(GetStats(wintielost));
            return GetPlayerChoice(wintielost);
        }
        if (input.Trim().ToLower() == "alltimestats"){
            string path = "rps_stats.txt";
            string existingStats = File.ReadAllText(path);
            Console.WriteLine(existingStats);
            return GetPlayerChoice(wintielost);
        }
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
        return playerWins ? $"You win!" : $"Computer wins!";
    }
}
