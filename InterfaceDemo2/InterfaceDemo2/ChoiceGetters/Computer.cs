using InterfaceDemo2.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace InterfaceDemo2.ChoiceGetters
{
    class Computer : IChoiceGetter
    {
        

        public Choice GetChoice()
        {
            Random rng = new Random();
            int number = rng.Next(1, 4);

            switch(number)
            {
                case 1:
                    return Choice.Rock;
                case 2:
                    return Choice.Paper;
                case 3:
                    return Choice.Scissors;
                default:
                    throw new Exception("Error: RngChoice didn't return a value 1-3");

            }
        }
    }
}
