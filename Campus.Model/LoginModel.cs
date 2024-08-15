
using System.ComponentModel.DataAnnotations;

namespace Campus.Model { }

public class LoginModel
{
    [Required(ErrorMessage = "Email is required!")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Email is Invalid!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; set; }
}

