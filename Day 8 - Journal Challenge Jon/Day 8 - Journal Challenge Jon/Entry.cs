using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day_8___Journal_Challenge_Jon
{
    public class Entry
    {
        private DateTime _Date;
        private string Text;
        private bool Special;
        private string Title;

        public Entry(string body, DateTime enteredDate, bool isItSpecial, string title)
            {
            Text = body;
            _Date = enteredDate;
            Special = isItSpecial;
            Title = title;
            }

        public bool GetSpecial()
        {
            return Special;
        }

        public string GetSpecialAsString()
        {
            if (Special)
            {
                return "Special Entry";
            }
            else
            {
                return "Daily Entry";
            }
        }

        public string GetText()
        {
            return Text;
        }
        public DateTime GetDate()
        {
            return _Date;
        }
        public void ReviseText()
        {
            if (Special)
            {
                Console.WriteLine("Please enter new body text for this special entry.");
                Text = Console.ReadLine();
                Console.WriteLine("The text has been edited. Press any key to continue..");
                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Error: Cannot edit body text for non-special entries.");
            }
        }

        public string GetTitle()
        {
            return Title;
        }
        public void ReviseTitle()
        {
            if (Special)
            {
                Console.WriteLine("Please enter new Title for this special entry.");
                Title = Console.ReadLine();
                Console.WriteLine("The title has been edited. Press any key to continue..");
                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Error: Cannot edit body text for non-special entries.");
            }
        }
    }
}
