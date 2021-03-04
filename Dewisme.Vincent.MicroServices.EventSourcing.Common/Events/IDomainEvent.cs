using System;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Events
{
    public interface IDomainEvent<out TKey>
    {
        long AggregateVersion { get; }

        TKey AggregateId { get; }

        DateTime Timestamp { get; }
    }
}
