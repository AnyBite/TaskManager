using MediatR;

namespace TaskManager.Application.Tasks.Queries;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskItem?>;
