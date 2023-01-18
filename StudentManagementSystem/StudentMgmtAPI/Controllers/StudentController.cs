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
            Manager manager= new Manager();
            return manager.DataSource.Courses;
        }
        [HttpPost]
        [Route("courseenroll")]
        public bool EnrollInCourse([FromBody]EnrollmentRequest enrollmentRequest)
        {
            Manager manager = new Manager();
            
            var student = manager.DataSource.Students.SingleOrDefault(s => s.Id == enrollmentRequest.StudentId);
            var course = manager.DataSource.Courses.SingleOrDefault(c=> c.Id== enrollmentRequest.CourseId);

            student.Courses.Add(course);

            if(student==null || course==null) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
