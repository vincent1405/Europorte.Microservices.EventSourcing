using Dewisme.Vincent.ShoppingList.Domain.Events;
using Dewisme.Vincent.Microservices.EventSourcing.Common.Entities;
using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Dewisme.Vincent.ShoppingList.Domain
{
    public class Store : AggregateRootBase<Store, Guid>
    {
        private Store() { }

        public Store(Guid id, StoreGroup storeGroup, string name) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            GroupId = storeGroup?.Id;
            Name = name.Trim();
        }

        public Guid? GroupId { get; private set; }

        public string Name { get; private set; }

        protected override void Apply(IDomainEvent<Guid> @event)
        {
            switch (@event)
            {
                case StoreCreated storeCreated:
                    Id = storeCreated.AggregateId;
                    Name = storeCreated.Name;
                    GroupId = storeCreated.GroupId;
                    break;

                case StoreRenamed storeRenamed:
                    Name = storeRenamed.NewName;
                    break;

                case StoreGroupChanged storeGroupChanged:
                    GroupId = storeGroupChanged.NewStoreGroupId;
                    break;
            }
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException(paramName: nameof(newName), message: $"New name of a store cannot be null nor empty.");
            }

            AddEvent(new StoreRenamed(this, newName.Trim()));
        }

        public void AttachToGroup(StoreGroup storeGroup)
        {
            if (storeGroup == null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(storeGroup), 
                    message: $"The store group cannot be null. Please use {nameof(DetachFromGroup)} if you want to detach this store from its current group."
                );
            }

            if (storeGroup.Id != GroupId)
            {
                AddEvent(new StoreGroupChanged(this, storeGroup));
            }
        }

        public void DetachFromGroup()
        {
            if (GroupId.HasValue)
            {
                AddEvent(new StoreGroupChanged(this, null));
            }
        }
    }
}
