// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Data.SqlTypes;
using System;
using System.IO;

namespace RpsWorkshop;

public enum Choice
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
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();
        Console.WriteLine("How many rounds do you want to play?");
        string roundInput = Console.ReadLine() ?? "";
        int rounds;
        while(!(int.TryParse(roundInput, out rounds))){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not a valid integer. How many rounds do you want to play?");
            Console.ForegroundColor = ConsoleColor.White;
            roundInput = Console.ReadLine() ?? "";
        }
        Console.WriteLine();

        for(int i = 0; i < rounds; i++){
        Choice playerChoice = GetPlayerChoice();
        Choice computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You played:      {playerChoice}");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        UpdateScore(result);
        Console.WriteLine(result);
        Console.WriteLine("Scores:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("You: " + userScore);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Computer: " + computerScore);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Ties: " + ties);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        if(result.Contains("tie")){
            i--;
            Console.WriteLine("Tied round, adding another round...");
        }
        updateRoundHistory(playerChoice, computerChoice, result);
        }

        bestOfRounds(userScore, computerScore, rounds);

    }

    public static void updateRoundHistory(Choice playerChoice, Choice computerChoice, string result){

        string path = "history.txt";
        string line = $"{DateTime.Now} | Player: {playerChoice} | Computer: {computerChoice} | Result: {result}";
        File.AppendAllText(path, line + Environment.NewLine);
    }

    public static void updateBestOfHistory(string winMessage){

        string path = "history.txt";
        File.AppendAllText(path, winMessage + Environment.NewLine);
    }


    public static void bestOfRounds(int userScore, int computerScore, int rounds) {
        if(userScore > computerScore){
            string winMessage = $"You won best out of {rounds} rounds.";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(winMessage);
            Console.ForegroundColor = ConsoleColor.White;
            updateBestOfHistory(winMessage);
        } else if (computerScore > userScore){
            string winMessage = $"Computer won best out of {rounds} rounds.";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(winMessage);
            Console.ForegroundColor = ConsoleColor.White;
            updateBestOfHistory(winMessage);
        } else {
            string winMessage = $"It's a tie out of {rounds} rounds!";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(winMessage);
            Console.ForegroundColor = ConsoleColor.White;
            updateBestOfHistory(winMessage);
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
        Choice choice;
        string input = Console.ReadLine() ?? "";
        string trimmed = input.Trim().ToLower();
        while (!(Enum.TryParse<Choice>(trimmed, ignoreCase: true, out choice) && Enum.IsDefined(typeof(Choice), choice)))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid choice, please enter rock, paper, or scissors: ");
            Console.ForegroundColor = ConsoleColor.White;
            input = Console.ReadLine() ?? "";
            trimmed = input.Trim().ToLower();
        }

        return choice;
    }

    private static readonly Random random = new Random();
    // Picks rock, paper, or scissors at random for the computer.
    private static Choice GetComputerChoice()
    {
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
