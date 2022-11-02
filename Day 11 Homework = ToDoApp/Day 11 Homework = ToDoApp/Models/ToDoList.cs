using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11_Homework___ToDoApp.Models
{
    internal class ToDoList
    {
        //-ToDoList (int id; string title; List<ToDoItem> toDoList; DateTime created)

        public int Id;
        public DateTime CreationDate;
        public string Title;
        public List<ToDoItem> Items;

       

        public ToDoList()
        {
            Id = 0; //COME BACK TO MAKE THIS SELF ASSIGNING
            Items = new List<ToDoItem>();
            CreationDate = DateTime.Now;
            Title = AskTitle();
        }

        private string AskTitle()
        {
            Console.WriteLine("Please title this list: ");
            string title = Console.ReadLine();
            if (title == "")
            {
                return "Untitled";
            }
            else return title;
        }

        public void SaveList()
        {

        }
            


            
    }
}
