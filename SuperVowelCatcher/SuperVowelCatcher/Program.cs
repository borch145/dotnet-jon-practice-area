using System;

namespace SuperVowelCatcher
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Vowel a = new Vowel(50, 5, 5);
            Vowel e = new Vowel(50, 5, 3);

            a.SetName();
            Console.Clear();
            e.SetName();
            Console.Clear();
            Console.WriteLine("Fighters are ready. Press any key to enter battle.");
            Console.ReadKey();

            Battle first = new Battle();

            first.BattleStart(a, e);

        }
    }
}
