using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet(template:"badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet(template:"badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }


        [HttpGet(template: "unauthorized")]
        public IActionResult GetUnauthorizedRequest(int id)
        {
            return Unauthorized();
        }
    }
}
