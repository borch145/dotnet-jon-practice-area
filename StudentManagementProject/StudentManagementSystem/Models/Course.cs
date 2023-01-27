using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public Category Categorey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
