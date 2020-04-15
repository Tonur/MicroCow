using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LocationEventService.Domain.Commands;
using LocationEventService.Domain.Events;
using LocationEventService.Domain.Models;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace LocationEventService.Domain.CommandHandlers
{
    public class UpsertCowLocationCommandHandler : IRequestHandler<UpsertLocationCommand, bool>
    {
        private readonly IGenericRepository<Location, string> _genericRepository;
        private readonly IEventBus _eventBus;

        public UpsertCowLocationCommandHandler(IEventBus eventBus, IGenericRepository<Location, string> genericRepository)
        {
            _eventBus = eventBus;
            _genericRepository = genericRepository;
        }

        public async Task<bool> Handle(UpsertLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var location = await _genericRepository.GetById(request.Location.EarTag);
                if (location == null)
                {
                    await _genericRepository.Create(request.Location);
                    _eventBus.PublishEvent(new LocationUpsertedEvent(request.Location));
                }
                else
                {
                    location.Latitude = request.Location.Latitude;
                    location.Longitude = request.Location.Longitude;

                    await _genericRepository.Update(location.EarTag, location);
                    _eventBus.PublishEvent(new LocationUpsertedEvent(location));
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
