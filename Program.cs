// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Drawing;

namespace RpsWorkshop;

public class Program
{
    private enum Choice
    {
        Rock,
        Paper,
        Scissors,
        Invalid,
    }

    // Matrix to determine if the player's selected choice's win condition (the dict's value) corresponds to the computer choice
    private static Dictionary<Choice, Choice> WinMatrix = new Dictionary<Choice, Choice> {
        {Choice.Rock, Choice.Scissors},
        {Choice.Paper, Choice.Rock},
        {Choice.Scissors, Choice.Paper},
    };

    public static void Main()
    {
        const string WRITE_PATH = "./history.txt";
        const string SAVE_FILE_PATH = "./save.txt";
        List<string> resultMessages = new List<string> { "You win!", "Computer wins!", "It's a tie!" };

        int wins = 0;
        int losses = 0;
        int ties = 0;

        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        // Game loop
        while (true)
        {

            Console.WriteLine($"Score - You: {wins} Computer: {losses} Ties: {ties}");

            string playerChoice = GetPlayerChoice();
            string computerChoice = GetComputerChoice();

            // Simple player exit check
            if (playerChoice == "exit") break;

            // Player chooses to load
            if (playerChoice == "load")
            {
                Console.WriteLine();
                Console.WriteLine("Loading file into scoreboard...");
                Console.WriteLine();
                try
                {
                    using (StreamReader sr = new StreamReader(SAVE_FILE_PATH))
                    {
                        // There is just one line for save data information
                        string saveLine = sr.ReadLine();
                        if (saveLine != null)
                        {
                            string[] infoArr = saveLine.Split(new char[] { ' ' });
                            if (infoArr.Length == 3)
                            {
                                wins = int.Parse(infoArr[0]);
                                losses = int.Parse(infoArr[1]);
                                ties = int.Parse(infoArr[2]);
                                continue;
                            }
                            else { Console.Write("No save data found."); continue; }
                        }
                        else
                        {
                            Console.Write("No save data found.");
                            continue;
                        }
                    }
                } catch (Exception ex) { Console.WriteLine("No save data created yet."); continue; }
            }
            Choice playerChoiceEnum = playerChoice switch
            {
                "rock" => Choice.Rock,
                "paper" => Choice.Paper,
                "scissors" => Choice.Scissors,
                _ => Choice.Invalid,
            };

            Choice computerChoiceEnum = computerChoice switch
            {
                "rock" => Choice.Rock,
                "paper" => Choice.Paper,
                "scissors" => Choice.Scissors,
                _ => Choice.Invalid, // Should never occur, but is handled anyway
            };

            if (playerChoiceEnum == Choice.Invalid)
            {
                Console.WriteLine($"Invalid choice of \"{playerChoice}\", please select again.");
                Console.WriteLine();
                continue;
            }

            // In case computer gives invalid choice
            if (computerChoiceEnum == Choice.Invalid)
            {
                Console.WriteLine($"Computer improperly selected \"{computerChoice}\", restarting.");
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            int resultInt = DetermineWinner(playerChoiceEnum, computerChoiceEnum);
            string result = resultMessages[resultInt];

            // History file write
            try
            {
                AppendToFile(WRITE_PATH, playerChoice, computerChoice, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File append could not be performed: {ex.Message}");
            }
            
            // Switch on results for scoreboard
            switch (resultInt)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Green;
                    wins++;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    losses++;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    ties++;
                    break;
            }
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White; // Reset color
        }
        // Save file
        try
        {
            Console.WriteLine("Saving data to file");
            using (StreamWriter sw = new StreamWriter(SAVE_FILE_PATH))
            {
                sw.WriteLine($"{wins} {losses} {ties}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("No save data created.");
        }
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
        Console.Write("Enter your choice (rock, paper, scissors, load, exit): ");
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

    // Returns an int describing who won this round (0 = player, 1 = computer, 2 = tie)
    private static int DetermineWinner(Choice player, Choice computer)
    {
        if (player == computer)
        {
            return 2;
        }

        // Using decision matrix
        bool playerWins = WinMatrix[player] == computer;

        return playerWins ? 0 : 1;
    }
}
