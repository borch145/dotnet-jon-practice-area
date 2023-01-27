using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Data
{
    public interface IDataSource
    {
        public List<Student> GetStudents();
        public List<Course> GetCourses();
        public bool EnrollStudentInCourse(Student student, Course courseToEnroll);
    }
}
