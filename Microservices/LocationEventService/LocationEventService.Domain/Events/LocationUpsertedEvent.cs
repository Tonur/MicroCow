using LocationEventService.Domain.Models;
using RabbitMQ.Bus.Events;

namespace LocationEventService.Domain.Events
{
    public class LocationUpsertedEvent :Event
    {
        public Location Location { get; }

        public LocationUpsertedEvent(Location location)
        {
            Location = location;
        }
    }
}