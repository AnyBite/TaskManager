using MassTransit;

namespace TaskManager.Infrastructure.Messaging;

public class EventPublisher
{
    private readonly IPublishEndpoint _bus;

    public EventPublisher(IPublishEndpoint bus)
    {
        _bus = bus;
    }

    public async Task PublishEvents(IEnumerable<object> events)
    {
        foreach (var evt in events)
        {
            await _bus.Publish(evt);
        }
    }
}
