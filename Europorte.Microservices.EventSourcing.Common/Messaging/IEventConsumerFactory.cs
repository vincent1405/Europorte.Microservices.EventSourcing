using Europorte.Microservices.EventSourcing.Common.Entities;

namespace Europorte.Microservices.EventSourcing.Common.Messaging
{
    public interface IEventConsumerFactory
    {
        IEventConsumer Build<TAggregateRoot, TKey>() where TAggregateRoot : IAggregateRoot<TKey>;
    }
}
