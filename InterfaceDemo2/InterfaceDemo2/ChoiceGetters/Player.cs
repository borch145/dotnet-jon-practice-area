using InterfaceDemo2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDemo2.ChoiceGetters
{
    class Player : IChoiceGetter
    {
        public Choice GetChoice()
        {
            bool validInput = false;
            string input = "";
            Choice choice = new Choice();

            while (!validInput)
            {
                Console.WriteLine("Please choose R, P, or S: ");
                input = Console.ReadLine().ToLower();

                if (input != "r" && input != "p" && input!= "s")
                {
                    Console.WriteLine(input + " was not a valid choice. Press any key to retry.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    validInput = true;
                }
            }
            switch (input)
            {
                case "r":
                    choice = Choice.rock;
                    break;
                case "p":
                    choice = Choice.paper;
                    break;
                case "s":
                    choice = Choice.scissors;
                    break;
                default:
                    throw new Exception("Player GetChoice input did not return a valid choice");
            }
            return choice;
        }

    }
}
