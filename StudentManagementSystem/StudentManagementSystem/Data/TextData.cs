using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Data
{
    internal class TextData : IDataSource
    {
        public List<Student> Students { get ; set; }
        public List<Course> Courses { get; set; }

        public TextData() 
        {
            Students = PopulateStudents();
            Courses = PopulateCourses();
        }

        private List<Course> PopulateCourses()
        {
            throw new NotImplementedException();
        }

        private List<Student> PopulateStudents()
        {
            throw new NotImplementedException();
        }
    }
}
