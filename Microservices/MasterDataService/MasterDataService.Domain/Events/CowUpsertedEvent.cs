using MasterDataService.Domain.Models;
using RabbitMQ.Bus.Events;
using Shared.Interfaces;

namespace MasterDataService.Domain.Events
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