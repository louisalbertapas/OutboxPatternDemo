using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutboxPatternDemo.App.Contracts.User;
using OutboxPatternDemo.Application.Users.Commands.CreateUser;

namespace OutboxPatternDemo.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var createUserRequest = new CreateUserRequest(request.Email, request.FirstName, request.LastName);

            var result = await _mediator.Send(createUserRequest, cancellationToken);

            return Ok(result);
        }
    }
}
