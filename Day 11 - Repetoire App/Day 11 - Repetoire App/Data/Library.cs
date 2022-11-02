using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Day_11___Repetoire_App.Models;

namespace Day_11___Repetoire_App.Data
{
    internal class Library
    {
        string SaveFile { get; set; }
        public List<Song> Songs { get; set; }

        public Library()
        {
            SaveFile = "C:\\Users\\Jonquil\\source\\repos\\Day 11 - Repetoire App\\Day 11 - Repetoire App\\Data\\data.txt";
            Songs = new List<Song>();   

            if (File.Exists(SaveFile))
            {
                PopulateSongs();
            }
            else
            {
                File.Create(SaveFile).Close();
            }
          

          
        }
        public void AddSong(string songName)
        {
            Song song = new Song();
            int newId;
            

            if (Songs.Count > 0)
            {
                newId = Songs
                .OrderByDescending(s => s.ID)
                .First()
                .ID + 1;
            }

            else { newId = 1; }
            using(StreamWriter sw = File.AppendText(SaveFile))
            {
                if(newId != 1)
                {
                    sw.Write("\n");
                }    
                
                sw.Write($"{newId},{songName}");
            }
        }

        private void PopulateSongs()
        {
            using (StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while((line = sr.ReadLine()) != null)
                {
                    

                    string[] splitLine = line.Split(',');

                    Song toAdd = new Song()
                    {
                        ID = int.Parse(splitLine[0]),
                        Title = splitLine[1]
                    };

                    Songs.Add(toAdd);

                }
            }
        }

        public List<Song> GetAllSongs()
        {
            return Songs;
        }
    }
}
