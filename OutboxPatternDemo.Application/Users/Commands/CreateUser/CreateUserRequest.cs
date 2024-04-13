using MediatR;

namespace OutboxPatternDemo.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserRequest(string Email, string FirstName, string LastName) : IRequest<Guid>;
}
