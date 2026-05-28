// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.IO;
namespace RpsWorkshop;

public class Program
{

    public static List<Game> games = new List<Game>();

    public enum Choice
    {
        rock,
        paper,
        scissors,
        winrate,
        stats,
        q
    }

    public class Game{
        public Choice? PlayerChoice { get; set; }
        public Choice? ComputerChoice { get; set; }  
        public bool? win { get; set; }
    }

    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        while(true){

            var game = new Game();

            game.PlayerChoice = GetPlayerChoice();
            if (game.PlayerChoice == Choice.q){
                break;
            } else if (game.PlayerChoice == Choice.winrate){
                double winRate = CalculateWinRate(games);
                Console.WriteLine();
                Console.WriteLine($"Your win rate is: {winRate:F2}%");
                continue;
            } else if (game.PlayerChoice == Choice.stats){
                printStats();
                continue;
            } else if ((game.PlayerChoice != Choice.rock) && (game.PlayerChoice != Choice.paper) && (game.PlayerChoice != Choice.scissors)){
                Console.WriteLine("Invalid input. Please enter rock, paper, scissors, winrate, stats, or q to quit.");
                continue;
            }

            game.ComputerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {game.PlayerChoice}");
            Console.WriteLine($"Computer played: {game.ComputerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(game.PlayerChoice, game.ComputerChoice);
            if (result == "You win!") game.win = true;

            Program.games.Add(game);
            
            Console.WriteLine(result);
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Choice? GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors, winrate, stats, q): ");
        string? input = Console.ReadLine().Trim().ToLower() ?? "";
        if (Enum.TryParse(input, out Choice choice) && input != null)
        {
            return choice;
        } 
        return null;
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Choice? GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors" };
        Random random = new Random();
        int index = random.Next(choices.Length);
        if (Enum.TryParse(choices[index], out Choice choice))
        {
            return choice;
        }
        return null;
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(Choice? player, Choice? computer)
    {
        var result = "";
        if (player == computer)
        {
            result = "It's a tie!";
        }

        bool playerWins =
            (player == Choice.rock && computer == Choice.scissors) ||
            (player == Choice.paper && computer == Choice.rock) ||
            (player == Choice.scissors && computer == Choice.paper);

        if (result == "")
        {
            result = playerWins ? "You win!" : "Computer wins!";
        }

        LogGame(player, computer, result);
        return result;
    }

    private static void LogGame(Choice? player, Choice? computer, string result)
    {
        string logEntry = $"{DateTime.Now}: Player - {player}, Computer - {computer}, Result - {result}";
        File.AppendAllText("history.txt", logEntry + Environment.NewLine);
    }

    private static double CalculateWinRate(List<Game> games)
    {
        if (games.Count == 0) return 0;
        var wonGames = games.Where(g => g.win == true).Count();
        return wonGames / (double)games.Count * 100;
    }

    private static void printStats()
    {
        string[] games = File.ReadAllLines("history.txt");
        var win= 0;
        var loss= 0;
        var tie= 0;
        
        foreach (var game in games)
        {
            switch (game)
            {
                    case var g when g.Contains("You win!"):
                        win++;
                        break;
                    case var g when g.Contains("Computer wins!"):
                        loss++;
                        break;
                    case var g when g.Contains("It's a tie!"):
                        tie++;
                        break;
                    default:
                        break;
            };
        }

        Console.WriteLine();
        Console.WriteLine("=== Statistics ===");
        Console.WriteLine($"Wins: {win}");
        Console.WriteLine($"Losses: {loss}");
        Console.WriteLine($"Ties: {tie}");
    }

}
