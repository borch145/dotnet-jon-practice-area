using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentMgmtAPI.DataTransferObjectModels;
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

        public Categorey ParseEnumIndexToCategorey(int categoreyIndex)
        {
            switch (categoreyIndex)
            {
                case 0:
                    return Categorey.Science;
                case 1:
                    return Categorey.Art;
                case 2:
                    return Categorey.Math;
                default:
                    return Categorey.Invalid;

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
                response.Success = true;
                response.Message = $"{student.Name} has been enrolled in {course.Name}";
                student.Courses.Add(course); //make this a EnrollCourse()
                return response;
            }
        }
    }
}
