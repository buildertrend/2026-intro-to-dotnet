// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace RpsWorkshop;

public class Program
{
    public static void Main(string[] args)
    {
        bool computerCheats = false;
        if (args.Length == 1)
        {
            if (args[0] == "--cheat")
            {
                computerCheats = true;
            }
        }
        
        Dictionary<string, int> results = new Dictionary<string, int>();
        results["playerWins"] = 0;
        results["computerWins"] = 0;
        results["draw"] = 0; 
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        int rounds = GetRounds();
        bool winner = false; 
        while (winner == false)
        {
            string playerChoice = string.Empty;
            while (!ValidateChoice(playerChoice))
            {
                playerChoice = GetPlayerChoice();
            }
            string computerChoice = GetComputerChoice(playerChoice, computerCheats);

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(playerChoice, computerChoice, results);
            SaveSession(result);
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine($"Player Wins: {results["playerWins"]} | Computer Wins: {results["computerWins"]} | Draws: {results["draw"]}");

            winner = DetermineGameWinner(results, rounds); 
        } 
        
    }

    private static int GetRounds()
    {
        int rounds = 0;
        while (rounds < 1)
        {
            Console.WriteLine("How many games do you want to play? Enter [Stats] to see lifetime stats");

            try
            {
                string input = Console.ReadLine().Trim().ToLower(); 
                if (input == "stats")
                {
                    GetStats();
                    continue; 
                }
                rounds = int.Parse(input);
                if (rounds % 2 == 0)
                {
                    throw new Exception("Games to play must be an Odd Integer");
                }
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine("Enter an odd integer.");
                    rounds = 0; 
                }
            }
        }
        return rounds; 

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
    private static string GetComputerChoice(string playerChoice, bool computerCheats)
    {
        if (computerCheats)
        {
            string computerChoice = "";
            switch (playerChoice)
            {
                case "rock":
                    computerChoice = "paper";
                    break;
                case "paper":
                    computerChoice = "scissors";
                    break;
                default:
                    computerChoice = "paper";
                    break;
            }
            return computerChoice;
        } 

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

    // Returns a string describing who won this round. Increments scoreboard.
    private static string DetermineWinner(string player, string computer, Dictionary<string, int> results)
    {
        if (player == computer)
        {
            results["draw"] += 1; 
            return "It's a tie!";
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");
        
        if (playerWins)
        {
            results["playerWins"] += 1;
            return "You win!";
        }
        else
        {
            results["computerWins"] += 1;
            return "Computer wins!";
        }
    }

    // returns TRUE if the game should end, otherwise FALSE
    private static bool DetermineGameWinner(Dictionary<string, int> results, int rounds)
    {
        if (results["playerWins"] > (rounds / 2))
        {
            Console.WriteLine("PLAYER WINS THE SERIES!");
            return true;
        } else if (results["computerWins"] > (rounds / 2)) {
            Console.WriteLine("COMPUTER WINS THE SERIES!"); 
            return true;
        }

        if ((results["playerWins"] + results["computerWins"] + results["draw"]) >= rounds)
        {
            if (results["playerWins"] > results["computerWins"])
            {
                Console.WriteLine("PLAYER WINS THE SERIES!");
                return true;
            }
            else if (results["computerWins"] > results["playerWins"])
            {
                Console.WriteLine("COMPUTER WINS THE SERIES!");
                return true;
            } else
            {
                Console.WriteLine("THE SERIES IS A DRAW");
                return true; 
            }
        }
        
        return false;
    }

    private static void SaveSession(string message)
    {
        string filePath = $"{Directory.GetCurrentDirectory()}\\history.txt";

        using (StreamWriter writer = new StreamWriter(filePath, true)) // append=true
        {
            writer.WriteLine(message);
        }
    }

    private static void GetStats()
    {
        int playerWins = 0;
        int computerWins = 0;
        int draws = 0;

        string filePath = $"{Directory.GetCurrentDirectory()}\\history.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            // ReaderObject reads a single line, stores it in Line string variable and then displays it on
            // console
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim().ToLower() == "you win!") playerWins++;
                if (line.Trim().ToLower() == "computer wins!") computerWins++;
                if (line.Trim().ToLower() == "it's a tie!") draws++;
            }

            Console.WriteLine($"LIFETIME STATS: Player Wins: {playerWins} | Computer Wins: {computerWins} | Ties: {draws}");
        }
    } 
}
