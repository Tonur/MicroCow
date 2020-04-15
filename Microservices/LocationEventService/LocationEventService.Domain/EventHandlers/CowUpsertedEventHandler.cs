using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationEventService.Domain.Events;
using LocationEventService.Domain.Models;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace LocationEventService.Domain.EventHandlers
{
    public class CowUpsertedEventHandler : IEventHandler<CowUpsertedEvent>
    {
        private readonly IGenericRepository<Location, string> _locationRepository;

        public CowUpsertedEventHandler(IGenericRepository<Location, string> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task Handle(CowUpsertedEvent @event)
        {
            var location = await _locationRepository.GetById(@event.Cow.EarTag);

            if (location == null)
            {
                await _locationRepository.Create(new Location(){EarTag = @event.Cow.EarTag});
            }
        }
    }
}
