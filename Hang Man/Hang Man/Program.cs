using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hang_Man
{
    class Program
    {
        static void Main(string[] args)
        {
            //call the greetplayer function, and check to see if he or she wants to play
            GreetPlayer();
            //call the hangman function to start game

        }

        static void HangMan()
        {
            //an empty string to store the randomly selected word
            string lettersGuessed = string.Empty;
            //a list string variable to hold the user input
            //List<string> userInput = new List<string>();
            //a boolean value to store false for gameSolved            
            // bool wordGuessed = false;
            //an integer variable to store the number of tries player has to guess right word 
            //call the Random constructor 
            Random rwg = new Random();
            bool keepPlaying = true;
            int numTries = 10;
            List<string> words = new List<string>() { "homeland", "country", "world", "Africa", "internet", "laptop", "galaxy", "America", "ethiopia" };
            //declare a new variable List string data type that holds our list of items
            //words = File.ReadAllText("words.txt").Split('\n').ToList();
            //store the value of the randomly selected word from List in the empty string variable
            string wordPick = words[rwg.Next(0, words.Count())];
            //declare an empty string to store the characters entered by the user
            //a for loop to loop through the given word and add _ for ever letter found.
            

            while (keepPlaying)
            {
                Console.WriteLine(MaskedWord(wordPick, lettersGuessed));
                Console.WriteLine("You have " + numTries + " lives left");
                Console.WriteLine("Enter a guess");
                string input = Console.ReadLine().ToUpper();
                if (input.Length == 1)
                {
                    lettersGuessed += input;

                    if (wordPick.Contains(input))
                    {
                        Console.WriteLine("Good job!");

                        if (AllLettersGuessed(MaskedWord(wordPick, lettersGuessed)))
                        {
                            keepPlaying = false;
                            Console.WriteLine("You won!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad job");
                        numTries--;
                    }
                }
                else
                {
                    if (wordPick == input)
                    {
                        Console.WriteLine("You won!");
                        keepPlaying = false;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect");
                        numTries--;
                    }
                }
                if (numTries == 0)
                {
                    keepPlaying = false;
                    Console.WriteLine("");
                }
            }
        }

        static string MaskedWord(string wordToGuess, string lettersGuessed)
        {
            string returnString = "";

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                char letter = wordToGuess[i];

                if (lettersGuessed.ToUpper().Contains(Char.ToUpper(letter)))
                {
                    returnString += letter + " ";
                }
                else
                {
                    returnString += "_" + " ";
                }
            }

            return returnString;
        }

        static bool AllLettersGuessed(string maskedWord)
        {
            if (maskedWord.Contains("_"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

            static void GreetPlayer()
            {
                //greet player, ask for name and print to console a personalized greeting with 
                //the input from player returned with the greeting
                Console.Write("What is your name, friend?==>>");
                //take the input from player and store in the string variable input
                string input = Console.ReadLine();
                Console.WriteLine("Welcome to my game " + input + "!");
                Console.WriteLine();
                //take the input from player and print on to the console if they want to play or no
                Console.Write("\nDo you want to play HangMan, " + input + "? Yes/No==>>");
                //Give player the choice of whethere to play or not, and depending on the choice run the following if/else statement
                if (Console.ReadLine() == "yes".ToString().ToLower())
                {
                    //if they choose to play give them the following instructions on how to play hangman
                    Console.WriteLine();
                    Console.WriteLine("***************************");
                    Console.WriteLine("Great! Let's play then!" +
                        "\n***************************" +
                        "\nHere are the rules: " +
                        "\nYou will be guessing a word" +
                        "\nEither guess the correct word or" +
                        "\nGuess the letters one a time.");
                    Console.WriteLine("***************************");
                    Console.WriteLine("When you're ready guess the word, or enter a letter to play." +
                        "\nYou have " + "numsGuess" + " tries to get it right");
                    Console.WriteLine();
                    HangMan();
                }
               
            }

            static void HighScores(int playerScore)
            {
                Console.Write("Your name:==>>");
                string playerName = Console.ReadLine();

                AlemEntities db = new AlemEntities();

                HighestScore newHighestScore = new HighestScore();
                newHighestScore.DateCreated = DateTime.Now;
                newHighestScore.Game = "Guess That Number";
                newHighestScore.Name = playerName;
                newHighestScore.Score = playerScore;

                db.HighestScores.Add(newHighestScore);

                db.SaveChanges();
            }

            static void DisplayHighestScore()
            {
                Console.Clear();
                Console.WriteLine("Guess That Number High Scores");
                Console.WriteLine("*****************************");

                AlemEntities db = new AlemEntities();
                List<HighestScore> highestScoreList = db.HighestScores
                    .Where(x => x.Game == "Guess That Number")
                    .OrderBy(x => x.Score).Take(10)
                    .ToList();

                foreach (HighestScore highScore in highestScoreList)
                {
                    Console.WriteLine("{0}. {1} - {2} on {3}",
                        highestScoreList.IndexOf(highScore) + 1,
                        highScore.Name,
                        highScore.Score,
                        highScore.DateCreated.Value.ToShortDateString());
                }
                Console.WriteLine("\n\n\n");


            }
    }
       

}


