using Campus.Domain;
using Campus.Repository.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Campus.Repository { }

public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
{
    public StudentCourseRepository(DataContext context) : base(context) { }

    //get a list of course a student has registered for
    public async Task<List<StudentCourse>> GetCoursesByStudent(string studentId)
    {
        return await table.Where(c => c.StudentId.Equals(studentId)).ToListAsync();
    }

    public async Task DeRegister(int courseId)
    {
        var studentCourses = await table.Where(c => c.CourseId.Equals(courseId)).ToListAsync();

        try
        {
            foreach (var studentCourse in studentCourses)
            {
                table.Remove(studentCourse);
            }
        }
        catch { }
    }
}
