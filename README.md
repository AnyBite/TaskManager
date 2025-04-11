---

### 📄 `README.md`

```markdown
# 🧠 TaskManager — Event-Sourced Task Management API

A modular, event-sourced backend built with [.NET 9](https://devblogs.microsoft.com/dotnet/announcing-dotnet-9-preview-1/) using:

- ✅ **MartenDB** for event sourcing and projections
- ✅ **MassTransit** with RabbitMQ for distributed messaging
- ✅ **Clean Architecture + DDD** structure
- ✅ **Vertical Slice architecture** with MediatR
- ✅ **Minimal APIs** for fast, clean endpoints

---

## 📦 Tech Stack

| Layer | Tool |
|------|------|
| Storage | PostgreSQL + Marten |
| Messaging | MassTransit + RabbitMQ |
| Architecture | Clean Arch, DDD, Vertical Slice |
| API | .NET 9 + Minimal APIs |
| Projections | Marten `SelfAggregate<T>` or `SingleStreamProjection<T>` |
| DI | ASP.NET Core built-in |
| Testing | (Planned) xUnit or NUnit |

---

## 📂 Project Structure

```bash
TaskManager/
├── TaskManager.Api/              # Minimal API layer
├── TaskManager.Application/     # Commands, Queries, UseCases
├── TaskManager.Domain/          # Aggregates, Events, ValueObjects
├── TaskManager.Infrastructure/  # Marten, MassTransit, EventPublisher, Consumers
├── TaskManager.Persistence/     # Projections, Marten configuration
└── TaskManager.Tests/           # Unit/Integration tests (coming soon)
```

---

## ⚙️ Setup Instructions

### 1. 🔧 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- PostgreSQL (locally or via Docker)
- RabbitMQ (locally or via Docker)

---

### 2. 🔌 Database Setup

Ensure PostgreSQL is running, and create the database manually:

```bash
createdb -U postgres taskmanager
```

Or use Docker:

```bash
docker run --name postgres \
  -e POSTGRES_DB=taskmanager \
  -e POSTGRES_PASSWORD=secret \
  -p 5432:5432 \
  -d postgres
```

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Host=localhost;Database=taskmanager;Username=postgres;Password=secret"
}
```

---

### 3. 🐇 RabbitMQ Setup

Spin up RabbitMQ:

```bash
docker run -d --hostname rabbit \
  -p 5672:5672 -p 15672:15672 \
  --name rabbitmq \
  rabbitmq:management
```

Access it at: [http://localhost:15672](http://localhost:15672)  
(username: `guest`, password: `guest`)

---

### 4. 🚀 Run the App

```bash
dotnet run --project TaskManager.Api
```

Then head to: [http://localhost:5082/swagger](http://localhost:5082/swagger)

---

## 📬 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/tasks` | Create a new task |
| `GET`  | `/tasks/{id}` | Get task by ID |
| `PUT`  | `/tasks/{id}/status` | Change task status |
| `GET`  | `/tasks` | List all tasks |
| `GET`  | `/tasks/{id}/events` | Get task event stream |

---

## 📘 Domain Events

All changes to tasks are stored as immutable events:

- `TaskCreated`
- `TaskAssigned` (coming soon)
- `TaskStatusChanged`

They are stored in Marten's event store and can be replayed to rebuild aggregates or queried for audit/history.

---

## 📣 Event Bus

- Publishes events via **MassTransit**
- You can register consumers (e.g. for logging, notifications)

```csharp
public class TaskCreatedConsumer : IConsumer<TaskCreated>
{
    public Task Consume(ConsumeContext<TaskCreated> context)
    {
        Console.WriteLine($"Task created: {context.Message.Title}");
        return Task.CompletedTask;
    }
}
```

---

## 🧪 Tests (Coming Soon)

Planned:
- ✅ Unit tests for aggregates
- ✅ Integration tests for API and projections
- ✅ Event stream assertions

---

## 🔧 Dev Tips

- If using enum inputs (e.g. `status`), make sure to add this in `Program.cs`:

```csharp
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
```

---

## 📄 License

MIT (or replace with your org’s license)

---

## 🙋‍♂️ Want to Contribute?

Pull requests, issue reports, and suggestions welcome!

---

## ✨ Author

Built with love by [Your Name] — feel free to fork and customize!
```
