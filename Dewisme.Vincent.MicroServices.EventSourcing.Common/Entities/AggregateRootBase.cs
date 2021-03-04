using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Dewisme.Vincent.Microservices.EventSourcing.Common.Entities
{
    public abstract class AggregateRootBase<TAggregateRoot, TKey> : EntityBase<TKey>, IAggregateRoot<TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        #region Factory

        private static readonly ConstructorInfo Ctor;

        static AggregateRootBase()
        {
            var aggregateType = typeof(TAggregateRoot);
            Ctor = aggregateType.GetConstructor(
                bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                binder: null,
                types: Array.Empty<Type>(),
                modifiers: Array.Empty<ParameterModifier>()
            );

            if (Ctor == null)
            {
                throw new InvalidOperationException($"Unable to find parameterless constructor for aggregate root type '{aggregateType.FullName}'.");
            }
        }

        public static TAggregateRoot Create(IEnumerable<IDomainEvent<TKey>> events)
        {
            if (events == null || !events.Any())
            {
                throw new ArgumentNullException(
                    paramName: nameof(events),
                    message: $"Cannot create instance of '{typeof(TAggregateRoot).FullName}' because the {nameof(events)} argument is null or empty."
                );
            }

            var instance = (TAggregateRoot)Ctor.Invoke(Array.Empty<object>());

            if (instance is AggregateRootBase<TAggregateRoot, TKey> baseAggregate)
            {
                foreach (var @event in events)
                {
                    baseAggregate.AddEvent(@event);
                }
            }

            instance.ClearEvents();

            return instance;
        }

        #endregion

        private readonly ConcurrentQueue<IDomainEvent<TKey>> events = new();
        protected AggregateRootBase() { }

        protected AggregateRootBase(TKey id) : base(id)
        {
        }

        public long Version { get; private set; }

        public IReadOnlyCollection<IDomainEvent<TKey>> Events => events.ToImmutableArray();

        protected void AddEvent(IDomainEvent<TKey> @event)
        {
            events.Enqueue(@event);
            Apply(@event);
            Version++;
        }

        protected abstract void Apply(IDomainEvent<TKey> @event);

        public void ClearEvents()
        {
            events.Clear();
        }
    }
}
