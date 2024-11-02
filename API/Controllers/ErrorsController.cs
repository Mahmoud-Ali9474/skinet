using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("errors/{statusCode}")]
public class ErrorsController : BaseApiController
{
    [HttpGet]
    public IActionResult Error(int statusCode)
    {
        return new ObjectResult(new ApiResponse(statusCode));
    }
}
