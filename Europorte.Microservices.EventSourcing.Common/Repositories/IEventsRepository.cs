using Europorte.Microservices.EventSourcing.Common.Entities;
using System.Threading.Tasks;

namespace Europorte.Microservices.EventSourcing.Common.Repositories
{
    public interface IEventsRepository<TAggregateRoot,TKey>
        where TAggregateRoot:class,IAggregateRoot<TKey>
    {
        Task AppendAsync(TAggregateRoot aggregateRoot);

        Task<TAggregateRoot> RehydrateAsync(TKey aggregateKey);
    }
}
