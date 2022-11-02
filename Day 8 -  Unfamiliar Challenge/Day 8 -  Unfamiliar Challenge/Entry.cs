using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8____Unfamiliar_Challenge
{
    public class Entry
    {
        public DateTime Date;
        public string Text;
       // private bool Deleteable;

        public Entry(string text, DateTime date)
            {
            Text = text;
            Date = date;
            }

    }
}
