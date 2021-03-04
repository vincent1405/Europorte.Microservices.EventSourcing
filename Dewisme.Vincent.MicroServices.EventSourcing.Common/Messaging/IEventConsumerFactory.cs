using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Messaging
{
    public interface IEventConsumerFactory
    {
        IEventConsumer Build<TAggregateRoot, TKey>() where TAggregateRoot : IAggregateRoot<TKey>;
    }
}
