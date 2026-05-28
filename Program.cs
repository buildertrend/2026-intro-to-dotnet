// Rock Paper Scissors - Intern Workshop Starter
//
// This is intentionally minimal. It plays exactly ONE round vs the computer
// and exits. Your job during the workshop is to extend it.
//
// Pick a feature from the README and go.

using System.ComponentModel;
using System.IO.Pipes;
using System.IO;

namespace RpsWorkshop;

public class Program
{
    
    public static void Main()
    {
        

        Console.Write("How many rounds do you want to play? ");
        var rounds = int.Parse(Console.ReadLine() ?? "1");
        var playerWins = 0;
        var computerWins =0;
        var tieWins =0;
        

        for (int i =0; i< rounds; i++)
        {
        Console.WriteLine("=== Rock Paper Scissors ===");
        Console.WriteLine();

        string playerChoice = GetPlayerChoice();
        string computerChoice = GetComputerChoice();

        Console.WriteLine();
        Console.WriteLine($"You played:      {playerChoice}");
        Console.WriteLine($"Computer played: {computerChoice}");
        Console.WriteLine();

        string result = DetermineWinner(playerChoice, computerChoice);
        Console.WriteLine(result);
        

        File.AppendAllText("history.txt", $"Round {i+1}: Player: {playerChoice}, Computer: {computerChoice}, Result: {result}\n");


        if(result == "You win!")
            {
                playerWins++;
                
            }
        else if(result == "Computer wins!")
            {
                computerWins++;

            }
            else
            {
                tieWins++;
            }
        Console.Write("Score- You: "+playerWins+ " Computer: "+computerWins+" Tie: "+tieWins+"\r\n");
            

        }
        if (playerWins > computerWins)
        {
            Console.Write("Overall: You win!");

        }
        else if(playerWins<computerWins)
        {
            Console.Write("Overall: Computer Wins!");
        }
        else
        {
            Console.Write("Overall: It is a tie!");
        }
     
        


       
    }

    // Prompts the player and returns their choice as a lowercase string.
    // Note: no input validation yet. Garbage in = garbage out. (Hint, hint.)
    private static string GetPlayerChoice()
    {
        Console.Write("Enter your choice (rock, paper, scissors): ");
        string input = Console.ReadLine() ?? "";
        bool a = true;

        while(a){
            if(input == "rock" || input == "paper" || input == "scissors"){
                a = false;
                return input.Trim().ToLower();
            }
            else if(input == "status"){
                
            }
            else{
                Console.Write("Invalid choice. Please enter rock, paper, or scissors: ");
                input = Console.ReadLine() ?? "";
            }
        }
        return null;


        
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

    public enum Choices{
        Rock,
        Paper,
        Scissors

    }




}
