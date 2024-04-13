using MediatR;
using OutboxPatternDemo.Application.Interfaces;
using OutboxPatternDemo.Domain.DomainEvents;
using OutboxPatternDemo.Domain.Repositories;

namespace OutboxPatternDemo.Application.Users.Events
{
    internal sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserRegisteredDomainEventHandler(IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByIdAsync(notification.UserId, cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
            {
                return;
            }

            await _emailService
                .SendRegisteredEmailAsync(user, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
