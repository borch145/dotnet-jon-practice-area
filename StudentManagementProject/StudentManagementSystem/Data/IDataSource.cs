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
        public bool DropCourseFromStudentCourses(Student student, Course courseToDrop);
        public bool RemoveStudent(Student student);
        public bool AddStudent (Student student);
        public bool AddCourseToCourseList(Course course);
        public bool EditCourse(int courseId, string name, Category category, string description);
        public bool RemoveCourseFromCourseList(int courseId);
    }
}
