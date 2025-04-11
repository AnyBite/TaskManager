using System;


namespace TaskManager.Domain.Events
{
    public record TaskStatusChanged(string TaskId, Enums.TaskStatus NewStatus);
}
