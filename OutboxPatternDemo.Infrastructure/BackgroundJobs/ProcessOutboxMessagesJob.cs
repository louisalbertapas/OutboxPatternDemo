using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OutboxPatternDemo.Domain.Entities.Base;
using OutboxPatternDemo.Persistence;
using OutboxPatternDemo.Persistence.Outbox;
using Quartz;

namespace OutboxPatternDemo.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(ApplicationDbContext applicationDbContext,
            IPublisher publisher)
        {
            _applicationDbContext = applicationDbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _applicationDbContext
                .Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            if (!messages.Any())
            {
                return;
            }

            foreach (OutboxMessage outboxMessage in messages)
            {
                IDomainEvent? domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                if (domainEvent is null)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
