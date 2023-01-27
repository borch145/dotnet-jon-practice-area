using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Data
{
    public interface IDataSource
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
    }
}
