using System.Collections.Generic;
using System.Text;
using LocationQueryService.Domain.Models;
using RabbitMQ.Bus.Events;
using Shared;
using Shared.Interfaces;

namespace LocationQueryService.Domain.Events
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
