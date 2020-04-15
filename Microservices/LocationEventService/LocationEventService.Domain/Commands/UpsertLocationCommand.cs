using LocationEventService.Domain.Models;
using RabbitMQ.Bus.Commands;

namespace LocationEventService.Domain.Commands
{
    public class UpsertLocationCommand : Command
    {
        public Location Location { get; }

        public UpsertLocationCommand(Location location)
        {
            Location = location;
        }
    }
}