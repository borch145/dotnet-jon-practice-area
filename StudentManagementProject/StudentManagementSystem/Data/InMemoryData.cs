using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace StudentManagementSystem.Data
{
    internal class InMemoryData : IDataSource
    {
        private List<Student> Students { get; set; }
        private List<Course> Courses { get; set; } 
        public InMemoryData() 
        {
            Courses = new List<Course>()
            {
                new Course() 
                {
                    Id= 1,
                    Categorey=Category.Science,
                    Name= "Intro to Chemistry",
                    Description = "This course focuses on the basics of chemistry. Content includes introduction to the periodic table of elements, Bohr modeling, elemental composition, chemical compound formation and nomenclature."
                },
                new Course()
                {
                    Id=2,
                    Categorey=Category.Math,
                    Name= "Ancient Egyptian Algebra",
                    Description= "Prove your academic might in this exciting cryptic course. Reign supreme over your Current Egyptian Algebra peers."
                },
                new Course()
                {
                    Id=3,
                    Categorey=Category.Math,
                    Name= "Current Egyptian Algebra",
                    Description= "A softball option for those looking for a modern twist on the classic math of alegbra."
                },
                new Course()
                {
                    Id=4,
                    Categorey=Category.Art,
                    Name= "Intro to Graphic Design",
                    Description= "Learn the basics of graphic software and 3D modeling in this exposure to digital arts and media."
                },

            };

            Students = new List<Student>()
            {
                new Student()
                {
                    Id= 1,
                    Name= "Doug Dimmadome",
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
                    Name= "Jungy Bornello",
                    Age= 30,
                    Courses= new List<Course>()
                    {
                        Courses[2],
                        Courses[3]
                    }
                },

            };
        }

        public List<Student> GetStudents()
        {
            return Students;
        }
        public List<Course> GetCourses()
        {
            return Courses;
        }
        public bool EnrollStudentInCourse(Student student, Course courseToEnroll)
        {
            try
            {
                student.Courses.Add(courseToEnroll);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool DropCourseFromStudentCourses(Student student, Course courseToDrop)
        {
            try
            {
                student.Courses.Remove(courseToDrop);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool RemoveStudent(Student student)
        {
            try
            {
                Students.Remove(student);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool AddStudent(Student student)
        {
            try
            {
                Students.Add(student);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool AddCourseToCourseList(Course course)
        {
            try
            {
                Courses.Add(course);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool EditCourse(int courseId, string name, Category category, string description)
        {
            try
            {
                var course = Courses.SingleOrDefault(c => c.Id == courseId);
                course.Name = name;
                course.Categorey = category;
                course.Description = description;
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool RemoveCourseFromCourseList(int courseId)
        {
            try
            {
                var course = Courses.SingleOrDefault(c => c.Id == courseId);
                Courses.Remove(course);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
