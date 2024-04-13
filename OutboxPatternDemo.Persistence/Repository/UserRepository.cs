using Microsoft.EntityFrameworkCore;
using OutboxPatternDemo.Domain.Entities;
using OutboxPatternDemo.Domain.Repositories;

namespace OutboxPatternDemo.Persistence.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(User user) => _applicationDbContext.Set<User>().Add(user);
    }
}
