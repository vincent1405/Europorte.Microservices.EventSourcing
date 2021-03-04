using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;
using System.Threading.Tasks;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Services
{
    public interface IEventsService<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        Task PersistAsync(TAggregateRoot aggregateRoot);

        Task<TAggregateRoot> RehydrateAsync(TKey aggregateKey);
    }
}
