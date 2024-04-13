using OutboxPatternDemo.Domain.Entities;

namespace OutboxPatternDemo.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(User user);
    }
}
