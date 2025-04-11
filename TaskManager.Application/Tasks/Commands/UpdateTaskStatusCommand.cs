using MediatR;

namespace TaskManager.Application.Tasks.Commands
{
    public record UpdateTaskStatusCommand(Guid Id, Domain.Enums.TaskStatus NewStatus) : IRequest<Unit>;
}
