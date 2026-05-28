// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public class Program
{
    public enum Choice { Unknown, Rock, Paper, Scissors, Lizard, Spock }
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Rock Paper Scissors Lizard Spock ===");
        Console.WriteLine();

        Choice playerChoice = GetPlayerChoice();

        Choice computerChoice;
        if (args.Length >= 0 && args[0] == "--cheat")
        {
            computerChoice = GetComputerChoice(playerChoice);
        }
        else
        {
            computerChoice = GetComputerChoice();
        }

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        String result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Choice GetPlayerChoice()
    {
        Choice choice = Choice.Unknown;
        while (choice == Choice.Unknown)
        {
            Console.Write("Enter your choice (rock, paper, scissors, lizard, spock): ");
            String input = Console.ReadLine() ?? "";
            choice = inputToChoice(input.Trim().ToLower());
            if (choice == Choice.Unknown) { Console.WriteLine("\nInvalid Input, Please Try Again"); }
        }
        return choice;
    }

    // Picks rock, paper, scissors, lizard, or spock at random for the computer.
    private static Choice GetComputerChoice()
    {
        string[] choices = { "rock", "paper", "scissors", "lizard", "spock" };
        Random random = new Random();
        int index = random.Next(choices.Length);
        return inputToChoice(choices[index]);
    }

    // Picks rock, paper, scissors, lizard, or spock based on previous player move.
    private static Choice GetComputerChoice(Choice playerChoice)
    {
        return getWinningChoice(playerChoice);
    }

    // Returns a string describing who won this round.
    private static string DetermineWinner(Choice player, Choice computer)
    {
        if (player == computer)
        {
            return "It's a tie!";
        }

        bool playerWins =
            (player == Choice.Rock && (computer == Choice.Scissors || computer == Choice.Lizard)) ||
            (player == Choice.Paper && (computer == Choice.Rock || computer == Choice.Spock)) ||
            (player == Choice.Scissors && (computer == Choice.Paper || computer == Choice.Lizard)) ||
            (player == Choice.Lizard && (computer == Choice.Paper || computer == Choice.Spock)) ||
            (player == Choice.Spock && (computer == Choice.Rock || computer == Choice.Scissors));

        return playerWins ? "You win!" : "Computer wins!";
    }
    
    private static Choice inputToChoice(String input)
    {
        Choice choice = input switch
        {
            "rock" => Choice.Rock,
            "paper" => Choice.Paper,
            "scissors" => Choice.Scissors,
            "lizard" => Choice.Lizard,
            "spock" => Choice.Spock,
            _ => Choice.Unknown
        };

        return choice;
    }

    private static Choice getWinningChoice(Choice playerChoice)
    {
        switch (playerChoice) {
            case Choice.Rock: return Choice.Paper;
            case Choice.Paper:  return Choice.Scissors;
            case Choice.Scissors: return Choice.Spock;
            case Choice.Lizard: return Choice.Rock;
            case Choice.Spock: return Choice.Lizard;
            default : return Choice.Unknown;
        };
    }
}
