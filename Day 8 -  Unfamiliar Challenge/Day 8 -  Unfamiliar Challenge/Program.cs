using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_8____Unfamiliar_Challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Journal App


            //Functionality:
            //1. User can add a daily journal log
            //2. User can read a journal entry from any given day
            //3. User can add entries separate from daily logs
            //4. User can edit logs from any given entry, daily or special.
            //5. User can delete entries from special log, but CANNOT delete daily logs

            //** There will be a menu which allows the user to choose from options and submenus if needed.

            //New types: DateTime and Lists

            DateTime first = new DateTime(1, 1, 1);
            DateTime second = new DateTime(2, 2, 2);
            DateTime third = new DateTime(3, 3, 3);
            Entry entry1 = new Entry("apples", second );
            Entry entry2 = new Entry("bananas", third);
            Entry entry3 = new Entry("cake", first);
            
            Journal journal = new Journal();

            journal.Entries.Add(entry1);
            journal.Entries.Add(entry2);
            journal.Entries.Add(entry3);
            journal.SortList();

            foreach (Entry entry in journal.Entries)
            {
                Console.WriteLine(entry.Text);
            }
            Console.ReadLine();

        }

        public void TitleScreen()
        {
            Console.WriteLine("Press 1 to add a new daily log");
            Console.WriteLine("Press 2 to add a special entry to any day");
            Console.WriteLine("Press 3 ");
        }
    }
}
