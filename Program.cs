// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

enum GameOutcome
{
    PlayerWin,
    ComputerWin,
    Tie,
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");

        List<GameOutcome> history = LoadGameOutcomes();
        int wins = history.Where(o => o == GameOutcome.PlayerWin).Count();
        int losses = history.Where(o => o == GameOutcome.ComputerWin).Count();
        int ties = history.Where(o => o == GameOutcome.Tie).Count();
        Console.Write($"Wins: {wins}, Losses: {losses}, Ties: {ties}");

        Console.WriteLine();

        string playerChoice = GetPlayerChoice();
        string computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        GameOutcome outcome = DetermineWinner(playerChoice, computerChoice);
        SaveGameOutcome(outcome);
        string result = outcome switch
        {
            GameOutcome.PlayerWin => "You win!",
            GameOutcome.ComputerWin => "Computer wins.",
            GameOutcome.Tie => "It's a tie!",
            _ => "The programmer is stupid."
        };
        Console.WriteLine(result);
    }

    // Prompts the player and returns their choice as a lowercase string.
    private static string GetPlayerChoice()
    {
        while (true)
        {
            Console.Write("Enter your choice (rock, paper, scissors): ");
            string input = Console.ReadLine() ?? "";

            input.Trim().ToLower();
            List<string> choices = new List<string> { "rock", "paper", "scissors" };
            if (choices.Contains(input))
            {
                return input;
            }
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
    private static GameOutcome DetermineWinner(string player, string computer)
    {
        if (player == computer)
        {
            return GameOutcome.Tie;
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");

        return playerWins ? GameOutcome.PlayerWin : GameOutcome.ComputerWin;
    }

    private static void SaveGameOutcome(GameOutcome outcome)
    {
        File.AppendAllText("history.txt", outcome.ToString() + "\n");
    }

    private static List<GameOutcome> LoadGameOutcomes()
    {
        List<string> file = File.ReadAllLines("history.txt").ToList();

        List<GameOutcome> history = file.Select(o => o switch
        {
            "PlayerWin" => GameOutcome.PlayerWin,
            "ComputerWin" => GameOutcome.ComputerWin,
            "Tie" => GameOutcome.Tie,
            _ => GameOutcome.Tie,
        }).ToList();

        return history;
    }
}
