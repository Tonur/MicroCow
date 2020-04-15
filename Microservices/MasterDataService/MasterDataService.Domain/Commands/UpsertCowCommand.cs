using MasterDataService.Domain.Models;
using RabbitMQ.Bus.Commands;
using Shared.Interfaces;

namespace MasterDataService.Domain.Commands
{
    public class UpsertCowCommand : Command
    {
        public Cow Cow { get; }

        public UpsertCowCommand(Cow cow)
        {
            Cow = cow;
        }
    }
}