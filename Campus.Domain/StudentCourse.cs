
using System.ComponentModel.DataAnnotations;

namespace Campus.Domain { }

public record StudentCourse
{
    [Key]
    public Guid StudentCourseId {  get; set; }
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
}

