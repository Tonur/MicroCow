using LocationEventService.Domain.Models;
using RabbitMQ.Bus.Events;

namespace LocationEventService.Domain.Events
{
    public class CowUpsertedEvent : Event
    {
        public Cow Cow { get; }

        public CowUpsertedEvent(Cow cow)
        {
            Cow = cow;
        }
    }
}