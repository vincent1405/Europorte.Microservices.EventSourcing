using Europorte.Microservices.EventSourcing.Common.Entities;
using System;

namespace Europorte.Microservices.EventSourcing.Common.Events
{
    public class DomainEventBase<TAggregateRoot, TKey> : IDomainEvent<TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        protected DomainEventBase() { }

        protected DomainEventBase(TAggregateRoot aggregateRoot)
        {
            AggregateVersion = aggregateRoot.Version;
            AggregateId = aggregateRoot.Id;
            Timestamp = DateTime.UtcNow;
        }

        public long AggregateVersion { get; private set; }

        public TKey AggregateId { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
