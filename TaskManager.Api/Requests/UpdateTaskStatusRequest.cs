namespace TaskManager.Api.Requests
{
    public record UpdateTaskStatusRequest(Domain.Enums.TaskStatus NewStatus);
}
