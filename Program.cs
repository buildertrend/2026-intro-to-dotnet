// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public class Program
{
    static int wins=0;
    static int losses=0;
    static int ties=0;

    public static void Main()
    {
        Console.WriteLine("===First to 3 Wins===");
        Console.WriteLine("=== Rock Paper Scissors ===");
        while (wins < 3 && losses < 3)
        {
            Console.WriteLine();

            string playerChoice = GetPlayerChoice();
            string computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

            string result = DetermineWinner(playerChoice, computerChoice);
            Console.WriteLine(result);
            string score = getWLT();
            Console.WriteLine(score);
        }
        if(losses >= 3)
        {
            Console.WriteLine("Game Over, Computer Wins!");
        }
        else
        {
            Console.WriteLine("Game Over, You Win!");
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
        if (player == computer)
        {
            ties++;
            return "It's a tie!"; 
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");

        if (playerWins)
        {
            wins++;
        }
        else
        {
            losses++;
        }
            return playerWins ? "You win!" : "Computer wins!";
    }

    private static string getWLT()
    {

        return $"Score: [Wins: {wins}]  [Losses: {losses}] [Ties: {ties}]";
    }

}
