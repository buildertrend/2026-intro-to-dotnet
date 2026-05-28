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
        int player_wins = 0;
        int comp_wins = 0;
        PrintScoreboard(player_wins, comp_wins);
        Console.WriteLine();


        bool play_again = true;
        while (play_again)
        {
            string playerChoice = GetPlayerChoice();
            string computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(playerChoice, computerChoice);
            UpdateScores(ref player_wins, ref comp_wins, result);
            PrintScoreboard(player_wins, comp_wins);
            Console.WriteLine(result);
            play_again = CheckIfNextRound();
        }
    }

    private static void UpdateScores(ref int player_wins, ref int comp_wins, string result)
    {
        if (result == "You win!")
        {
            player_wins++;
        }
        else if (result == "Computer wins!")
        {
            comp_wins++;
        }
    }

    private static void PrintScoreboard(int player_wins, int comp_wins)
    {
        Console.WriteLine($"Numer of Wins:  You - {player_wins} | Computer - {comp_wins}");
    }

    private static bool CheckIfNextRound()
    {
        Console.WriteLine("Would you like to play again (y/n)?");
        string input = "";
        bool waitingOnInput = true;
        while (waitingOnInput)
        {
            input = Console.ReadLine() ?? "";
            input = input.Trim().ToLower();
            if (input == "y")
            {
                return true;  // user wants to play again
            }
            else if (input == "n")
            {
                return false; // user does not want to play again
            }
            else
            {
                Console.WriteLine("Please Enter y or n.");
            }
        }
        return false;
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        var reprompt = true;
        string input = "";
        while (reprompt) {
            Console.Write("Enter your choice (rock, paper, scissors): ");
            input = Console.ReadLine() ?? "";
            reprompt = ValidatePlayerChoice(input);
        }
        return input;
    }

    private static bool ValidatePlayerChoice(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            input = input.Trim().ToLower();
            if (input == "rock" || input == "paper" || input == "scissors")
            {
                return false;  // no reprompt needed
            }
            else
            {
                Console.WriteLine("Invalid Syntax. Try Again.");
                return true;
            }
        }
        else
        {
            Console.WriteLine("Please Enter Input.");
            return true;
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
