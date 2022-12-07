using System;
using System.Collections.Generic;
using System.Text;

namespace CSHARPFINAL_PCPARTPICKER.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Part> Parts { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
