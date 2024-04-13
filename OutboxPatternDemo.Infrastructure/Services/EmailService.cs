using OutboxPatternDemo.Application.Interfaces;
using OutboxPatternDemo.Domain.Entities;

namespace OutboxPatternDemo.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendRegisteredEmailAsync(User user, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
