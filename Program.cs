// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

public class Program
{
    enum Move
    {
        rock,
        paper,
        scissors,
        gun
    }

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
            Move playerChoice = GetPlayerChoice();
            Move computerChoice = GetComputerChoice();

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
    private static Move GetPlayerChoice()
    {
        var reprompt = true;
        string input = "";
        while (reprompt) {
            Console.Write("Enter your choice (rock, paper, scissors): ");  // gun is hidden ;)
            input = Console.ReadLine() ?? "";
            reprompt = ValidatePlayerChoice(input);
        }
        input = input.Trim().ToLower();
        if (input == "rock")
        {
            return Move.rock;
        }
        else if (input == "paper")
        {
            return Move.paper;
        }
        else if (input == "scissors")
        {
            return Move.scissors;  // input is already validated, so 3rd option is scissors
        }
        else
        {
            return Move.gun;
        }

    }

    private static bool ValidatePlayerChoice(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            input = input.Trim().ToLower();
            if (input == "rock" || input == "paper" || input == "scissors" || input == "gun")
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
    private static Move GetComputerChoice()
    {
        Random random = new Random();
        return (Move)random.Next(0, 3);  // computer cannot choose gun
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
            (player == Move.scissors && computer == Move.paper) ||
            (player == Move.gun);

        return playerWins ? "You win!" : "Computer wins!";
    }
}
