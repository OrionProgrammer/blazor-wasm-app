namespace Campus.Repository.Helpers;

using System;
using System.Threading.Tasks;
using Domain;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public IStudentRepository StudentRepository { get; }
    public IStudentCourseRepository StudentCourseRepository { get; }

    public UnitOfWork(DataContext dataContext,
        IStudentRepository _studentRepository,
        IStudentCourseRepository _studentCourseRepository)
    {
        this._context = dataContext;
        this.StudentRepository = _studentRepository;
        this.StudentCourseRepository = _studentCourseRepository;
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}