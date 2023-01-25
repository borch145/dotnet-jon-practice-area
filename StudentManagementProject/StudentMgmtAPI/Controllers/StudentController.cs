using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;
using StudentManagementSystem.Models;
using StudentManagementSystem.Data;
using StudentManagementSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMgmtAPI.DataTransferObjectModels;

namespace StudentMgmtAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        public List<Student> GetStudents()
        {
            Manager manager = new Manager();
            return manager.DataSource.Students;
        }
        [HttpGet]
        [Route("courses")]
        public List<Course> GetCourses()
        {
            Manager manager = new Manager();
            return manager.DataSource.Courses;
        }
        [HttpPost]
        [Route("courseenroll")]
        public APIResponse EnrollInCourse([FromBody] EnrollmentRequest enrollmentRequest)
        {
            Manager manager = new Manager();

            var student = manager.DataSource.Students.SingleOrDefault(s => s.Id == enrollmentRequest.StudentId);
            var course = manager.DataSource.Courses.SingleOrDefault(c => c.Id == enrollmentRequest.CourseId);

            APIResponse response = new APIResponse();


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
                response.Success = true;
                response.Message = $"{student.Name} has been enrolled in {course.Name}";
                student.Courses.Add(course);
                return response;
            }
        }

        [HttpPost]
        [Route("coursedrop")]
        public APIResponse DropCourse([FromBody] EnrollmentRequest enrollmentRequest)
        {
            Manager manager = new Manager();

            var student = manager.DataSource.Students.SingleOrDefault(s => s.Id == enrollmentRequest.StudentId);
            var course = student.Courses.SingleOrDefault(c => c.Id == enrollmentRequest.CourseId);

            APIResponse response = new APIResponse();


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
                response.Success = true;
                response.Message = $"{student.Name} has been dropped from {course.Name}";
                student.Courses.Remove(course);
                return response;
            }
        }

        [HttpDelete]
        [Route("removestudent")]
        public APIResponse DeleteStudent([FromBody] int studentId)
        {
            Manager manager = new Manager();

            var student = manager.DataSource.Students.SingleOrDefault(s => s.Id == studentId);

            APIResponse response = new APIResponse();

            if (student == null)
            {
                response.Success = false;
                response.Message = "Error: input student ID not found in datasource.";
                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "Student has been removed.";
                manager.DataSource.Students.Remove(student);
                return response;
            }
        }

        [HttpPost]
        [Route("addstudent")]
        public APIResponse AddStudent([FromBody] AddStudentRequest addStudentRequest)
        {
            APIResponse response = new APIResponse();
            response.Success = true;

            Manager manager = new Manager();
            var student = new Student()
            {
                Name = addStudentRequest.Name,
                Age = addStudentRequest.Age,
                Id = manager.DataSource.Students.Max(s => s.Id) + 1,
                Courses = new List<Course>()
            };

            if (student.Name != null & student.Age != 0 & student.Id != 0)
            {
                try
                {
                    manager.DataSource.Students.Add(student);
                }
                catch
                {
                    response.Success = false;
                    response.Message = "AddStudent() failed to add new student to datasource.";
                }
                if (response.Success)
                {
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

        [HttpPost]
        [Route("addcourse")]
        public APIResponse AddCourse([FromBody] AddCourseRequest addCourseRequest)
        {
            APIResponse response = new APIResponse();
            response.Success = true;

            Manager manager = new Manager();
            var course = new Course()
            {
                Id = manager.DataSource.Courses.Max(c => c.Id) + 1,
                Categorey = manager.ParseEnumIndexToCategorey(addCourseRequest.Categorey),
                Name = addCourseRequest.Name,
                Description = addCourseRequest.Description
            };

            if (course.Name != null & course.Description != null)
            {
                try
                {
                    manager.DataSource.Courses.Add(course);
                }
                catch
                {
                    response.Success = false;
                    response.Message = "AddCourse() failed to add new course to datasource.";
                }
                if (response.Success)
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

        [HttpPost]
        [Route("editcourse")]
        public APIResponse EditCourse([FromBody] EditCourseRequest editCourseRequest)
        {
            APIResponse response = new APIResponse();
            response.Success = true;

            Manager manager = new Manager();
            var course = manager.DataSource.Courses.SingleOrDefault(c => c.Id == editCourseRequest.Id);

            if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: {editCourseRequest.Name}'s Id not found in datasource.";
            }
            else
            {
                response.Success = true;
                response.Message = "Course has been updated successfully!";

                course.Categorey = manager.ParseEnumIndexToCategorey(editCourseRequest.Categorey);
                course.Name = editCourseRequest.Name;
                course.Description = editCourseRequest.Description;
            }

            return response;

        }

        [HttpDelete]
        [Route("removecourse")]
        public APIResponse DeleteCourse([FromBody] int courseId)
        {
            APIResponse response = new APIResponse();
            response.Success = true;

            Manager manager = new Manager();
            var course = manager.DataSource.Courses.SingleOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                response.Success = false;
                response.Message = $"Error: Course Id not found in datasource.";
            }

            else
            {
                response.Success = true;
                response.Message = "Course has been deleted successfully!";

                manager.DataSource.Courses.Remove(course);
            }

            return response;
        }
    }
}
