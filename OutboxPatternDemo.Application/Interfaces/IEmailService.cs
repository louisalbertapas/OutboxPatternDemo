using OutboxPatternDemo.Domain.Entities;

namespace OutboxPatternDemo.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendRegisteredEmailAsync(User user, CancellationToken cancellationToken = default);
    }
}
