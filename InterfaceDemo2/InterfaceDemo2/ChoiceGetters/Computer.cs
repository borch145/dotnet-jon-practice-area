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
                    return Choice.rock;
                case 2:
                    return Choice.paper;
                case 3:
                    return Choice.scissors;
                default:
                    throw new Exception("Error: RngChoice didn't return a value 1-3");

            }
        }
    }
}
