using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListRedo.BLL;

namespace ToDoListRedo.UI
{
    internal class Menu
    {
        public void Run()
        {
            bool userQuit = false;
            while (!userQuit)
            {
                Console.Clear();

                Console.WriteLine("This is your ToDo List.");
                Console.WriteLine();
                Console.WriteLine("| 1 | View List");
                Console.WriteLine("| 2 | Add Item");
                Console.WriteLine("| 3 | Edit Item");
                Console.WriteLine("| 4 | Delete Item");
                Console.WriteLine("|ESC| Quit");
                Console.WriteLine();
                Console.WriteLine("Enter a number to make a selection, or ESC to quit.");

                var cki = Console.ReadKey(true);
                workflow workflow = new workflow();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:

                        workflow.ListItems();

                        break;

                    case ConsoleKey.D2:

                        workflow.AddItem();

                        break;

                    case ConsoleKey.D3:

                        workflow.EditItem();

                        break;

                    case ConsoleKey.D4:

                        workflow.DeleteItem();

                        break;

                    case ConsoleKey.Escape:
                        userQuit = true;
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
