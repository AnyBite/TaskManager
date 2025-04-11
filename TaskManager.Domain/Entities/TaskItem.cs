using TaskManager.Domain.Enums;
using TaskManager.Domain.Events;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string AssignedTo { get; private set; }
    public TaskStatus Status { get; private set; }

    private readonly List<object> _events = new();

    public IReadOnlyList<object> Events => _events;

    private TaskItem() { } // For Marten

    public TaskItem(Guid id, string title)
    {
        Id = id;
        Title = title;
        Status = TaskStatus.Todo;

        AddEvent(new TaskCreated(id, title));
    }

    public void AssignTo(string userId)
    {
        AssignedTo = userId;
        AddEvent(new TaskAssigned(Id, userId));
    }

    public void ChangeStatus(TaskStatus newStatus)
    {
        if (Status != newStatus)
        {
            Status = newStatus;
            AddEvent(new TaskStatusChanged(Id, newStatus));
        }
    }


    private void AddEvent(object @event) => _events.Add(@event);

}
