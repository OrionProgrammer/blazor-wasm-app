using Campus.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Campus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<CourseModel>> Index()
        {
            List<CourseModel> courseModels = new List<CourseModel>
            {
                new CourseModel
                {
                    CourseId = 1,
                    Name = "Information Technology",
                    Details = "Some information about the course goes here"
                },
                new CourseModel
                {
                    CourseId = 2,
                    Name = "Civil Engineering",
                    Details = "Some information about the course goes here"
                },
                new CourseModel
                {
                    CourseId = 3,
                    Name = "Accounts Management",
                    Details = "Some information about the course goes here"
                },
                new CourseModel
                {
                    CourseId = 4,
                    Name = "Bio Technology",
                    Details = "Some information about the course goes here"
                },
                new CourseModel
                {
                    CourseId = 5,
                    Name = "Artificial Intelligence",
                    Details = "Some information about the course goes here"
                },
            };

            return Ok(courseModels);
        }
    }
}
