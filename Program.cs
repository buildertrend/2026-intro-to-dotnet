// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public enum Move{
        rock,
        paper,
        scissors,
    }

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        Move playerChoice = GetPlayerChoice();
        Move computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Move GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        string move = input.Trim().ToLower();
        return stringToMove(move);
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

    private static Move stringToMove(string input)
    {
        Move move = input switch
        {
            "rock" => Move.rock,
            "paper" => Move.paper,
            "scissors" => Move.scissors,
            _ => Move.rock,
        };
        return move;
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(Move player, Move computer)
    {
        if (player == computer)
        {
            return "It's a tie!";
        }

        bool playerWins =
            (player == Move.rock && computer == Move.scissors) ||
            (player == Move.paper && computer == Move.rock) ||
            (player == Move.scissors && computer == Move.paper);

        return playerWins ? "You win!" : "Computer wins!";
    }
}
