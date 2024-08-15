
using Campus.Repository.Helpers;

namespace Campus.Repository.Interfaces { }

public interface IStudentCourseRepository : IGenericRepository<StudentCourse>
{
    Task<List<StudentCourse>> GetCoursesByStudent(string studentId);

    Task DeRegister(int courseId);
}

