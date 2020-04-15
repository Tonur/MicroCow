using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDataService.Domain.Commands;
using MasterDataService.Domain.Models;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace MasterDataService.Application.Service
{
    public class DataService
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<Cow, string> _genericRepository;

        public DataService(IEventBus eventBus, IGenericRepository<Cow, string> genericRepository)
        {
            _eventBus = eventBus;
            _genericRepository = genericRepository;
        }

        public void Upsert(Cow cow)
        {
            _eventBus.SendCommand(new UpsertCowCommand(cow));
        }

        public async Task<Cow> Read(string earTag)
        {
            return await _genericRepository.GetById(earTag);
        }
    }
}
