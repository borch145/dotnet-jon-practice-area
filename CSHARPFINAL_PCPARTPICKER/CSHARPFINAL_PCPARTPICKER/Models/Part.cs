using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PartCategory Category { get; set; }
        public decimal Cost { get; set; }
        public int NumberInStock { get; set; }
    }
}
