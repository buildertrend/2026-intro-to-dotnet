// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

namespace RpsWorkshop;

// Enumeration for the option selection
public enum Choice 
{
    Rock,
    Paper,
    Scissors
}
public class Program
{
    private static Random random = new Random();

    public static void Main()
    {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        int playerScore = 0;
        int computerScore = 0;

        while(playerScore < 2 && computerScore < 2) 
        {
            Choice playerChoice = GetPlayerChoice();
            Choice computerChoice = GetComputerChoice();

            Console.WriteLine();
            Console.WriteLine($"You played:      {playerChoice}");
            Console.WriteLine($"Computer played: {computerChoice}");
            Console.WriteLine();

         //    string result = DetermineWinner(playerChoice, computerChoice);
         //    Console.WriteLine(result);

            int roundWinner = NRoundsWinner(playerChoice, computerChoice);

            // A switch that helps print out the messages based of the round winner.
            string message = roundWinner switch
            {
                1 => "You win! :)",
                2 => "Computer wins! :(",
                0 => "It's a tie...",
                _ => "Error, the programmer is silly.",
            };
            Console.WriteLine(message);
            
            if(roundWinner == 1) 
            {
                playerScore++;
            } 
            else if (roundWinner == 2)
            {
                computerScore++;
            }

            Console.WriteLine($"Player Score: {playerScore} - Computer Score: {computerScore}");
            Console.WriteLine();
        }

        if(playerScore > computerScore) {
            Console.WriteLine("Congratulations :) You won!");
        } else {
            Console.WriteLine("Sorry...You LOST!");
        }
        Console.WriteLine();
        Console.WriteLine("Final Score: ");
        Console.WriteLine($"Player: {playerScore} - Computer: {computerScore}");
    }

    private static bool IsValidChoice(Choice validChoice)
    {
        return validChoice == Choice.Rock || validChoice == Choice.Scissors || validChoice == Choice.Paper;
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static Choice GetPlayerChoice()
    {
        while(true)
        {
            Console.Write("Enter your choice (rock, paper, scissors): ");
            string input = Console.ReadLine() ?? "";
            input = input.Trim().ToLower();
            
            Choice choice = input switch 
            {
                "rock" => Choice.Rock,
                "scissors" => Choice.Scissors,
                "paper" => Choice.Paper,
                _ => (Choice)(-1)
            };

            if(choice != (Choice)(-1)) {
                return choice;
            }

            Console.WriteLine("Invalid choice. Please try again.....");
        }
    }

    // Picks rock, paper, or scissors at random for the computer.
    private static Choice GetComputerChoice()
    {
        Choice[] choices = { Choice.Rock, Choice.Paper, Choice.Scissors };
        // Random random = new Random();
        int index = random.Next(choices.Length);
        return choices[index];
    
}

    // Returns a string describing who won this round.
 //    private static string DetermineWinner(string player, string computer)
 //    {
 //        if (player == computer)
 //        {
 //            return "It's a tie!";
 //        }

 //        bool playerWins =
 //            (player == "rock" && computer == "scissors") ||
 //            (player == "paper" && computer == "rock") ||
 //            (player == "scissors" && computer == "paper");

 //        return playerWins ? "You win!" : "Computer wins!";
 //    

    // Returns a number either 1 or 2 depending on who wins the round.
    // This will help the porgram to increment the Score of the player/computer.
    private static int NRoundsWinner(Choice player, Choice computer) 
    {
        if(player == computer) 
        {
            return 0;
        }

        bool winner = 
            (player == Choice.Rock && computer == Choice.Scissors) ||
            (player == Choice.Scissors && computer == Choice.Paper) ||
            (player == Choice.Paper && computer == Choice.Rock);

        if(winner == true) {
            return 1;
        } else {
            return 2;
        }
    }
}