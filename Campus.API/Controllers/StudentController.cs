using AutoMapper;
using Campus.Repository.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Campus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(
              IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //register
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] StudentCourseModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = GetErrors() });

            // map model to entity
            var studentCourse = _mapper.Map<StudentCourse>(model);
            studentCourse.StudentCourseId = Guid.NewGuid();

            try
            {
                // create user
                await _unitOfWork.StudentCourseRepository.InsertAsync(studentCourse);
                await _unitOfWork.Complete();

                return CreatedAtAction(nameof(Register), new { });
            }
            catch (Exception ex)
            {
                // return error message if there was an exception 
                return StatusCode(StatusCodes.Status500InternalServerError, new { errors = ex.Message });
            }
        }

        //de-regster
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeRegister(int id)
        {
            #region Validation
            if (id == 0)
                return BadRequest(new { message = "Id not supplied" });
            #endregion

            await _unitOfWork.StudentCourseRepository.DeRegister(id);
            await _unitOfWork.Complete();
            
            return Ok();
        }

        //my-course-list
        [HttpGet("list/{id}")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List(string id)
        {
            #region Validation
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Id not supplied" });
            #endregion

            try
            {
                var result = await _unitOfWork.StudentCourseRepository.GetCoursesByStudent(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
