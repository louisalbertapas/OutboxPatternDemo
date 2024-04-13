using OutboxPatternDemo.Domain.Entities.Base;

namespace OutboxPatternDemo.Domain.DomainEvents
{
    public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent
    {
    }
}
