using Kanadeiar.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest1.Controllers;

[ApiController]
[Route("api/test")]
[ExceptionHandling]
public class ValuesController : ControllerBase
{
    public ValuesController()
    {

    }

    [HttpGet]
    public IActionResult GetOneValue()
    {
        return Ok("test");
    }
}
