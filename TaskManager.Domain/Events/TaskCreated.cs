using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Events
{
    public record TaskCreated(string TaskId, string Title);
    public record TaskAssigned(string TaskId, string UserId);
}
