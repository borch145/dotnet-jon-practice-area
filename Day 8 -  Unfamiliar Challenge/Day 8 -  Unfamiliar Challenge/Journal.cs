using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8____Unfamiliar_Challenge
{
    public class Journal
    {
        public List<Entry> Entries;

        public Journal()
        {
            Entries = new List<Entry>();
        }

        public void SortList()
        {
            Entries = Entries.OrderBy(o=>o.Date).ToList();
           
        }

    }



}
