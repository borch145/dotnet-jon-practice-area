using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Models
{
    public class GrowthPoint
    {
        public int XPos;
        public int YPos;

        public GrowthPoint(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;    
        }
    }
}
