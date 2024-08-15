using AutoMapper;
using Campus.API.Controllers;
using Campus.API.Helpers;
using Campus.Model;
using Campus.Repository.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Campus.API.Controllers { }

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IOptions<AppSettings> appSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(JsonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(JsonResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { errors = GetErrors() });

        var user = _unitOfWork.StudentRepository.Authenticate(model.Email, model.Password);
        await _unitOfWork.Complete();

        if (user is null)
        {
            return NotFound(new
            {
                errors = "Username or Password is incorrect!"
            });
        }

        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(_appSettings.JWT_TOKEN_VALIDITY_MINS);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.StudentId.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        UserSessionModel userSessionModel = new UserSessionModel
        {
            UserId = user.StudentId.ToString(),
            UserName = user.Name,
            Role = "Student",
            Token = tokenString,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
        };

        return Ok(userSessionModel);
    }
    

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] StudentModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { errors = GetErrors() });

        // map model to entity
        var student = _mapper.Map<Student>(model);
        student.StudentId = Guid.NewGuid();

        try
        {
            // create user
            await _unitOfWork.StudentRepository.InsertAsync(student);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(Register), new { id = student.StudentId});
        }
        catch (Exception ex)
        {
            // return error message if there was an exception 
            return StatusCode(StatusCodes.Status500InternalServerError, new { errors = ex.Message });
        }
    }
}

