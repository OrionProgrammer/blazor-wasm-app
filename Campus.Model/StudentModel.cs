
using System.ComponentModel.DataAnnotations;

namespace Campus.Model { }

public class StudentModel
{
    public Guid StudentId { get; set; }

    [Required(ErrorMessage = "Name is required!")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Surname is required!")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Email is required!")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Email is Invalid!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; set; }

    public string? Token { get; set; }
}

