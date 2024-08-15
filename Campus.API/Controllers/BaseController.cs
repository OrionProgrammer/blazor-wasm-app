namespace Campus.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class BaseController : ControllerBase
{

    [NonAction]
    public List<string> GetErrors()
    {
        var errorList = (from item in ModelState
                         where item.Value.Errors.Any()
                         select item.Value.Errors[0].ErrorMessage).ToList();

        return errorList;
    }
}