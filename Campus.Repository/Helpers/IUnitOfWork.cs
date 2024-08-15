namespace Campus.Repository.Helpers;

public interface IUnitOfWork
{
    public IStudentRepository StudentRepository { get; }
    public IStudentCourseRepository StudentCourseRepository { get; }

    Task Complete();

}
