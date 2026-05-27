// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System;
using System.IO;
namespace RpsWorkshop;

public class Program
{

    public static List<Game> games = new List<Game>();

    public class Game{
        public string? PlayerChoice { get; set; }
        public string? ComputerChoice { get; set; }  
        public bool? win { get; set; }
    }

    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        var playing = true;

        while(playing){

            var game = new Game();

            game.PlayerChoice = GetPlayerChoice();
            if (game.PlayerChoice == "q"){
                playing = false;
                continue;
            }
            game.ComputerChoice = GetComputerChoice();

            if (game.PlayerChoice == "winrate"){
                double winRate = CalculateWinRate(games);
                Console.WriteLine($"Your win rate is: {winRate:F2}%");
                return;
            }

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
        var result = "";
        if (player == computer)
        {
            result = "It's a tie!";
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");

        if (result == "")
        {
            result = playerWins ? "You win!" : "Computer wins!";
        }

        LogGame(player, computer, result);
        return result;
    }

    private static void LogGame(string player, string computer, string result)
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
}
