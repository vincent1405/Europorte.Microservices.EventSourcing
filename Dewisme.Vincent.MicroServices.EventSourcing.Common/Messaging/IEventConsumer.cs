using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;
using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Messaging
{
    public interface IEventConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }

    public interface IEventConsumer<TAggregateRoot, out TKey>:IEventConsumer where TAggregateRoot : IAggregateRoot<TKey>
    {
        event EventReceivedHandler<TKey> EventReceived;
    }

    public delegate Task EventReceivedHandler<in TKey>(object sender, IDomainEvent<TKey> receivedEvent);   
}
