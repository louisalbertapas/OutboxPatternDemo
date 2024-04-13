using MediatR;
using OutboxPatternDemo.Domain.Entities;
using OutboxPatternDemo.Domain.Repositories;

namespace OutboxPatternDemo.Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserRequestHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = User.Create(Guid.NewGuid(), request.FirstName, request.LastName, request.Email);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
