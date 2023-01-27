using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementSystem.Logic
{
    public class Manager
    {
        public IDataSource DataSource { get; set; }
        public Manager() 
        {
            DataSource = Settings.DataSource;
        }

        public Category ParseEnumIndexToCategorey(int categoreyIndex)
        {
            switch (categoreyIndex)
            {
                case 0:
                    return Category.Science;
                case 1:
                    return Category.Art;
                case 2:
                    return Category.Math;
                default:
                    return Category.Invalid;

            }
        }
        public List<Student> GetStudents()
        {
            return DataSource.GetStudents();
        }
        public List<Course> GetCourses()
        {
            return DataSource.GetCourses();
        }
        public Response EnrollInCourse(int courseId, int studentId)
        {
            Response response = new Response();

            var student = DataSource.GetStudents().SingleOrDefault(s => s.Id == studentId);
            var course = DataSource.GetCourses().SingleOrDefault(c => c.Id == courseId);

            if (student == null & course == null)
            {
                response.Success = false;
                response.Message = "Error: input student.Id and course.Id not found in datasource.";
                return response;
            }
            else if (student == null)
            {
                response.Success = false;
                response.Message = "Error: input student.Id not found in datasource.";
                return response;
            }
            else if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: input course.Id not found in datasource.";
                return response;
            }
            else
            {
                bool courseEnrollSuccess = DataSource.EnrollStudentInCourse(student, course);

                if (courseEnrollSuccess)
                {
                    response.Success = true;
                    response.Message = $"{student.Name} has been enrolled in {course.Name}";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"There was an error in writing {student.Name}'s {course.Name} to the datasource.";
                }
                return response;
            }
        }
        public Response DropCourse(int courseId, int studentId)
        {
            Response response = new Response();

            var student = DataSource.GetStudents().SingleOrDefault(s => s.Id == studentId);
            var course = DataSource.GetCourses().SingleOrDefault(c => c.Id == courseId);

            if (student == null & course == null)
            {
                response.Success = false;
                response.Message = "Error: input student.Id and course.Id not found in datasource.";
                return response;
            }
            else if (student == null)
            {
                response.Success = false;
                response.Message = "Error: input student.Id not found in datasource.";
                return response;
            }
            else if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: input course.Id not found in datasource.";
                return response;
            }
            else
            {
                bool dropCourseSuccess = DataSource.DropCourseFromStudentCourses(student, course);

                if (dropCourseSuccess)
                {
                    response.Success = true;
                    response.Message = $"{student.Name} has been dropped from {course.Name}";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"There was an error in writing {student.Name}'s drop of {course.Name} to the datasource.";
                }
                return response;
            }
        }
        public Response RemoveStudent(int studentId)
        {
            Response response = new Response();
            var student = DataSource.GetStudents().SingleOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                response.Success = false;
                response.Message = "Error: input student ID not found in datasource.";
                return response;
            }
            else
            {
                bool studentRemovalSuccess = DataSource.RemoveStudent(student);
                
                if (studentRemovalSuccess)
                {
                    response.Success = true;
                    response.Message = "Student has been removed.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"There was an error in dropping {student.Name} from the datasource.";
                }
                return response;
            }
        }
        public Response AddStudent(string name, int age)
        {
            Response response = new Response();

            var student = new Student()
            {
                Name = name,
                Age = age,
                Id = DataSource.GetStudents().Max(s => s.Id) + 1,
                Courses = new List<Course>()
            };

            if (student.Name != null & student.Age != 0 & student.Id != 0)
            {
                
                bool addStudentSuccess = DataSource.AddStudent(student);
              
                if(!addStudentSuccess)
                {
                    response.Success = false;
                    response.Message = "AddStudent() failed to add new student to datasource.";
                }
                else
                {
                    response.Success = true;  
                    response.Message = $"{student.Name} has been added!";
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Error: Request failed. Invalid assignment of Name, Age, or Id Student property at AddStudent()";
            }

            return response;
        }
        public Response AddCourseToCourseList(string name, int category, string description)
        {
            Response response = new Response();

            var course = new Course()
            {
                Id = DataSource.GetCourses().Max(c => c.Id) + 1,
                Categorey = ParseEnumIndexToCategorey(category),
                Name = name,
                Description = description
            };

            if (course.Name != null & course.Description != null)
            {
                bool addCourseSuccess = DataSource.AddCourseToCourseList(course);
                  
                if(!addCourseSuccess)
                {
                    response.Success = false;
                    response.Message = "AddCourseToCourseList() failed to add new course to datasource.";
                }
                else
                {
                    response.Message = $"{course.Name} has been added!";
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Error: Request failed. Invalid assignment of Name or Description property at AddCourse()";
            }

            return response;
        }
        public Response EditCourse(int id, string name, int category, string description)
        {
            Response response = new Response();
            var course = DataSource.GetCourses().SingleOrDefault(c => c.Id == id);
            var parsedCategory = ParseEnumIndexToCategorey(category);

            if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: {name}'s Id not found in datasource.";
            }
            else
            {

                bool courseEditSuccess = DataSource.EditCourse(id, name, parsedCategory, description);
                
                if(courseEditSuccess)
                {
                    response.Success = true;
                    response.Message = $"Course {name} has been updated successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Database failed to update with edited course.";
                }
            }
            return response;
        }
        public Response RemoveCourseFromCourseList(int courseId)
        {
            Response response = new Response();
            var course = DataSource.GetCourses().SingleOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: Course Id not found in datasource.";
            }

            else
            {
                bool removeCourseSuccess = DataSource.RemoveCourseFromCourseList(courseId);

                if (removeCourseSuccess)
                {
                    response.Success = true;
                    response.Message = "Course has been deleted successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error: database failed to remove course successfuly.";
                }
            }
            return response;
        }
    }
}
