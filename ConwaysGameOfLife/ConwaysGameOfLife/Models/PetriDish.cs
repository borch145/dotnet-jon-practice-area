using ConwaysGameOfLife.Data.Starting_Cultures;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife.Models
{
    internal class PetriDish
    {
        public List<Cell> CellCulture { get; set; }

        public PetriDish(IStartingCulture startingCulture)
        {
            CellCulture = new List<Cell>();

            bool isAlive = false;
            for (int x = 1; x < Settings.CultureSizeX+1; x++)
            {
                for (int y = 1; y < Settings.CultureSizeY+1; y++)
                {
                    //maybe simplify with linq ?
                    foreach(GrowthPoint growthPoint in startingCulture.GrowthPoints)
                    {
                        if (x == growthPoint.XPos && y == growthPoint.YPos)
                        {
                            isAlive = true;
                        }
                    }
                    if (isAlive)
                    {
                        Cell cell = new Cell(x, y, 1, false);
                        CellCulture.Add(cell);
                        isAlive = false;
                    }
                    else
                    {
                        Cell cell = new Cell(x, y, 0, false);
                        CellCulture.Add(cell);
                    }
                }
              
            }
           
        }
    }
}
