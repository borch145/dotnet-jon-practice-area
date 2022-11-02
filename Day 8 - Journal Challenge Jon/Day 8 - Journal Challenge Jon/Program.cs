using System;
using System.Numerics;

namespace Day_8___Journal_Challenge_Jon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Journal App
            //QUESTION: USING THE BREAK FOR THE DELETE OPTION----OK PRACTICE IN THIS CASE? ALTERNATIVE? WHAT IF YOU WANT TO DELETE MULTIPLE ENTRIES AT ONCE? LOOKS LIKE YOU CANT USE A MODIFIED SELECTION IN A
            //LOOP.

            //Functionality:
            //1. User can add a daily journal log
            //2. User can read a journal entry from any given day
            //3. User can add entries separate from daily logs
            //4. User can edit logs from any given entry, daily or special.
            //5. User can delete entries from special log, but CANNOT delete daily logs
            //** There will be a menu which allows the user to choose from options and submenus if needed.
            //New types: DateTime and Lists
            
            Console.WriteLine("Welcome to MyJournal, the one and only Journal application.");
            Console.WriteLine();
            Console.WriteLine("To whom does this Journal belong?");
            string journalName = Console.ReadLine(); 
            Journal journal = new Journal(journalName);


            MainMenu(journal);
            

        }

        public static void MainMenu(Journal journal)
        {
            Console.Clear();
            Console.WriteLine("Welcome to your journal.");
            Console.WriteLine();
            Console.WriteLine("Press a key to navigate the following options:");
            Console.WriteLine();
            Console.WriteLine("  | A | - Add a daily journal entry. ");
            Console.WriteLine("  | R | - Read entries from the journal. ");
            Console.WriteLine("  | S | - Add a special journal entry. ");
            Console.WriteLine("  | E | - Edit a special journal entry. ");
            Console.WriteLine("  | D | - Delete a special journal entry. ");
            bool selectionMade = false;

            while (selectionMade == false)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                ConsoleKey input = keyPress.Key;


                switch (input)
                {
                    case ConsoleKey.A:
                        Console.Clear();
                        journal.AddEntryDaily(journal);
                        selectionMade = true;
                        break;
                    case ConsoleKey.R:
                        Console.Clear();
                        journal.ReadEntry(journal);
                        selectionMade = true;
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        journal.AddEntrySpecial(journal);
                        selectionMade = true;
                        break;
                    case ConsoleKey.E:
                        journal.EditEntry();
                        selectionMade = true;
                        break;
                    case ConsoleKey.D:
                        journal.DeleteEntry();
                        selectionMade = true;
                        break;
                    default:
                        break;
                }

                MainMenu(journal);
            }
            
        }

        

        
    }

}
