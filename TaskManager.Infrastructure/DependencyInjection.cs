using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Marten;
using MassTransit;
using TaskManager.Domain.Events;
using TaskManager.Infrastructure.Messaging;
using TaskManager.Infrastructure.Messaging.Consumers;
using Marten.Events.Projections;

namespace TaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddMarten(options =>
        {
            options.Connection(config.GetConnectionString("Default"));
            options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsString;
            options.Events.AddEventType(typeof(TaskCreated));
            options.Events.AddEventType(typeof(TaskAssigned));

            options.Projections.Add<TaskItemProjection>(ProjectionLifecycle.Inline);
        });

        services.AddScoped<EventPublisher>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<TaskCreatedConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}
