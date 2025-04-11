using Marten.Events.Aggregation;
using TaskManager.Domain.Events;

public class TaskItemProjection : SingleStreamProjection<TaskItem>
{
    public TaskItem Create(TaskCreated @event) =>
        new TaskItem(@event.TaskId.ToString(), @event.Title);

    public void Apply(TaskAssigned @event, TaskItem task) =>
        task.AssignTo(@event.UserId);

    public void Apply(TaskStatusChanged @event, TaskItem task) =>
        task.ChangeStatus(@event.NewStatus);
}
