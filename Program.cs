// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.Reflection.Metadata.Ecma335;

namespace RpsWorkshop;

public class Program
{
    public static void Main()
    {
        int n = getNumRounds();
        int playerWins = 0;
        int computerWins = 0;
        int ties = 0;
        while (playerWins < n && computerWins < n)
        {
            Console.WriteLine("=== Rock Paper Scissors Lizard Spock ===");
            Console.WriteLine();

            string playerChoice = GetPlayerChoice();
            string computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(playerChoice, computerChoice);
            Console.WriteLine(result);
            if(result == "You win!")
            {
                playerWins++;
            }
            else if(result == "Computer wins!")
            {
                computerWins++;
            }
            else
            {
                ties++;
            }
            Console.WriteLine($"\nScore:\n You: {playerWins} \n Computer: {computerWins} \n Ties: {ties}");
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        while (true)
        {
            Console.Write("Enter your choice (rock, paper, scissors, lizard, spock): ");
            string input = Console.ReadLine() ?? "";

            input = input.Trim().ToLower();

            if (input != "rock" && input != "paper" &&
                input != "scissors" && input != "lizard"
                && input != "spock")
            {
                Console.WriteLine("Invalid Input");
            }
            else
            {
                return input.Trim().ToLower();
            }
        }
    }

    private static int getNumRounds()
    {
        while (true)
        {
            Console.Write("First to: ");
            string input = Console.ReadLine() ?? "";
            try
            {
                int num = Int32.Parse(input);
                if (num <= 0)
                {
                    Console.WriteLine("Invalid Input");
                }
                else
                {
                    return num;
                }
            }
            catch
            {
                Console.WriteLine("Input was not a number");
            }
        }
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static string GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors", "lizard", "spock" };
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

        // win choice decision for rock paper scissors lizard spock
        bool playerWins =
            (player == "rock" && (computer == "scissors" || computer == "lizard")) ||
            (player == "paper" && (computer == "rock" || computer == "spock")) ||
            (player == "scissors" && (computer == "paper" || computer == "lizard")) ||
            (player == "lizard" && (computer == "spock" || computer == "paper")) ||
            (player == "spock" && (computer == "scissors" || computer == "rock"))
            ;


        return playerWins ? "You win!" : "Computer wins!";
    }
}
