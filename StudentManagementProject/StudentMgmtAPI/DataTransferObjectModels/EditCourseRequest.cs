using StudentManagementSystem.Models;

namespace StudentMgmtAPI.DataTransferObjectModels
{
    public class EditCourseRequest
    {
        public int Id { get; set; }
        public int Categorey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
