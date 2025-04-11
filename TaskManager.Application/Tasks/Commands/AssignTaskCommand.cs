using MediatR;

namespace TaskManager.Application.Tasks.Commands;

public record AssignTaskCommand(Guid Id, string UserId) : IRequest<Unit>;
