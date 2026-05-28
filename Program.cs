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
        int pcount=0;
        int ccount=0;
        int n=0;
        while(n<3){
        string playerChoice = GetPlayerChoice();
        string computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
        if (result=="You win!")
        {
            pcount+=1;
        }
        else if(result=="Computer Wins!")
        {
            ccount+=1;
        }
        n+=1;
        }
        if (pcount > ccount)
        {
            Console.WriteLine("Player wins best of 3");
        }
        else if (ccount > pcount)
        {
            Console.WriteLine("Computer wins best of 3");
        }
        else
        {
            Console.WriteLine("they were all ties");
        }
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        input=input.Trim().ToLower();

        bool a = false;
        while(a==false){
            if(input=="rock"||input=="paper"||input=="scissors"){
                 a=true;
                 return input.Trim().ToLower();
                
            }
            else{
               Console.WriteLine("Invalid input Try again");
               Console.Write("Enter your choice (rock, paper, scissors): ");
               input = Console.ReadLine() ?? "";
               input=input.Trim().ToLower();
           

        }
        
        }
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
            return "It's a tie!";
        }

        bool playerWins =
            (player == "rock" && computer == "scissors") ||
            (player == "paper" && computer == "rock") ||
            (player == "scissors" && computer == "paper");

        return playerWins ? "You win!" : "Computer wins!";
    }
}
