using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Day_11_Homework___ToDoApp.Models
{//-ToDoItem Class(int id; string title; string body; DateTime created, DateTime complete by)
    internal class ToDoItem
    {
        public int Id { get; set; }
        public string Body;
        public DateTime CreationDate;
        public DateTime CompleteBy;


        public ToDoItem()
        {
            Id = 0;//update this to be self assigning
            Body = AskBody();
            CreationDate = DateTime.Now;
            CompleteBy = AskCompletionDate();
        }

        public string AskBody()
        {
            Console.WriteLine("Please input item description: ");
            string title = Console.ReadLine();
            if (title == "")
            {
                return "Untitled";
            }
            else return title;
        }

        public DateTime AskCompletionDate()
        {
            return //FUCK
        }

    }
}
