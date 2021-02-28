using Europorte.Microservices.EventSourcing.Common.Events;
using System.Collections.Generic;

namespace Europorte.Microservices.EventSourcing.Common.Entities
{
    /// <summary>
    /// Contract to be implemented by any aggregate root.
    /// </summary>
    /// <typeparam name="TKey">Type of the aggregate root identity.</typeparam>
    public interface IAggregateRoot<out TKey>:IEntity<TKey>
    {
        long Version { get; }

        IReadOnlyCollection<IDomainEvent<TKey>> Events { get; }

        void ClearEvents();
    }
}
