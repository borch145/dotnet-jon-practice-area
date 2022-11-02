using System;
using System.Linq.Expressions;
using System.Threading;

namespace Day_6___Parse_Challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ask player 1 for a number 1-10
            //clear screen once the number is picked
            //make player two keep guessing a number 1-10 until they guess the correct number
            //bonus---make the game rerun once over

            bool keepPlaying = true;

            while (keepPlaying)
            {
                int pOneSelection = PlayerOneSelectNum();
                PlayerTwoGuess(pOneSelection);
                keepPlaying = ContinuePlaying();
            }
            

        }

        static int PlayerOneSelectNum()
        {

            start:
            Console.WriteLine("Player One, please select a number 1-10.");
            string numSelect = Console.ReadLine();
            int parsedSelect;
            bool validSelection = int.TryParse(numSelect, out parsedSelect);

            while(validSelection == false || parsedSelect < 1 || parsedSelect >10)
            {
                
                Console.Clear();
                Console.WriteLine("Invalid Selection. Press any key to select again....");
                Console.ReadKey();
                Console.Clear();
                goto start;


            }

            Console.Clear();
            Console.WriteLine("Your selection was " + parsedSelect + ". Press any key for player twos turn to guess.");
            Console.ReadKey();
            Console.Clear();
            return parsedSelect;    
        }

        static bool PlayerTwoGuess(int rightAnswer)
        {
            start:
            Console.WriteLine("Player Two, guess Player One's number 1-10.");
            string guess = Console.ReadLine();
            int parsedGuess;
            bool validGuess = int.TryParse(guess, out parsedGuess);

            while (validGuess == false || parsedGuess != rightAnswer)
            {

                Console.Clear();
                Console.WriteLine("Invalid guess. Press any key to select again....");
                Console.ReadKey();
                Console.Clear();
                goto start;

            }


            return false;
 
        }

        static bool ContinuePlaying()
        {
            Console.WriteLine("You are correct! Type yes to play again.");
            string playAgain = Console.ReadLine();
            playAgain = playAgain.ToLower();

            if (playAgain == "yes")
            {
                Console.Clear();
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing!");
                Console.ReadKey();
                return false;
            }
        }

    }
}
