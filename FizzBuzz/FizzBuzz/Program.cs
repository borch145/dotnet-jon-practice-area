using System;

namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 1; i < 101; i++)
            //{
            //    if (i % 3 != 0 && i % 5 != 0)
            //    {
            //        Console.WriteLine(i);
            //    }
            //    else if (i % 3 == 0 && i % 5 != 0)
            //    {
            //        Console.WriteLine(i + " fizz");
            //    }
            //    else if (i % 3 != 0 && i % 5 == 0)
            //    {
            //        Console.WriteLine(i + " buzz");
            //    }
            //    else
            //    {
            //        Console.WriteLine(i + " fizz buzz");
            //    }
            //}
            

            for (int i = 1; i < 101; i++)
            {
                Console.Write(i + " ");
                if (i % 3 == 0)
                {
                    Console.Write("Fizz ");
                }
                if (i % 5 == 0)
                {
                    Console.Write("Buzz");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        
    }
}
