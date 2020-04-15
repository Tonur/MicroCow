using LocationEventService.Domain.Commands;
using LocationEventService.Domain.Models;
using RabbitMQ.Bus.Bus.Interfaces;

namespace LocationEventService.Application.Services
{
    public class EventService
    {
        private IEventBus _eventBus;

        public EventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Upsert(Location location)
        {
            _eventBus.SendCommand(new UpsertLocationCommand(location));
        }
    }
}
