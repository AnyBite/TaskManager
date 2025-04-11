using Marten;
using MediatR;
using TaskManager.Infrastructure.Messaging;

namespace TaskManager.Application.Tasks.Commands;

public class AssignTaskHandler : IRequestHandler<AssignTaskCommand, Unit>
{
    private readonly IDocumentSession _session;
    private readonly EventPublisher _publisher;

    public AssignTaskHandler(IDocumentSession session, EventPublisher publisher)
    {
        _session = session;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _session.Events.AggregateStreamAsync<TaskItem>(request.Id, token: cancellationToken);
        if (task == null)
            throw new Exception("Task not found");

        task.AssignTo(request.UserId);

        _session.Events.Append(request.Id, task.Events);
        await _session.SaveChangesAsync(cancellationToken);
        await _publisher.PublishEvents(task.Events);

        return Unit.Value;
    }
}
