using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LocationQueryService.Domain.Events;
using LocationQueryService.Domain.Models;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared;
using Shared.Interfaces;

namespace LocationQueryService.Domain.EventHandlers
{
    public class CowUpsertedEventHandler : IEventHandler<CowUpsertedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<CowLocation, string> _genericRepository;

        public CowUpsertedEventHandler(IEventBus eventBus, IGenericRepository<CowLocation, string> genericRepository)
        {
            _eventBus = eventBus;
            _genericRepository = genericRepository;
        }

        public async Task Handle(CowUpsertedEvent @event)
        {
            try
            {
                var cowLocation = await _genericRepository.GetById(@event.Cow.EarTag);

                if (cowLocation == null)
                {
                    cowLocation = new CowLocation()
                    {
                        EarTag = @event.Cow.EarTag, 
                        Name = @event.Cow.Name
                    };
                    await _genericRepository.Create(cowLocation);
                }
                else
                {
                    cowLocation.Name = @event.Cow.Name;
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
