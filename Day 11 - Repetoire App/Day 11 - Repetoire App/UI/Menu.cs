using Day_11___Repetoire_App.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11___Repetoire_App.UI
{
    internal class Menu
    {

        public void Run()
        {
            bool userExit = false;
            while (!userExit)
            {
                Console.Clear();

                Console.WriteLine("-= Welcome to Repertoire Tracker =-");
                Console.WriteLine();
                Console.WriteLine("| 1 | List my repertoire");
                Console.WriteLine("| 2 | Add repertoire");
                Console.WriteLine("|ESC| Quit");
                Console.WriteLine();
                Console.WriteLine("Enter a number to make a selection, or ESC to quit.");

                var cki = Console.ReadKey(true);

                Workflow workflow = new Workflow();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:

                        workflow.ListSongs();
                        
                            break;
                    
                    case ConsoleKey.D2:

                        workflow.AddSong();
                        
                        break;
                    
                    case ConsoleKey.Escape:
                        userExit = true;    
                        break;
                    default:
                        break;
                        
                }
            }
        }
    }
}
