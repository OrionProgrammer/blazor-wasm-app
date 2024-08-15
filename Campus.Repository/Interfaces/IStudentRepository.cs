
using Campus.Repository.Helpers;

namespace Campus.Repository.Interfaces { }

public interface IStudentRepository : IGenericRepository<Student>
{
    Student Authenticate(string email, string password);
}

