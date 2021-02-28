using Europorte.Microservices.EventSourcing.Common.Entities;
using Europorte.Microservices.EventSourcing.Common.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Europorte.Microservices.EventSourcing.Common.Messaging
{
    public class EventConsumerFactory : IEventConsumerFactory
    {
        private readonly IServiceScopeFactory scopeFactory;

        public EventConsumerFactory(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public IEventConsumer Build<TAggregateRoot, TKey>() where TAggregateRoot : IAggregateRoot<TKey>
        {
            using var scope = scopeFactory.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<IEventConsumer<TAggregateRoot, TKey>>();

            async Task OnEventReceived(object sender, IDomainEvent<TKey> receivedEvent)
            {
                var @event = EventReceivedFactory.Create((dynamic)receivedEvent);

                using var innerScope = scopeFactory.CreateScope();
                var mediator = innerScope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Publish(@event, CancellationToken.None);
            }

            consumer.EventReceived += OnEventReceived;

            return consumer;
        }
    }
}
