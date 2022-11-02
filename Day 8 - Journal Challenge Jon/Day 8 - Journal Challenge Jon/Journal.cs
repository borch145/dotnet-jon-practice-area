using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day_8___Journal_Challenge_Jon
{
    internal class Journal
    {
        private List<Entry> Entries;
        private string Name;


        public Journal(string journalName)

        {
            Entries = new List<Entry>();
            
            Name = journalName;
        }

        public void AddToEntries(Entry toEnter)
        {
            
                Entries.Add(toEnter);
           
        }

        public void AddEntryDaily(Journal journal)
        {
            bool makeEntry = true;
            bool emptyDate = false;
            DateTime enteredDate = new ();
            // THIS IS BREAKING BECAUSE THE LOGIC SUCKS IF A SPECIAL ENTRY EXISTS WHEN THE WHILE LOOP IS CALLED
            //MAYBE ADD A BOOL 
            

            while (!emptyDate)
            {
                PrintEntryInfo();
                Console.WriteLine("Please input a date for this entry.");
                Console.WriteLine();
                enteredDate = AskDate();
                Console.Clear();


                if (Entries.Count > 0)
                {
                    foreach (Entry existingEntry in Entries)
                    {
                        if (existingEntry.GetDate() == enteredDate && existingEntry.GetSpecial() == false)
                        {
                            Console.WriteLine("There is already an entry for this date. Select a new date or go to main menu.");
                            

                            Console.WriteLine();
                            Console.WriteLine("    |   D   | Enter a new date");
                            Console.WriteLine("    | ENTER | Return to main menu.");
                            ConsoleKeyInfo keyPress = Console.ReadKey(true);
                            ConsoleKey input = keyPress.Key;

                            switch (input)

                            {
                                case ConsoleKey.D:
                                    Console.Clear();
                                    break;

                                case ConsoleKey.Enter:
                                    makeEntry = false;
                                    emptyDate = true;
                                    Console.Clear();
                                    break;
                            }
                            break;
                            
                        }

                        

                        else
                        {   
                            emptyDate = true;
                        }
                    }
                }
                else emptyDate = true;
            }

            if (makeEntry)
            {

                Console.WriteLine(enteredDate.ToString("MM/dd/yyyy") + " will be the date of the entry.");
                Console.WriteLine("Please enter the title for this entry.");
                string title = Console.ReadLine();
                Console.WriteLine("Please enter text body for this entry.");
                string body = Console.ReadLine();
                Entry entry = new Entry(body, enteredDate, false, title);

                Console.Clear();
                journal.AddToEntries(entry);
                Console.WriteLine("Your entry has been recorded. Press any key to return to main menu.");
                Console.ReadKey();
            }


        }

        public void AddEntrySpecial(Journal journal)
        {
            bool emptyDate = false;
            DateTime enteredDate = new();
            bool wasRevised = false;
            

            while (!emptyDate)
            {
                PrintEntryInfo();
                Console.WriteLine("Please input a date for this entry.");
                Console.WriteLine();
                enteredDate = AskDate();
                Console.Clear();


                if (Entries.Count > 0)
                {
                    foreach (Entry existingEntry in Entries)
                    {
                        if (existingEntry.GetDate() == enteredDate && existingEntry.GetSpecial() == true)
                        {
                            Console.WriteLine("There is already a special entry for this date:");
                            Console.WriteLine();
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine("Title: " + existingEntry.GetTitle());
                            Console.WriteLine("Entry Date: " + existingEntry.GetDate().ToString("MM/dd/yyyy"));
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine(existingEntry.GetSpecialAsString()); ;
                            Console.WriteLine();
                            Console.WriteLine("Entry Body:");
                            Console.WriteLine();
                            Console.WriteLine(existingEntry.GetText());
                            Console.WriteLine();
                            Console.WriteLine("=======================================================");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Would you like to overwrite this entry?");
                            Console.WriteLine();
                            Console.WriteLine("   | Y | Overwrite Entry.");
                            Console.WriteLine("   | N | Return to Main Menu.");
                            ConsoleKeyInfo keyPress = Console.ReadKey(true);
                            ConsoleKey input = keyPress.Key;

                            switch(input)
                                
                            {
                                case ConsoleKey.Y:
                                    Console.WriteLine();
                                existingEntry.ReviseTitle();
                                    Console.WriteLine();
                                existingEntry.ReviseText();
                                wasRevised = true;
                                emptyDate= true;
                                    break;

                                case ConsoleKey.N:
                                    wasRevised= true;
                                    break;
                            }
                            break;
                        }

                        else emptyDate = true;
                    }
                }
                else emptyDate = true;
            }

            if (!wasRevised)
            {
                Console.WriteLine(enteredDate.ToString("MM/dd/yyyy") + " will be the date of the entry.");
                Console.WriteLine("Please enter the title for this entry.");
                string title = Console.ReadLine();
                Console.WriteLine("Please enter text body for this entry.");
                string body = Console.ReadLine();
                Entry entry = new Entry(body, enteredDate, true, title);

                Console.Clear();
                journal.AddToEntries(entry);
                Console.WriteLine("Your entry has been recorded. Press any key to return to main menu.");
                Console.ReadKey();
            }



        }

        public void ReadEntry(Journal journal)
        {
            Console.WriteLine("Would you like to see an entry for a specific day?");
            Console.WriteLine();
            Console.WriteLine("| Enter | - View All Entries.");
            Console.WriteLine("|   A   | - View specific day.");

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            ConsoleKey input = keyPress.Key;
            bool selectionMade = false;
            bool entryFound = false;
            

            while (!selectionMade)
            {
                switch (input)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        SortEntriesByDate();
                        foreach (Entry entry in Entries)
                        {
                            
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine("Title: " + entry.GetTitle());
                            Console.WriteLine("Entry Date: " + entry.GetDate().ToString("MM/dd/yyyy"));
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine(entry.GetSpecialAsString());
                            Console.WriteLine();
                            Console.WriteLine("Entry Body:");
                            Console.WriteLine();
                            Console.WriteLine(entry.GetText());
                            Console.WriteLine();
                            Console.WriteLine("=======================================================");
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        selectionMade = true;
                        break;
                    
                    case ConsoleKey.A:
                        
                        Console.Clear();
                        PrintEntryInfo();

                        Console.WriteLine("For what date would you like to view entries?");
                        DateTime printDate = AskDate();
                        Console.Clear();

                        foreach (Entry entry in Entries)
                        {
                            if (printDate == entry.GetDate())
                            {
                                
                                Console.WriteLine("_______________________________________________________");
                                Console.WriteLine("Title: " + entry.GetTitle());
                                Console.WriteLine("Entry Date: " + entry.GetDate().ToString("MM/dd/yyyy"));
                                Console.WriteLine("_______________________________________________________");
                                Console.WriteLine(entry.GetSpecialAsString());
                                Console.WriteLine();
                                Console.WriteLine("Entry Body:");
                                Console.WriteLine();
                                Console.WriteLine(entry.GetText());
                                Console.WriteLine();
                                Console.WriteLine("=======================================================");
                                Console.WriteLine();
                                Console.WriteLine();
                                entryFound = true;
                            }
                            
                        }
                        if (!entryFound)
                        {
                            Console.WriteLine("There are no entries for " + printDate.ToString("MM/dd/yyyy"));
                        } 

                        selectionMade = true;
                        break;
                        
                    default:
                            break;

                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey();
            }
        }

        public DateTime AskDate()
        {
            bool validYear = false;
            bool validMonth = false;
            bool validDay = false;
            int enteredYear = 0;
            int enteredMonth = 0;
            int enteredDay = 0;



            while (!validYear)
            {
                Console.WriteLine("Enter a year.");
                string typedYear = Console.ReadLine();
                validYear = int.TryParse(typedYear, out enteredYear);
                if (validYear)
                {
                    Console.WriteLine("Year set to " + enteredYear);
                }
                else Console.WriteLine("Entry is invalid.");
            }
            while (!validMonth || enteredMonth < 1 || enteredMonth > 12)
            {
                Console.WriteLine("Enter a month using 1-12");
                string typedMonth = Console.ReadLine();
                validMonth = int.TryParse(typedMonth, out enteredMonth);

                if (validMonth && (enteredMonth >= 1 && enteredMonth <= 12))
                {
                    Console.WriteLine("Month set to " + enteredMonth);
                }
                else
                {
                    Console.WriteLine("Entry is invalid.");
                }
            }

            while (!validDay || enteredDay < 1 || enteredDay > 31)
            {
                Console.WriteLine("Enter a day using 1-31");
                string typedDay = Console.ReadLine();
                validDay = int.TryParse(typedDay, out enteredDay);

                if (validDay && (enteredDay >= 1 && enteredDay <= 31))
                {
                    Console.WriteLine("Day set to " + enteredDay);
                }
                else Console.WriteLine("Entry is invalid.");
            }


            DateTime enteredDate = new (enteredYear, enteredMonth, enteredDay);
            return enteredDate;

        }

        public void EditEntry()
        {
            DateTime enteredDate = new();
            bool wasRevised = false;
            bool emptyDate = true;
            bool returnToMenu = false;
            Console.Clear();


            while (emptyDate)
            {
                PrintEntryInfo();
                Console.WriteLine("Please input a date for the entry you'd like to edit.");
                Console.WriteLine();
                enteredDate = AskDate();
                


                if (Entries.Count > 0)
                {
                    foreach (Entry existingEntry in Entries)
                    {
                        if (existingEntry.GetDate() == enteredDate && existingEntry.GetSpecial() == true)
                        {
                            Console.WriteLine("This date has the following special entries:");
                            Console.WriteLine();
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine("Entry Date: " + existingEntry.GetDate().ToString("MM/dd/yyyy"));
                            Console.WriteLine("_______________________________________________________");
                            Console.WriteLine("Entry Type Special: " + existingEntry.GetSpecial());
                            Console.WriteLine();
                            Console.WriteLine("Entry Body:");
                            Console.WriteLine();
                            Console.WriteLine(existingEntry.GetText());
                            Console.WriteLine();
                            Console.WriteLine("=======================================================");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Would you like to overwrite the special entry? Press Y to overwrite or N to search a new date.");
                            Console.WriteLine();
                            Console.WriteLine("    |   Y   | Overwrite Entry");
                            Console.WriteLine("    |   N   | Search new date");
                            Console.WriteLine("    | ENTER | Return to main menu.");
                            ConsoleKeyInfo keyPress = Console.ReadKey(true);
                            ConsoleKey input = keyPress.Key;

                            switch (input)

                            {
                                case ConsoleKey.Y:
                                    existingEntry.ReviseTitle();
                                    existingEntry.ReviseText();
                                    wasRevised = true;
                                    emptyDate = false;
                                    break;

                                case ConsoleKey.N:
                                    Console.Clear();
                                    break;

                                case ConsoleKey.Enter:
                                    emptyDate = false;
                                    break;
                            }
                            break;

                        }

                        else
                        {
                            
                            emptyDate = true;
                            
                        }
                    }
                }
                else
                 {
                    
                    emptyDate = true;
                    

                  }
                if (emptyDate)
                 {
                    Console.WriteLine("No special entries exist for this date. Press any key to re-enter.");
                    Console.ReadKey();
                    Console.Clear();
                 }
            }

           if (!wasRevised)
            {
                Console.Clear();
                Console.WriteLine("No revisions were made. Press any key to return to main menu.");
                Console.ReadKey();
            }
            
        }

        public void DeleteEntry()
        {

            bool returnToMainMenu = false;
            bool selectionMade = false;

            while (!returnToMainMenu)
            {
                Console.Clear();
                PrintEntryInfo();
             
                bool deletionSuccess = false;


                Console.WriteLine("Select an option....");
                Console.WriteLine();
                Console.WriteLine("   |   D   | - Delete Special Entry by Date.");
                Console.WriteLine("   | Enter | - Return to Main Menu.");
                Console.WriteLine();

                

                while (!selectionMade)
                {
                    ConsoleKeyInfo keyPress = Console.ReadKey(true);
                    ConsoleKey input = keyPress.Key;

                    switch (input)
                    {
                        case ConsoleKey.D:
                            DateTime entryDate = AskDate();
                            foreach (Entry currentEntry in Entries)
                            {
                                if (currentEntry.GetDate() == entryDate && currentEntry.GetSpecial() == true)
                                {
                                    Entries.Remove(currentEntry);
                                    Console.WriteLine("Entry has been deleted. Press any key to return to main menu");
                                    deletionSuccess = true;
                                    returnToMainMenu = true;
                                    selectionMade = true;
                                    Console.ReadKey();
                                    break;
                                }

                            }
                            break;

                        case ConsoleKey.Enter:
                            returnToMainMenu = true;
                            selectionMade = true;
                            break;
                        default:
                            break;

                    }
                }

                if (!deletionSuccess && !returnToMainMenu)
                {
                    Console.WriteLine("Error: There are no special entries on this date. No deletion performed. ***NOTE: DAILY ENTRIES CANNOT BE DELETED***");
                    Console.WriteLine("Press any key to retry.");
                    Console.ReadKey(true);
                }

            }
        }

        public void PrintEntryInfo()
        {
            SortEntriesByDate();
            Console.WriteLine("The following entries have been made: ");
            Console.WriteLine("===============================================");
            Console.WriteLine("|    Date   |       Title       |     Type    |");
            Console.WriteLine("===============================================");
            foreach (Entry currentEntries in Entries)
            {
                Console.WriteLine(currentEntries.GetDate().ToString("MM/dd/yyyy") + "     " + currentEntries.GetTitle() + "   " + currentEntries.GetSpecialAsString());
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine();
        }

        public void SortEntriesByDate()
        {
            Entries = Entries.OrderBy(o=>o.GetDate()).ToList();
        }
    }
}
