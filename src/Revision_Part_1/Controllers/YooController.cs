using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Revision_Part_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YooController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("A bcdef ghi JK lMnOP choko poko Rahul!");
        }

        [HttpGet("one")]
        public IActionResult GetOne()
        {
            return Ok("One one one one one");
        }

        [HttpGet("two")]
        public IActionResult GetTwo()
        {
            return Ok("Two two two two two");
        }

        [HttpGet("three")]
        public IActionResult GetThree()
        {
            return Ok("Three three three three three");
        }

        [HttpGet("four")]
        public IActionResult GetFour()
        {
            return Ok("Four four four four four");
        }

        [HttpGet("five")]
        public IActionResult GetFive()
        {
            return Ok("Five five five five five");
        }
    }
}
