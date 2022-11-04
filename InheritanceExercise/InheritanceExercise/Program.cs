using System;

namespace InheritanceExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             Challenge:

            Make a Dog class which contains properties/methods universal to a dog (ex: Name, Bark(), etc...)

            Make a few dog breed classes which contain properties/methods unique to those breeds (ex: FurColor )
            */

            Retriever retriever = new Retriever(true, "Goldy", "Woof Woof", true, 50, "Digging through the trash.");
            Poodle poodle = new Poodle(false, "Piddlepops", "Yip Yip", false, 10, "Fetching the mail.");

            Console.WriteLine($"{retriever.Name} is a {(retriever.IsNaughty ? "Naughty" : "Good")} {(retriever.IsMale ? "boy" : "girl")}.");
            Console.WriteLine(retriever.Name + " is " + retriever.Activity);
            Console.WriteLine($"{poodle.Name} is a {(poodle.IsNaughty ? "Naughty" : "Good")} {(poodle.IsMale ? "boy" : "girl")}.");
            Console.WriteLine(poodle.Name + " is " + poodle.Activity);
            Console.ReadKey();

        }
    }
}
