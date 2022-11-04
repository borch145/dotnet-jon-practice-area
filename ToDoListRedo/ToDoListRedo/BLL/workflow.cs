using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoListRedo.Data;
using ToDoListRedo.Models;

namespace ToDoListRedo.BLL
{
    internal class workflow
    {
        public void AddItem()
        {
            Console.Clear();

            Console.WriteLine("Please enter your ToDoItem: ");
            string itemBody = Console.ReadLine();

            if (itemBody == "")
            {
                itemBody = "Untitlied";
            }

            ToDoList itemList = new ToDoList();
            itemList.AddItem(itemBody);
        }

        internal void DeleteItem()
        {
            throw new NotImplementedException();
        }

        internal void EditItem()
        {
            Console.Clear();
            ListItems();
            Console.WriteLine("Which item would you like to edit?");
            int idSelection =
        }

        internal void ListItems()
        {
            ToDoList itemList = new ToDoList();
            foreach (ToDoItem item in itemList.Items)
            {
                Console.WriteLine(item.ID + ": " + item.body);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
