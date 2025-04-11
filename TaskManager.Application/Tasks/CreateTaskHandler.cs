using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using MediatR;

namespace TaskManager.Application.Tasks
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IDocumentSession _session;

        public CreateTaskHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem(Guid.NewGuid(), request.Title);

            _session.Events.StartStream<TaskItem>(task.Id, task.Events);
            await _session.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }

}
