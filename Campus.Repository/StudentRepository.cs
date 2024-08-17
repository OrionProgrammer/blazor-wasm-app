using Campus.Domain;
using Campus.Repository.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Campus.Repository { }

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(DataContext context) : base(context) { }
    
    public async Task<Student> GetByUsername(string email)
    {
        return await table.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
}
