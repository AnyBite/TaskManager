using MediatR;
using Marten;
using TaskManager.Infrastructure.Messaging;

namespace TaskManager.Application.Tasks.Commands;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, Unit>
{
    private readonly IDocumentSession _session;
    private readonly EventPublisher _publisher;

    public UpdateTaskStatusHandler(IDocumentSession session, EventPublisher publisher)
    {
        _session = session;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _session.Events.AggregateStreamAsync<TaskItem>(request.Id, token: cancellationToken);
        if (task is null)
            throw new Exception("Task not found");

        task.ChangeStatus(request.NewStatus);

        _session.Events.Append(request.Id, task.Events);
        await _session.SaveChangesAsync(cancellationToken);
        await _publisher.PublishEvents(task.Events);

        return Unit.Value;
    }
}
