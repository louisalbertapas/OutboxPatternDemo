namespace OutboxPatternDemo.Domain.Entities.Base
{
    public abstract class Entity
    {
        public Guid Id { get; private init; }

        protected Entity(Guid id) => Id = id;
    }
}
