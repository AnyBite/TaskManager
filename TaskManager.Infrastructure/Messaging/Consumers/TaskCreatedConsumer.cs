using MassTransit;
using TaskManager.Domain.Events;

namespace TaskManager.Infrastructure.Messaging.Consumers;

public class TaskCreatedConsumer : IConsumer<TaskCreated>
{
    public Task Consume(ConsumeContext<TaskCreated> context)
    {
        Console.WriteLine($"[MassTransit] Task created: {context.Message.Title}");
        return Task.CompletedTask;
    }
}
