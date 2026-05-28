// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

using System;
using System.IO;

public enum Move{
        unknown,
        rock,
        paper,
        scissors,
    }

public class Program
{
    public const string STATSFILEPATH = "stats.txt";
    public static void Main()
    {
        List<string> VALIDINPUTS = new List<string> { "1", "2", "3" };
        string message = "\n1.) Play\n2.) Quit\n3.) Stats\n\nPress your number and hit enter!\n";
        string repeatMessage = "\nInvalid input. Try again\n\n1.) Play\n2.) Quit\n3.) Stats\n\nPress your number and hit enter!";
        Console.WriteLine("=== Rock Paper Scissors ===");
        string input = getInput(message,repeatMessage,VALIDINPUTS);

        while(input != "2")
        {
            if (input == "1")
            {
                roundLoop();
            }
            if(input == "3")
            {
                printStats();
            }
            input = getInput(message, repeatMessage, VALIDINPUTS);
        }

        
    }

    private static void printStats()
    {
        string fileContent = File.ReadAllText(STATSFILEPATH);
        Console.WriteLine("Stats: Wins-Losses-Ties");
        Console.WriteLine(fileContent);
    }

    private static void roundLoop()
    {
        Move playerChoice = GetPlayerChoice();
        Move computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
    }

    // Prompts the player and returns their choice as a lowercase string. Retrys if invalid input.
    private static Move GetPlayerChoice()
    {
        string input = getInput("Enter your choice (rock, paper, scissors): ",
            "Invalid input. Enter your choice (rock, paper, scissors): ",new List<string> { "rock","paper","scissors"});
            
        string move = input.Trim().ToLower();
        Move enumMove = stringToMove(move);
        return enumMove;
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Move GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors" };
        Random random = new Random();
        int index = random.Next(choices.Length);
        string move = choices[index];
        return stringToMove(move);
    }

    //converts string input to Enum. returns unknown if not valid input.
    private static Move stringToMove(string input)
    {
        Move move = input switch
        {
            "rock" => Move.rock,
            "paper" => Move.paper,
            "scissors" => Move.scissors,
            _ => Move.unknown,
        };
        return move;
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(Move player, Move computer)
    {
        int playerWinCount = 0;
        int playerLossCount = 0;
        int playerTieCount = 0;

        if (File.Exists(STATSFILEPATH))
        {
            string fileContent = File.ReadAllText(STATSFILEPATH);
            using (StringReader reader = new StringReader(fileContent))
            {
                string? line;
                line = reader.ReadLine();
                playerWinCount = int.Parse(line);
                line = reader.ReadLine();
                playerLossCount = int.Parse(line);
                line = reader.ReadLine();
                playerTieCount = int.Parse(line);
            }
        }

            if (player == computer)
            {
                playerTieCount++;
                updateStats(playerWinCount, playerLossCount, playerTieCount);
                return "It's a tie!";
            }

            bool playerWins =
                (player == Move.rock && computer == Move.scissors) ||
                (player == Move.paper && computer == Move.rock) ||
                (player == Move.scissors && computer == Move.paper);



            if (playerWins)
            {
                playerWinCount++;
                updateStats(playerWinCount, playerLossCount, playerTieCount);
            }
            else
            {
                playerLossCount++;
                updateStats(playerWinCount, playerLossCount, playerTieCount);
            }

            return playerWins ? "You win!" : "Computer wins!";
        }

    private static void updateStats(int w, int l, int t)
    {
        string stats = $"{w}\n{l}\n{t}";
        File.WriteAllText(STATSFILEPATH, stats);
    }

    //basic input validation
    private static string getInput(string message, string repeatMessage, List<string> validInputs)
    {
        Console.WriteLine(message);
        string input = Console.ReadLine() ?? "";
        while (!validInputs.Contains(input))
        {
            Console.WriteLine(repeatMessage);
            input = Console.ReadLine() ?? "";
        }
        return input;
    }
}
