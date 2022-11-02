using Day_11_Homework___ToDoApp.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day_11_Homework___ToDoApp.UI
{
    internal class Menu
    {/*
        UI Layer(Presentation)
        -A user can create, access, and modify ToDo lists of some nature
         Create new lists
                >create new or escape
         Create Task or Modify Task/List
                >Choose list or escape 
                    > create task or select Tasks or Rename List or Escape 
                        > Mark Complete/Edit/Delete
         View Lists and Tasks
                >View List >view Incomplete from all lists >View complete from all lists > 
                       |                     |                         |
               |duesoon|create|Del|  |duesoon|markcomp|Del|     |unmark|Del|Del All|
               |ESC|Mark|Rename|     |ESC|                      |ESC|
        */
        public void MainMenu()
        {
            bool userQuit = false;

            while (!userQuit)
            {
                Console.Clear();
                Console.WriteLine("Feeling productive? Knock things out with 2DO using the following options:");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   |  C  | CREATE NEW TODO LIST");
                Console.WriteLine("   |  T  | CREATE AND MODIFY TASKS");
                Console.WriteLine("   |  V  | VIEW LISTS AND TASKS");
                Console.WriteLine("   | ESC | EXIT");
                Console.CursorVisible = false;

                Workflow workflow = new Workflow();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                ConsoleKey key = consoleKeyInfo.Key;

                switch (key)
                {
                    case ConsoleKey.C:

                        MainSub1MenuC();
                        break;

                    case ConsoleKey.T:
                        MainSub2MenuT();
                        break;

                    case ConsoleKey.V:


                        break;

                    case ConsoleKey.Escape:
                        userQuit = true;
                        break;

                    default:
                        break;

                }
            }

        }
        public void MainSub1MenuC()
        {
            bool userQuit = false;

            while (!userQuit)
            {
                Console.Clear();
                Console.WriteLine("Feeling productive? Knock things out with 2DO using the following options:");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("   |  C  | CREATE NEW LIST");
                Console.WriteLine("   | ESC | EXIT");
                Console.CursorVisible = false;

                Workflow workflow = new Workflow();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                ConsoleKey key = consoleKeyInfo.Key;

                switch (key)
                {
                    case ConsoleKey.C:

                        workflow.CreateList();
                        break;

                   
                    case ConsoleKey.Escape:
                        userQuit = true;
                        break;

                    default:
                        break;

                }

            }
        }
       public void MainSub2MenuT()
        {


        }
    }

    

    
}
