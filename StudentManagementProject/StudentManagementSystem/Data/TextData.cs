using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace StudentManagementSystem.Data
{
    internal class TextData : IDataSource
    {
        private string StudentsSaveFile = $"{Settings.SaveFilePath}\\Students.txt";

        private string CoursesSaveFile = $"{Settings.SaveFilePath}\\Courses.txt";

        #region Parse & Write Methods
        private void RewriteStudentSaveFile(List<Student> studentList)
        {
            File.Create(StudentsSaveFile).Close();

            using (StreamWriter sw = File.AppendText(StudentsSaveFile))
            {
                foreach (var student in studentList)
                {
                    string commaSeparatedCourseIds = ParseCourseListToStringOfIds(student.Courses);

                    sw.WriteLine($"{student.Id},{student.Name},{student.Age}${commaSeparatedCourseIds}");
                }
            }
        }
        private void RewriteCourseSaveFile(List<Course> courseList)
        {
            File.Create(CoursesSaveFile).Close();

            using (StreamWriter sw = File.AppendText(CoursesSaveFile))
            {
                foreach (Course course in courseList)
                {
                    sw.WriteLine($"{course.Id}#{course.Categorey}#{course.Name}#{course.Description}");
                }
            }
        }
        private List<Course> ParseCourseIdsToList(string[] enrolledCourseIds)
        {
            List<Course> enrolledCourses = new List<Course>();
            List<Course> courseList = GetCourses();

            if (enrolledCourseIds[0] != "")
            {
                for (int i = 0; i < enrolledCourseIds.Length; i++)
                {
                    Course course = courseList.SingleOrDefault(c => c.Id == int.Parse(enrolledCourseIds[i]));
                    enrolledCourses.Add(course);
                }
            }
            return enrolledCourses;
        }
        private Category ParseToEnum(string categorey)
        {
            switch (categorey)
            {
                case "Categorey.Science":
                    return Category.Science;
                case "Categorey.Math":
                    return Category.Math;
                case "Categorey.Art":
                    return Category.Art;
                default:
                    return Category.Invalid;

            }
        }
        private string ParseCourseListToStringOfIds(List<Course> courseList)
        {
            string commaSeperateCourseIds = "";

            for (int i = 0; i < courseList.Count; i++)
            {
                if (i != courseList.Count - 1)
                {
                    commaSeperateCourseIds += $"{courseList[i].Id},";
                }
                else
                {
                    commaSeperateCourseIds += $"{courseList[i].Id}";
                }
            }
            return commaSeperateCourseIds;
        }
        #endregion

        #region Datalayer CRUD Methods
        public List<Course> GetCourses()
        {
            var courses = new List<Course>();
            if (File.Exists(CoursesSaveFile))
            {
                using (StreamReader sr = File.OpenText(CoursesSaveFile))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split('#');
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
                        students.Add(student);
                    }
                };
            }
            else
            {
                File.Create(CoursesSaveFile);
            }
            return students;
        }
        public bool EnrollStudentInCourse(Student student, Course courseToEnroll)
        {
            try
            {
                var studentList = GetStudents();

                var studentToUpdate = studentList.SingleOrDefault(s => s.Id == student.Id);
                studentToUpdate.Courses.Add(courseToEnroll);
                RewriteStudentSaveFile(studentList);
            }
            catch 
            {
                return false;
            }
            return true;
        }
        public bool DropCourseFromStudentCourses(Student student, Course course)
        {
            try
            {
                var studentList = GetStudents();

                var studentToUpdate = studentList.SingleOrDefault(s => s.Id == student.Id);
                var courseToDrop = studentToUpdate.Courses.SingleOrDefault(c =>c.Id == course.Id);

                studentToUpdate.Courses.Remove(courseToDrop);
                RewriteStudentSaveFile(studentList);
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
                var studentList = GetStudents();

                var studentToRemove = studentList.SingleOrDefault(s => s.Id == student.Id);
                studentList.Remove(studentToRemove);
                RewriteStudentSaveFile(studentList);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool AddStudent(Student studentToAdd)
        {
            try
            {
                var studentList = GetStudents();

                studentList.Add(studentToAdd);
                RewriteStudentSaveFile(studentList);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool AddCourseToCourseList(Course courseToAdd)
        {
            try
            {
                var courseList = GetCourses();

                courseList.Add(courseToAdd);
                RewriteCourseSaveFile(courseList);
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
                var courseList = GetCourses();
                var course = courseList.SingleOrDefault(c => c.Id == courseId);
                course.Name = name;
                course.Categorey = category;
                course.Description = description;
                RewriteCourseSaveFile(courseList);
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
                var courseList = GetCourses();
                var course = courseList.SingleOrDefault(c => c.Id == courseId);
                courseList.Remove(course);
                RewriteCourseSaveFile(courseList);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
