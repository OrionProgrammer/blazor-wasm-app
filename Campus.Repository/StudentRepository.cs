using Campus.Domain;
using Campus.Repository.Helpers;

namespace Campus.Repository { }

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(DataContext context) : base(context) { }
    
    public Student Authenticate(string email, string password)
    {
        return table.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
    }
}
