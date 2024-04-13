using MediatR;

namespace OutboxPatternDemo.Domain.Entities.Base
{
    public interface IDomainEvent : INotification
    {
    }
}
