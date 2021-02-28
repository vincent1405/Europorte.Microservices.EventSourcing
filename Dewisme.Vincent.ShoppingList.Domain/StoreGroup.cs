using Dewisme.Vincent.ShoppingList.Domain.Events;
using Europorte.Microservices.EventSourcing.Common.Entities;
using Europorte.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain
{
    public class StoreGroup : AggregateRootBase<StoreGroup, Guid>
    {
        public static StoreGroup Create(string name)
        {
            return new StoreGroup(Guid.NewGuid(), name);
        }

        private StoreGroup() { }

        public StoreGroup(Guid id, string name) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name.Trim();
        }

        public string Name { get; private set; }

        protected override void Apply(IDomainEvent<Guid> @event)
        {
            switch (@event)
            {
                case StoreGroupCreated storeGroupCreated:
                    Id = storeGroupCreated.AggregateId;
                    Name = storeGroupCreated.StoreGroupName;
                    break;

                case StoreGroupRenamed storeGroupRenamed:
                    Name = storeGroupRenamed.NewName;
                    break;
            }
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(
                    paramName:nameof(name),
                    message:$"The new name cannot be null nor empty."
                );
            }

            if(!string.Equals(name, Name, StringComparison.CurrentCulture))
            {
                AddEvent(new StoreGroupRenamed(this, name.Trim()));
            }
        }
    }
}
