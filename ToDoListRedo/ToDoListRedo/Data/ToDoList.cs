using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListRedo.Models;
using System.IO;


namespace ToDoListRedo.Data
{
    internal class ToDoList
    {
        public string SaveFile;
        public List<ToDoItem> Items;

        public ToDoList()
        {
            SaveFile = "C:\\Users\\Jonquil\\source\\repos\\ToDoListRedo\\ToDoListRedo\\Data\\data.txt";
            Items = new List<ToDoItem>();

            if (File.Exists(SaveFile))
            {
                PopulateList();
            }
            else
            {
                File.Create(SaveFile).Close();
            }



        }

        private void PopulateList()
        {
            using (StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {


                    string[] splitLine = line.Split(',');

                    ToDoItem toAdd = new ToDoItem()
                    {
                        ID = int.Parse(splitLine[0]),
                        body = splitLine[1]
                    };

                    Items.Add(toAdd);

                }
            }
        }

        public void AddItem(string itemBody)
        {
            ToDoItem item = new ToDoItem();
            int newId;


            if (Items.Count > 0)
            {
                newId = Items
                .OrderByDescending(s => s.ID)
                .First()
                .ID + 1;
            }

            else { newId = 1; }
            using (StreamWriter sw = File.AppendText(SaveFile))
            {
                if (newId != 1)
                {
                    sw.Write("\n");
                }

                sw.Write($"{newId},{itemBody}");
            }
        }


        public List<ToDoItem> GetAllItems()
        {
            return Items;
        }
    }
}
