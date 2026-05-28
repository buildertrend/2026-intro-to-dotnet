// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        Console.Write("How many rounds do you want to play? ");
        int numRounds;
        while (!int.TryParse(Console.ReadLine(), out numRounds) || numRounds <= 0){
            Console.WriteLine("Please enter a valid positive integer.");
            Console.Write("How many rounds do you want to play? ");
        }

        int playerWins = 0;
        int computerWins = 0;
        int roundsPlayed = 0;
        int ties = 0;

        while (roundsPlayed < numRounds)
        {
            string playerChoice = GetPlayerChoice();

            if (playerChoice == "stats")
            {
                PrintScoreboard(playerWins, computerWins, ties);
                continue; // don't count as a round, prompt again
            }

            string computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(playerChoice, computerChoice);
            Console.WriteLine(result);

            if (result == "You win!")
            {
                playerWins++;
                roundsPlayed++;
            }
            else if (result == "Computer wins!")
            {
                computerWins++;
                roundsPlayed++;
            }
            else if (result == "It's a tie!")
            {
                ties++;
            }
            PrintScoreboard(playerWins, computerWins, ties);
        }

        Console.WriteLine();
        Console.WriteLine($"Final score: You {playerWins}, Computer {computerWins}");

        if (playerWins > computerWins)
        {
            Console.WriteLine("You are the overall winner!");
        }
        else if (computerWins > playerWins)
        {
            Console.WriteLine("Computer is the overall winner!");
        }
        else
        {
            Console.WriteLine("The match is a tie!");
        }
    }

    
    private static void PrintScoreboard(int playerWins, int computerWins, int ties)
    {
        Console.WriteLine($"Score — You: {playerWins}  Computer: {computerWins}  Ties: {ties}");
        Console.WriteLine();
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        while (true)
        {
            Console.Write("Enter your choice (rock, paper, scissors, or stats): ");
            string input = Console.ReadLine() ?? "";
            input = input.Trim().ToLower();
            if (input == "rock" || input == "paper" || input == "scissors" || input == "stats")
            {
                return input;
            }

            Console.WriteLine("Invalid input. Please enter 'rock', 'paper', 'scissors', or 'stats'.");
        }
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

        return playerWins ? "You win!" : "Computer wins!";
    }
}
