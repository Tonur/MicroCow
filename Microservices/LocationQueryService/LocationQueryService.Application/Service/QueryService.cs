using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationQueryService.Domain.Models;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;

namespace LocationQueryService.Application.Service
{
    public class QueryService
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<CowLocation, string> _genericRepository;

        public QueryService(IEventBus eventBus, IGenericRepository<CowLocation, string> genericRepository)
        {
            _eventBus = eventBus;
            _genericRepository = genericRepository;
        }

        public async Task<CowLocation> Report(string earTag)
        {
            return await _genericRepository.GetById(earTag);
        }
    }
}
