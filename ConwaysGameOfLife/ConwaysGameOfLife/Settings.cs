using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife
{
    static class Settings
    {
        public static string AliveCharacter = "#";
        public static string DeadCharacter = " ";
        public static string BorderCharacterY = "|";
        public static string BorderCharacterX = "=";
        public static ConsoleColor PetriColor = ConsoleColor.DarkCyan;
        public static ConsoleColor BorderColor = ConsoleColor.Cyan;
        public static ConsoleColor AliveColor = ConsoleColor.Green;
        public static ConsoleColor DeadColor = ConsoleColor.Red;
        public static int TickSpeed = 000;
        public static int CultureSizeX = 65;
        public static int CultureSizeY = 25;
        public static int WindowSizeX = CultureSizeX + 2;
        public static int WindowSizeY = CultureSizeY + 2;
        public static int[] TestCultureArray = new int[18] {4,5,4,6,5,6,1,1,1,2,2,1,2,2,2,3,2,4};


}
}
