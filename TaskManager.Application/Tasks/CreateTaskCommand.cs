using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Tasks
{
    public record CreateTaskCommand(string Title) : IRequest<Guid>;

}
