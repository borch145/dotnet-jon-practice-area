using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Models
{
    internal class Cell
    {
        public int XPos;
        public int YPos;
        public int LifeStatus;
        public bool ChangedState;
        public ConsoleColor AliveColor = Settings.AliveColor;
        public ConsoleColor DeadColor = Settings.DeadColor;

        public Cell(int xPos, int yPos, int lifeStatus, bool changedState)
        {
            XPos = xPos;
            YPos = yPos;
            LifeStatus = lifeStatus;
            ChangedState = changedState;
            
        }
    }
}
