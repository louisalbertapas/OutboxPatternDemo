using MediatR;
using Microsoft.EntityFrameworkCore;
using OutboxPatternDemo.Infrastructure.BackgroundJobs;
using OutboxPatternDemo.Persistence;
using OutboxPatternDemo.Persistence.Interceptors;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                OutboxPatternDemo.Infrastructure.AssemblyReference.Assembly,
                OutboxPatternDemo.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.AddMediatR(OutboxPatternDemo.Application.AssemblyReference.Assembly);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
{
    var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
    optionsBuilder.UseSqlServer(connectionString).AddInterceptors(interceptor);
});

builder.Services.AddQuartz(configure =>
{
    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

    configure
        .AddJob<ProcessOutboxMessagesJob>(jobKey)
        .AddTrigger(
            trigger =>
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                            schedule.WithIntervalInSeconds(10)
                                .RepeatForever()));

    configure.UseMicrosoftDependencyInjectionJobFactory();
});

builder.Services.AddQuartzHostedService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
