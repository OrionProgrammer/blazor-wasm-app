using System.ComponentModel.DataAnnotations;

namespace Campus.Domain { }

public record Student
{
    [Key]
    public Guid StudentId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

