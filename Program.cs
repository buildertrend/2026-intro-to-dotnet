// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.ComponentModel;

namespace RpsWorkshop;

public enum Move
{
    rock,
    paper,
    scissors,
}

public class Program
{
    private const string WinningText = "You win!";
    private const string LosingText = "Computer wins!";
    private const string TieText = "It's a tie!";
    private static Random random = new Random();
    public static void Main()
    {
        int playerWins = 0;
        int computerWins = 0;
        int ties = 0;
        while (!IsGameOver(playerWins, computerWins, 3))
        {
            Console.WriteLine("=== Rock Paper Scissors ===");
            Console.WriteLine();

            Move playerChoice = GetPlayerChoice();
            Move computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();
            try
            {
                string result = DetermineWinner(playerChoice, computerChoice);
                if (result != WinningText && result != LosingText && result != TieText)
                {
                    throw new Exception($"Unexpected result from DetermineWinner: {result}");
                }
                if (result == WinningText)
                {
                    playerWins++;
                }
                else if (result == LosingText)
                {
                    computerWins++;
                }
                else
                {
                    ties++;
                }
                Console.WriteLine(GetScoreboard(playerWins, computerWins, ties));
                Console.WriteLine(result);
            } catch (Exception exception)
            {
                Console.WriteLine($"Something drastically went wrong: {exception.Message}");
            }
        }
    }

    private static bool IsGameOver(int playerWins, int computerWins, int numOfGames)
    {
        return playerWins >= numOfGames || computerWins >= numOfGames;
    }

    private static string GetScoreboard(int playerWins, int computerWins, int ties)
    {
        return $"Score: Player {playerWins} - Computer {computerWins} - Ties {ties}";
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Move GetPlayerChoice()
    {
        while (true)
        {
            Console.Write("Enter your choice (rock, paper, scissors): ");
            string input = Console.ReadLine() ?? "";

            input = input.Trim().ToLower();

            switch (input)
            {
                case "rock": return Move.rock;
                case "paper": return Move.paper;
                case "scissors": return Move.scissors;
                default:
                    Console.WriteLine("Please input a valid choice. Your choices are 'rock', 'paper', or 'scissors'.");
                    break;
            }
        }
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Move GetComputerChoice()
    {
        int index = random.Next(Enum.GetNames(typeof(Move)).Length);
        return (Move)index;
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
