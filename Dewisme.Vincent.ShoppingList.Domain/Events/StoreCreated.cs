using Dewisme.Vincent.Microservices.EventSourcing.Common.Events;
using System;

namespace Dewisme.Vincent.ShoppingList.Domain.Events
{
    public class StoreCreated : DomainEventBase<Store, Guid>
    {
        public Guid? GroupId { get; }
        public string Name { get; }

        private StoreCreated() { }

        public StoreCreated(Store store) : base(store)
        {
            GroupId = store.GroupId;
            Name = store.Name;
        }
    }
}
