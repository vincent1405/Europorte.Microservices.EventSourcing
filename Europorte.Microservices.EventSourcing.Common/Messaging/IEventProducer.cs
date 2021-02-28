using Europorte.Microservices.EventSourcing.Common.Entities;
using System.Threading.Tasks;

namespace Europorte.Microservices.EventSourcing.Common.Messaging
{
    public interface IEventProducer<in TaggregateRoot, in TKey>
        where TaggregateRoot : IAggregateRoot<TKey>
    {
        Task DispatchAsync(TaggregateRoot aggregateRoot);
    }
}
