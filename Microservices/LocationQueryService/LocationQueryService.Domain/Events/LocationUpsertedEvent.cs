using LocationQueryService.Domain.Models;
using RabbitMQ.Bus.Events;
using Shared.Interfaces;

namespace LocationQueryService.Domain.Events
{
    public class LocationUpsertedEvent : Event
    {
        public Location Location { get; }

        public LocationUpsertedEvent(Location location)
        {
            Location = location;
        }
    }
}