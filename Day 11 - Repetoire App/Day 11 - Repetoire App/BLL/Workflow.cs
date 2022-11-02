using Day_11___Repetoire_App.Data;
using Day_11___Repetoire_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11___Repetoire_App.BLL
{
    internal class Workflow
    {

        public void AddSong()
        {
            Console.Clear();

            Console.WriteLine("Please enter the name of the song to add: ");
            string songName = Console.ReadLine();
            
            if(songName =="")
            {
                songName = "Untitlied";
            }

            Library library = new Library();
            library.AddSong(songName);
        }

        internal void ListSongs()
        {
            Library library = new Library();
            foreach (Song song in library.Songs)
            {
                Console.WriteLine(song.ID + ": " + song.Title);
            }
            
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
