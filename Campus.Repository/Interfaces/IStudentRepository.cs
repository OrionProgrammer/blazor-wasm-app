
using Campus.Repository.Helpers;

namespace Campus.Repository.Interfaces { }

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<Student> GetByUsername(string email);
}

