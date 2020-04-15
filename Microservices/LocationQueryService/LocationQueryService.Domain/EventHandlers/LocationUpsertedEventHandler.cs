using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LocationQueryService.Domain.Events;
using LocationQueryService.Domain.Models;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace LocationQueryService.Domain.EventHandlers
{
    public class LocationUpsertedEventHandler : IEventHandler<LocationUpsertedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<CowLocation, string> _genericRepository;

        public LocationUpsertedEventHandler(IEventBus eventBus, IGenericRepository<CowLocation, string> genericRepository)
        {
            _eventBus = eventBus;
            _genericRepository = genericRepository;
        }

        public async Task Handle(LocationUpsertedEvent @event)
        {
            try
            {
                var cowLocation = await _genericRepository.GetById(@event.Location.EarTag);

                if (cowLocation == null)
                {
                    cowLocation = new CowLocation()
                    {
                        EarTag = @event.Location.EarTag, 
                        Latitude = @event.Location.Latitude,
                        Longitude = @event.Location.Longitude
                    };
                    await _genericRepository.Create(cowLocation);
                }
                else
                {
                    cowLocation.Latitude = @event.Location.Latitude;
                    cowLocation.Longitude = @event.Location.Longitude;
                    await _genericRepository.Update(cowLocation.EarTag, cowLocation);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
