using System;

namespace SumOfNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string inputString = Console.ReadLine();

            //int sum = 0;

            //foreach (char c in input)
            //{
            //    string toAddChar = c.ToString();
            //    int.TryParse(toAddChar, out int toAddDigit);
            //    sum += toAddDigit;
            //}
            //Console.WriteLine(sum);
            //Console.ReadKey();

            string inputString = Console.ReadLine();
            int.TryParse(inputString, out int inputInt);

            int sum = 0;

            while (inputInt != 0)
            { 
                int digit = inputInt % 10;
                inputInt = inputInt / 10;
                sum+=digit;
            }
            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
