using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace StudentManagementSystem.Data
{
    internal class TextData : IDataSource
    {
        private string StudentsSaveFile = "C:\\Users\\jonbo\\source\\repos\\StudentManagementSystem\\SchoolData\\Students.txt";

        private string CoursesSaveFile = "C:\\Users\\jonbo\\source\\repos\\StudentManagementSystem\\SchoolData\\Courses.txt";


        public List<Course> GetCourses()
        {
            var courses = new List<Course>();
            if(File.Exists(CoursesSaveFile)) 
            {
                using (StreamReader sr = File.OpenText(StudentsSaveFile))
                {
                    string line = "";

                    while((line = sr.ReadLine())!=null)
                    {
                        string[] splitLine = line.Split(',');
                        Course course = new Course()
                        {
                            Id = int.Parse(splitLine[0]),
                            Categorey = ParseToEnum(splitLine[1]),
                            Name = splitLine[2],
                            Description = splitLine[3]
                        };
                        courses.Add(course);
                    }
                };
            }
            else 
            {
                File.Create(CoursesSaveFile);
            }
            return courses;
        }
        private Categorey ParseToEnum(string categorey)
        {
           switch (categorey) 
            {
                case "Categorey.Science":
                    return Categorey.Science;
                case "Categorey.Math":
                    return Categorey.Math;
                case "Categorey.Art":
                    return Categorey.Art;
                default:
                    return Categorey.Invalid;

            }
        }
        public List<Student> GetStudents()
        {
            var students = new List<Student>();
            if (File.Exists(StudentsSaveFile))
            {
                using (StreamReader sr = File.OpenText(StudentsSaveFile))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] separateCourseList = line.Split('$');
                        string[] separateInfo = separateCourseList[0].Split(',');
                        string[] enrolledCourseIds = separateCourseList[1].Split(',');
                        Student student = new Student()
                        {
                            Id = int.Parse(separateInfo[0]),
                            Name = separateInfo[1],
                            Age = int.Parse(separateInfo[2]),
                            Courses = ParseCourseIdsToList(enrolledCourseIds)
                        };

                    }
                };
            }
            else
            {
                File.Create(CoursesSaveFile);
            }
            return students;
        }
        private List<Course> ParseCourseIdsToList(string[] enrolledCourseIds)
        {
            List<Course> enrolledCourses = new List<Course>();
            List<Course> courseList = GetCourses();

            for (int i = 0; i < enrolledCourseIds.Length; i++)
            {
                Course course = courseList.SingleOrDefault(c => c.Id == int.Parse(enrolledCourseIds[i]));
                enrolledCourses.Add(course);
            }
            return enrolledCourses;
        }
    }
}
