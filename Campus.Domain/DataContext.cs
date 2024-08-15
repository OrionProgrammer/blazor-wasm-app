using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.Extensions.Configuration;

namespace Campus.Domain;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("CampusDB");
    }

    public DbSet<Student> Student { get; set; }
    public DbSet<StudentCourse> StudentCourse { get; set; }
}

