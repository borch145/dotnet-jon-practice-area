using ConwaysGameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Data.Starting_Cultures
{
    public class TestCulture : IStartingCulture
    {
        public List<GrowthPoint> GrowthPoints { get; set; }
        
        public TestCulture()
        {
            GrowthPoints = new List<GrowthPoint>();

            int[] growthCoordinates = Settings.TestCultureArray;
            for(int i = 0; i < growthCoordinates.Length; i += 2)
            {
                GrowthPoint growthPoint = new GrowthPoint(growthCoordinates[i], growthCoordinates[i + 1]);
                GrowthPoints.Add(growthPoint);  
            }
        }

        
    }
}
