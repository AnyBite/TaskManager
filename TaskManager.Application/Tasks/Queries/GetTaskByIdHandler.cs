using Marten;
using MediatR;

namespace TaskManager.Application.Tasks.Queries;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItem?>
{
    private readonly IDocumentSession _session;

    public GetTaskByIdHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<TaskItem?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _session.LoadAsync<TaskItem>(request.Id, cancellationToken);
    }
}
