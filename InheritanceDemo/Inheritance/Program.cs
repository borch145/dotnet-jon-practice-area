using System;

namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            Goblin goblin = new Goblin(50);  

            Console.WriteLine(goblin.Message + " " + $"({goblin.Health} / {goblin.MaxHealth} HP)");

            Golem golem = new Golem(500);

            Console.WriteLine(golem.Message + " " + $"({golem.Health} / {golem.MaxHealth} HP)");
            /*
             Turnery expression ^ using enemy.IsEvil ? is equivalent to:
             
              if(enemy.isEvil)
            {
                console.WriteLine("Yes!");
            }
            else
            {
                Console.WriteLine("No!");
            }
             
            */
        }
    }
}
