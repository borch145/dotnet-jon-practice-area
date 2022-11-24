using ConwaysGameOfLife.Data;
using ConwaysGameOfLife.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConwaysGameOfLife.Logic
{
    internal class LifeSimulation
    {
        public PetriDish PetriDish;

        public LifeSimulation(PetriDish petriDish)
        {
            PetriDish = petriDish;
        }

        internal void Run()
        {
            RenderStartingDish(PetriDish);
            while (true)
            {
                List<Cell> toRender = AssessDishState(PetriDish);
                RenderNextDishState(toRender);
                Thread.Sleep(Settings.TickSpeed);
            }
        }
        private void RenderStartingDish(PetriDish petriDish)
        {
            for (int x = 0; x < Settings.CultureSizeX + 2; x++)
            {
                for (int y = 0; y < Settings.CultureSizeY + 2; y++)
                {
                    if (x == 0 || x == Settings.CultureSizeX + 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = Settings.BorderColor;
                        Console.BackgroundColor = Settings.PetriColor;
                        Console.Write(Settings.BorderCharacterY);
                    }
                    else if (y == 0 && x != 0 || y == Settings.CultureSizeY + 1 && x != Settings.CultureSizeX + 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = Settings.BorderColor;
                        Console.BackgroundColor = Settings.PetriColor;
                        Console.Write(Settings.BorderCharacterX);
                    }

                    else
                    {
                        Cell cell = petriDish.CellCulture.Find(c => c.XPos == x && c.YPos == y);
                        Console.SetCursorPosition(x, y);
                        Console.BackgroundColor = Settings.PetriColor;
                        if (cell.LifeStatus == 1)
                        {
                            Console.ForegroundColor = cell.AliveColor;
                            Console.Write(Settings.AliveCharacter);
                        }
                        else
                        {
                            Console.ForegroundColor = cell.DeadColor;
                            Console.Write(Settings.DeadCharacter);
                        }
                    }
                }
            }
        }
        private void RenderNextDishState(List<Cell> toRender)
        {
            foreach(Cell cell in toRender)
            {
                if (cell.LifeStatus == 1)
                {
                    Console.ForegroundColor = cell.AliveColor;
                    Console.SetCursorPosition(cell.XPos,cell.YPos);
                    Console.WriteLine(Settings.AliveCharacter);
                }
                else if (cell.LifeStatus == 0)
                {
                    Console.ForegroundColor = cell.DeadColor;
                    Console.SetCursorPosition(cell.XPos, cell.YPos);
                    Console.WriteLine(Settings.DeadCharacter);
                }
            }
        }
        private List<Cell> AssessDishState(PetriDish petriDish)
        {
            List<Cell> tempCulture = new List<Cell>();

            foreach (Cell cell in petriDish.CellCulture)
            {
                int neighborValues = GetNeighborValues(cell, petriDish);
                Cell tempCell = UpdateCell(cell, neighborValues);
                if (tempCell.ChangedState)
                {
                    tempCulture.Add(tempCell);
                }
            }
            return tempCulture;
            
        }
        private int GetNeighborValues(Cell cell, PetriDish petriDish)
        {
            int neighborValues = 0;
            
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    
                    if (x == 0 && y == 0) 
                    { 
                    }
                    else if (PetriDish.CellCulture.SingleOrDefault(c => c.XPos == cell.XPos + x && c.YPos == cell.YPos + y) != null)
                    {
                        neighborValues += petriDish.CellCulture.SingleOrDefault(c => c.XPos == cell.XPos + x && c.YPos == cell.YPos + y).LifeStatus;
                    }
                }
            }
            return neighborValues;
        }
        private Cell UpdateCell(Cell cell, int neighborValues)
        {
           if (cell.LifeStatus == 1 && (neighborValues == 2 || neighborValues == 3))
           {
           }
           else if (cell.LifeStatus == 0 && neighborValues == 3)
           {
                cell.LifeStatus = 1;
                cell.ChangedState = true;
           }
           else if (cell.LifeStatus ==1)
           {
                cell.LifeStatus = 0;
                cell.ChangedState = true;
           }
           
           return cell;
           
        }
        
    }
}
