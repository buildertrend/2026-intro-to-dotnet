// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.CommandLine;
using System.CommandLine.Parsing;

namespace RpsWorkshop;

enum GameOutcome
{
    PlayerWin,
    ComputerWin,
    Tie,
}

enum Choice
{
    Rock,
    Paper,
    Scissors,
    Lizard,
    Spock,
}

static class Constants
{
    public static readonly Dictionary<Choice, Choice[]> win = new Dictionary<Choice, Choice[]>
    {
        { Choice.Rock, new Choice[] { Choice.Paper, Choice.Spock } },
        { Choice.Paper, new Choice[] { Choice.Scissors, Choice.Lizard } },
        { Choice.Scissors, new Choice[] { Choice.Rock, Choice.Spock } },
        { Choice.Lizard, new Choice[] { Choice.Rock, Choice.Scissors } },
        { Choice.Spock, new Choice[] { Choice.Lizard, Choice.Paper } },
    };
};

public class Program
{
    public static void Main(string[] args)
    {
        bool cheatEnabled = false;
        int bestOf = 1;
        Option<bool> cheatOption = new("--cheat")
        {
            Description = "Let the computer cheat and always win"
        };
        Option<int> bestOfOption = new("--bestof")
        {
            Description = "Play best out of N rounds."
        };

        RootCommand rootCommand = new("RPS Game");
        rootCommand.Options.Add(cheatOption);
        rootCommand.Options.Add(bestOfOption);


        rootCommand.SetAction(parseResult =>
        {
            cheatEnabled = parseResult.GetValue<bool>("--cheat");
            bestOf = parseResult.GetValue<int>("--bestof");
        });
        ParseResult parseResult = rootCommand.Parse(args);
        parseResult.Invoke();

        // Must be an odd number
        if (bestOf % 2 == 0)
        {
            bestOf += 1;
        }

        Console.WriteLine("=== Rock Paper Scissors ===");

        Console.WriteLine($"Best of: {bestOf}");

        int playerWins = 0;
        int computerWins = 0;

        while ((PlayerWinBestOf(playerWins, computerWins, bestOf) != true) && (PlayerWinBestOf(playerWins, computerWins, bestOf) != false))
        {
            List<GameOutcome> history = LoadGameOutcomes();
            int wins = history.Where(o => o == GameOutcome.PlayerWin).Count();
            int losses = history.Where(o => o == GameOutcome.ComputerWin).Count();
            int ties = history.Where(o => o == GameOutcome.Tie).Count();
            Console.Write($"Wins: {wins}, Losses: {losses}, Ties: {ties}");

            Console.WriteLine();

            Choice playerChoice = GetPlayerChoice();
            Choice computerChoice = GetComputerChoice(cheatEnabled, playerChoice);

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
            if (outcome == GameOutcome.PlayerWin)
            {
                playerWins += 1;
            }
            else if (outcome == GameOutcome.ComputerWin)
            {
                computerWins += 1;
            }
            Console.WriteLine($"{result} {playerWins}/{computerWins}");
        }

        bool? bestOfOutcome = PlayerWinBestOf(playerWins, computerWins, bestOf);

        if (bestOfOutcome == true)
        {
            Console.WriteLine($"You win the best of {bestOf} with {playerWins} wins!");
        }
        else if (bestOfOutcome == false)
        {
            Console.WriteLine($"You lose the best of {bestOf} with {computerWins} losses.");
        }
        else
        {
            Console.WriteLine("Check your code because there's a logic bug");
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    private static Choice GetPlayerChoice()
    {
        while (true)
        {
            Console.Write("Enter your choice (rock, paper, scissors, lizard, spock): ");
            string input = Console.ReadLine() ?? "";
            input = input.Trim().ToLower();

            List<string> choices = new List<string> { "rock", "paper", "scissors", "lizard", "spock" };
            if (choices.Contains(input))
            {
                return input switch
                {
                    "rock" => Choice.Rock,
                    "paper" => Choice.Paper,
                    "scissors" => Choice.Scissors,
                    "lizard" => Choice.Lizard,
                    "spock" => Choice.Spock,
                    _ => Choice.Rock // not possible
                };
            }
        }
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Choice GetComputerChoice(bool cheatEnabled, Choice playerChoice)
    {
        Choice[] choices;
        if (cheatEnabled)
        {
            choices = Constants.win[playerChoice];
        }
        else
        {
            choices = new Choice[] { Choice.Rock, Choice.Paper, Choice.Scissors, Choice.Lizard, Choice.Spock };
        }

        Random random = new Random();
        int index = random.Next(choices.Length);
        return choices[index];
    }

    // Returns a string describing who won this round.
    private static GameOutcome DetermineWinner(Choice player, Choice computer)
    {
        if (player == computer)
        {
            return GameOutcome.Tie;
        }

        bool playerWins = !Constants.win[player].Contains(computer);

        return playerWins ? GameOutcome.PlayerWin : GameOutcome.ComputerWin;
    }

    private static void SaveGameOutcome(GameOutcome outcome)
    {
        File.AppendAllText("history.txt", outcome.ToString() + "\n");
    }

    private static List<GameOutcome> LoadGameOutcomes()
    {
        List<string> file;
        try
        {
            file = File.ReadAllLines("history.txt").ToList();
        }
        catch (FileNotFoundException)
        {
            file = new List<string> { "" };
        }

        List<GameOutcome> history = file.Select(o => o switch
        {
            "PlayerWin" => GameOutcome.PlayerWin,
            "ComputerWin" => GameOutcome.ComputerWin,
            "Tie" => GameOutcome.Tie,
            _ => GameOutcome.Tie,
        }).ToList();

        return history;
    }

    private static bool? PlayerWinBestOf(int playerWins, int computerWins, int bestOf)
    {
        int needed = (bestOf + 1) / 2;

        if (playerWins >= needed)
        {
            return true;
        }
        else if (computerWins >= needed)
        {
            return false;
        }
        else
        {
            return null;
        }
    }
}
