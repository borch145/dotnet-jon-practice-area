using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace StudentManagementSystem.Data
{
    internal class InMemoryData : IDataSource
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; } 
        public InMemoryData() 
        {
            Courses = new List<Course>()
            {
                new Course() 
                {
                    Id= 1,
                    Categorey=Categorey.Science,
                    Name= "Intro to Chemistry",
                    Description = "This course focuses on the basics of chemistry. Content includes introduction to the periodic table of elements, Bohr modeling, elemental composition, chemical compound formation and nomenclature."
                },
                new Course()
                {
                    Id=2,
                    Categorey=Categorey.Math,
                    Name= "Ancient Egyptian Algebra",
                    Description= "Prove your academic might in this exciting cryptic course. Reign supreme over your Current Egyptian Algebra peers."
                },
                new Course()
                {
                    Id=3,
                    Categorey=Categorey.Math,
                    Name= "Current Egyptian Algebra",
                    Description= "A softball option for those looking for a modern twist on the classic math of alegbra."
                },
                new Course()
                {
                    Id=4,
                    Categorey=Categorey.Art,
                    Name= "Intro to Graphic Design",
                    Description= "Learn the basics of graphic software and 3D modeling in this exposure to digital arts and media."
                },

            };

            Students = new List<Student>()
            {
                new Student()
                {
                    Id= 1,
                    Name= "Doug",
                    Age= 18,
                    Courses= new List<Course>()
                    {
                        Courses[0],
                        Courses[1]
                    }
                },
                 new Student()
                {
                    Id= 2,
                    Name= "Jungy",
                    Age= 30,
                    Courses= new List<Course>()
                    {
                        Courses[2],
                        Courses[3]
                    }
                },

            };
        }
    }
}
