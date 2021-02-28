using System;

namespace Europorte.Microservices.EventSourcing.Common.Events
{
    public interface IDomainEvent<out TKey>
    {
        long AggregateVersion { get; }

        TKey AggregateId { get; }

        DateTime Timestamp { get; }
    }
}
