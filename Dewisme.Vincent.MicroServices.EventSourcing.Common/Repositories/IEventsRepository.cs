using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;
using System.Threading.Tasks;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Repositories
{
    public interface IEventsRepository<TAggregateRoot,TKey>
        where TAggregateRoot:class,IAggregateRoot<TKey>
    {
        Task AppendAsync(TAggregateRoot aggregateRoot);

        Task<TAggregateRoot> RehydrateAsync(TKey aggregateKey);
    }
}
