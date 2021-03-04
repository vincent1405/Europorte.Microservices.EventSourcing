using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;
using Dewisme.Vincent.Microservices.EventSourcing.Common.Messaging;
using Dewisme.Vincent.Microservices.EventSourcing.Common.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Services
{
    public class EventsService<TAggregateRoot, TKey> : IEventsService<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private readonly IEventsRepository<TAggregateRoot, TKey> eventsRepository;
        private readonly IEventProducer<TAggregateRoot, TKey> eventProducer;

        public EventsService(IEventsRepository<TAggregateRoot,TKey> eventsRepository, IEventProducer<TAggregateRoot,TKey> eventProducer)
        {
            this.eventsRepository = eventsRepository ?? throw new ArgumentNullException(nameof(eventsRepository));
            this.eventProducer = eventProducer ?? throw new ArgumentNullException(nameof(eventProducer));
        }

        public async Task PersistAsync(TAggregateRoot aggregateRoot)
        {
            if(aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            if (!aggregateRoot.Events.Any())
            {
                return;
            }

            await eventsRepository.AppendAsync(aggregateRoot);

            await eventProducer.DispatchAsync(aggregateRoot);

            aggregateRoot.ClearEvents();
        }

        public async Task<TAggregateRoot> RehydrateAsync(TKey aggregateKey)
        {
            return await eventsRepository.RehydrateAsync(aggregateKey);
        }
    }
}
