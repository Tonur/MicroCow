using System;
using System.Threading;
using System.Threading.Tasks;
using MasterDataService.Domain.Commands;
using MasterDataService.Domain.Events;
using MasterDataService.Domain.Models;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace MasterDataService.Domain.CommandHandlers
{
    public class UpsertCowCommandHandler : IRequestHandler<UpsertCowCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<Cow, string> _dataServiceRepository;

        public UpsertCowCommandHandler(IEventBus eventBus, IGenericRepository<Cow, string> dataServiceRepository)
        {
            _eventBus = eventBus;
            _dataServiceRepository = dataServiceRepository;
        }

        public async Task<bool> Handle(UpsertCowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cow = await _dataServiceRepository.GetById(request.Cow.EarTag);
                if (cow == null)
                {
                    await _dataServiceRepository.Create(request.Cow as Cow);
                    _eventBus.PublishEvent(new CowUpsertedEvent(request.Cow));
                }
                else
                {
                    cow.Name = request.Cow.Name;
                    cow.Birthday = request.Cow.Birthday;

                    await _dataServiceRepository.Update(cow.EarTag, cow);
                    _eventBus.PublishEvent(new CowUpsertedEvent(cow));
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
