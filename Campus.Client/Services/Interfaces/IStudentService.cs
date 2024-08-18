using Campus.Model;

namespace Campus.Client.Services.Interfaces
{
    public interface IStudentService
    {
        Task<bool> Register(StudentCourseModel studentCourseModel);
        Task<bool> DeRegister(int courseId);
        Task<List<StudentCourseModel>> FetchMyCourses(string studentId);
    }
}
