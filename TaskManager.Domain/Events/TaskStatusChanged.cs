using System;


namespace TaskManager.Domain.Events
{
    public record TaskStatusChanged(Guid TaskId, Enums.TaskStatus NewStatus);
}
