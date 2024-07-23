using MediatorTest.Features.User.Commands;
using MediatorTest.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Command command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _sender.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] GetUser.Query query)
        {
            var result = await _sender.Send(query);

            return (result is not null) ? Ok(result) : BadRequest();
        }
    }
}