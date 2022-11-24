using ConwaysGameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Data.Starting_Cultures
{
    interface IStartingCulture
    {
        public List<GrowthPoint> GrowthPoints { get; set; }
    }
}
