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
            return manager.GetStudents();
        }

        [HttpGet]
        [Route("courses")]
        public List<Course> GetCourses()
        {
            Manager manager = new Manager();
            return manager.GetCourses();
        }

        [HttpPost]
        [Route("courseenroll")]

        public Response EnrollInCourse([FromBody] EnrollmentRequest enrollmentRequest)
        {
            Manager manager = new Manager();
            Response response = manager.EnrollInCourse(enrollmentRequest.CourseId, enrollmentRequest.StudentId);

            return response;
        }

        [HttpPost]
        [Route("coursedrop")]
        public Response DropCourse([FromBody] EnrollmentRequest enrollmentRequest)
        {
            Manager manager = new Manager();
            Response response = manager.DropCourse(enrollmentRequest.CourseId, enrollmentRequest.StudentId);

            return response;
        }

        [HttpDelete]
        [Route("removestudent")]
        public Response DeleteStudent([FromBody] int studentId)
        {
            Manager manager = new Manager();
            Response response = manager.RemoveStudent(studentId);

            return response;
        }

        [HttpPost]
        [Route("addstudent")]
        public Response AddStudent([FromBody] AddStudentRequest addStudentRequest)
        {
            Manager manager = new Manager();
            Response response = manager.AddStudent(addStudentRequest.Name, addStudentRequest.Age);

            return response;
        }

        [HttpPost]
        [Route("addcourse")]
        public Response AddCourse([FromBody] AddCourseRequest addCourseRequest)
        {
            Manager manager = new Manager();
            Response response = manager.AddCourseToCourseList(addCourseRequest.Name, addCourseRequest.Category, addCourseRequest.Description);
            
            return response;
        }

        [HttpPost]
        [Route("editcourse")]
        public Response EditCourse([FromBody] EditCourseRequest editCourseRequest)
        {
            Manager manager = new Manager();
            Response response = manager.EditCourse(editCourseRequest.Id, editCourseRequest.Name, editCourseRequest.Categorey, editCourseRequest.Description);

            return response;
        }

        [HttpDelete]
        [Route("removecourse")]
        public Response DeleteCourse([FromBody] int courseId)
        {
            Manager manager = new Manager();
            Response response = manager.RemoveCourseFromCourseList(courseId);
            
            return response;
        }
    }
}
